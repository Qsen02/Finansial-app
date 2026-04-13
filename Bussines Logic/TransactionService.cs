using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic
{
    public class TransactionService
    {
        private readonly FinansalContext _context;
        public TransactionService(FinansalContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionsById(int transactionId) 
        {
            Transaction? transaction = await _context.Transactions.Include(el=>el.User).FirstOrDefaultAsync(el=>el.Id == transactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found!");
            }
            return transaction;
        }
        public async Task<Transaction> CreateTransaction(string description, decimal price, CategoryType category, TypeEnum type) {
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
        public async Task<List<Transaction>> GetAllTransactionsForUser(int userId) 
        {
            User? user = await _context.Users.Include(el => el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            List<Transaction> transactions = user.Transactions.ToList();
            return transactions;
        }
        public async Task<List<Transaction>> GetAllIncamesForUser(int userId)
        {
            User? user = await _context.Users.Include(el => el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            List<Transaction> transactions = user.Transactions.Where(el=>el.Type == TypeEnum.Income).ToList();
            return transactions;
        }
        public async Task<List<Transaction>> GetAllExpensesForUser(int userId)
        {
            User? user = await _context.Users.Include(el => el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            List<Transaction> transactions = user.Transactions.Where(el => el.Type == TypeEnum.Expenses).ToList();
            return transactions;
        }
        public async Task<List<Transaction>> GetUserTransactionsByDate(int userId,int year, int month) 
        {
            User? user = await _context.Users.Include(el => el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            var start = new DateOnly(year, month, 1);
            var end = start.AddMonths(1);

            List<Transaction> transactions= user.Transactions
                .Where(t => t.Created_at >= start && t.Created_at < end)
                .ToList();
            return transactions;
        }
        public async Task<List<Transaction>> SearchTransactions(int userId,string keyword,CategoryType? category) 
        {
            User? user = await _context.Users.Include(el => el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            if (category != null)
            {
                List<Transaction> transactions = user.Transactions
               .Where(el => el.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase) && el.Category == category)
               .ToList();
                return transactions;
            }
            else
            {
                List<Transaction> transactions = user.Transactions
                    .Where(el => el.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return transactions;
            }
        }
    }
}
