using Finansal_app.ViewModels;
using Microsoft.IdentityModel.Tokens;

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
            string keyword = Keyword.Text;
            if (keyword == null) {
                keyword = string.Empty;
            }
            await viewModel.SearchTransactionsByKeywords(userViewModel.UserId, keyword);
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