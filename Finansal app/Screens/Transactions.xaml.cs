using Data.Models;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class Transactions : ContentPage
{
    private readonly TransactionViewModel viewModel;
    private readonly UserViewModel userViewModel;
	public Transactions(TransactionViewModel viewModel,UserViewModel userViewModel)
	{
        InitializeComponent();
        this.viewModel = viewModel;
        this.userViewModel = userViewModel;
        BindingContext = viewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadTransactionsForUser(userViewModel.UserId);
    }
    public async void SortByIncames(object sender,EventArgs e) 
    {
        await viewModel.LoadIncamesForUser(userViewModel.UserId);
    }
    public async void SortByExpenses(object sender, EventArgs e)
    {
        await viewModel.LoadExpensesForUser(userViewModel.UserId);
    }
    public async void OnSearch(object sender, EventArgs e)
    {
        try
        {
            CategoryType? categoryEnum = null;
            string keyword = Keyword.Text;
            string category = Category.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(keyword)) {
                keyword = string.Empty;
            }

            if (category != "All" && category != null)
            {
                categoryEnum = Enum.Parse<CategoryType>(category);
            }

            await viewModel.SearchTransactions(userViewModel.UserId, keyword, categoryEnum);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }
    public async void OnBack(object sender, EventArgs e) 
    {
        await Shell.Current.GoToAsync("//Home");
    }
}