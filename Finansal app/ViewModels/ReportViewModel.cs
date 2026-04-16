using Bussines_Logic;
using Data.Models;
using Finansal_app.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finansal_app.ViewModels
{
    public class ReportViewModel: INotifyPropertyChanged
    {
        private readonly TransactionService transactionService;
        public List<Transaction> transactions { get; set; }
        private decimal _allIncames;
        public decimal AllIncames
        {
            get => _allIncames;
            set
            {
                _allIncames = value;
                OnPropertyChanged();
            }
        }

        private decimal _allExpenses;
        public decimal AllExpenses
        {
            get => _allExpenses;
            set
            {
                _allExpenses = value;
                OnPropertyChanged();
            }
        }

        private CategoryReport _food;
        public CategoryReport Food
        {
            get => _food;
            set
            {
                _food = value;
                OnPropertyChanged();
            }
        }

        private CategoryReport _technologies;
        public CategoryReport Technologies
        {
            get => _technologies;
            set
            {
                _technologies = value;
                OnPropertyChanged();
            }
        }

        private CategoryReport _clothes;
        public CategoryReport Clothes
        {
            get => _clothes;
            set
            {
                _clothes = value;
                OnPropertyChanged();
            }
        }

        private CategoryReport _travels;
        public CategoryReport Travels
        {
            get => _travels;
            set
            {
                _travels = value;
                OnPropertyChanged();
            }
        }

        private CategoryReport _education;
        public CategoryReport Education
        {
            get => _education;
            set
            {
                _education = value;
                OnPropertyChanged();
            }
        }
        private decimal _difference;
        public decimal Difference
        {
            get => _difference;
            set
            {
                _difference = value;
                OnPropertyChanged();
            }
        }
        public ReportViewModel(TransactionService transactionService) 
        {
            this.transactionService = transactionService;
        }

        public async Task LoadTransactionsForLastMonth(int userId)
        {
           List<Transaction> curTransactions = await transactionService.GetUserTransactionsLastMonth(userId);
           transactions = curTransactions;
            AllIncames = curTransactions.Where(el=>el.Type == TypeEnum.Income).Sum(el=>el.Price);
            AllExpenses = curTransactions.Where(el => el.Type == TypeEnum.Expenses).Sum(el => el.Price);
            Dictionary<CategoryType,CategoryReport> categoryData = curTransactions
             .GroupBy(t => t.Category)
             .ToDictionary(
                 g => g.Key,
                 g => new CategoryReport()
                 {
                     Incames = g.Where(t => t.Type == TypeEnum.Income).Sum(t => t.Price),
                     Expenses = g.Where(t => t.Type == TypeEnum.Expenses).Sum(t => t.Price)
                 }
             );
            Food = GetCategoryReport(categoryData, CategoryType.Food);
            Technologies = GetCategoryReport(categoryData, CategoryType.Technologies);
            Clothes = GetCategoryReport(categoryData, CategoryType.Clothes);
            Travels = GetCategoryReport(categoryData, CategoryType.Travels);
            Education = GetCategoryReport(categoryData, CategoryType.Education);
            Difference = AllIncames - AllExpenses;
        }
        private CategoryReport GetCategoryReport(
        Dictionary<CategoryType, CategoryReport> data,
        CategoryType category)
            {
                return data.TryGetValue(category, out var result)
                    ? result
                    : new CategoryReport();
            }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

