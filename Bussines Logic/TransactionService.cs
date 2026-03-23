using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic
{
    public class TransactionService
    {
        private readonly FinansalContext _context;
        private readonly UserService _userService;
        public TransactionService(FinansalContext context,UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Transaction> GetTransactionsById(int transactionId) 
        {
            Transaction transaction = await _context.Transactions.Include(el=>el.User).FirstOrDefaultAsync(el=>el.Id == transactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found!");
            }
            return transaction;
        }
        public async Task<Transaction> CreateTransaction(string description, int price, CategoryType category, TypeEnum type) {
            Transaction newTrasaction = new Transaction() 
            {
                Description = description,
                Price = price,
                Category = category,
                Type = type
            };
            _context.Transactions.Add(newTrasaction);
            await _context.SaveChangesAsync(); 
            return newTrasaction;
        }
        public async Task<List<Transaction>> GetAllIncamesForUser(int userId) 
        {
            User user = await _userService.GetUserById(userId);
            List<Transaction> incames = user.Transactions.Where(el => el.Type == TypeEnum.Income).ToList();
            return incames;
        }
        public async Task<List<Transaction>> GetUserTransactionsByDate(int userId,int year, int month) 
        {
            User user= await _userService.GetUserById(userId);
            var start = new DateOnly(year, month, 1);
            var end = start.AddMonths(1);

            List<Transaction> transactions= user.Transactions
                .Where(t => t.Created_at >= start && t.Created_at < end)
                .ToList();
            return transactions;
        }
        public async Task<List<Transaction>> SearchTransactionsByKeywords(int userId,string keyword) 
        {
            User user = await _userService.GetUserById(userId);

            List<Transaction> transactions = user.Transactions
                .Where(el=>EF.Functions.Like(el.Description,$"%{keyword}%"))
                .ToList();
            return transactions;
        }

        public async Task<List<Transaction>> SearchTransactionsByCategory(int userId, CategoryType category)
        {
            User user = await _userService.GetUserById(userId);

            List<Transaction> transactions = user.Transactions
                .Where(el => el.Category == category)
                .ToList();
            return transactions;
        }
    }
}
