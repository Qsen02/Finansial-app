using Bussines_Logic;
using Data.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finansal_app.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private readonly TransactionService _transactionService;
        public ObservableCollection<Transaction> Transactions { get; set; } = new();
        public TransactionViewModel(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task<Transaction> CreateTransaction(string description, decimal price, CategoryType category, TypeEnum type) 
        {
            return await _transactionService.CreateTransaction(description, price, category, type);
        }
        public async Task LoadTransactionsForUser(int UserId) 
        {
            List<Transaction> transactions = await _transactionService.GetAllTransactionsForUser(UserId);
            Transactions.Clear();
            foreach (Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        public async Task LoadIncamesForUser(int UserId)
        {
            List<Transaction> transactions = await _transactionService.GetAllIncamesForUser(UserId);
            Transactions.Clear();
            foreach (Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        public async Task LoadExpensesForUser(int UserId)
        {
            List<Transaction> transactions = await _transactionService.GetAllExpensesForUser(UserId);
            Transactions.Clear();
            foreach (Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        public async Task SearchTransactions(int userId, string keyword, CategoryType? category) 
        {
            List<Transaction> transactions = await _transactionService.SearchTransactions(userId,keyword,category);
            Transactions.Clear();
            foreach (Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
