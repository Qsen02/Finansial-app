using Finansal_app.ViewModels;

namespace Finansal_app.Screens
{
    public partial class Home : ContentPage
    {
        private readonly UserViewModel _viewModel;
        public Home(UserViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadUserAsync();

            _viewModel.BalanceUpdated += () =>
            {
            };
        }

        public async void GoToExpenseForm(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//MakeExpense");
        }
        public async void GoToIncameForm(object sender, EventArgs e) 
        {
             await Shell.Current.GoToAsync("//MakeIncame");
        }
        public async void MakeReport(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//Reports");
        }
        public async void SeeTransactions(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//Transactions");
        }
    }
}
