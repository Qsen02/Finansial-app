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
}