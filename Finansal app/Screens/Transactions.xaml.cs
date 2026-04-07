using AndroidX.Lifecycle;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class Transactions : ContentPage
{
    private readonly TransactionViewModel viewModel;
	public Transactions(TransactionViewModel viewModel)
	{
        this.viewModel = viewModel;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadTransactionsForUser();
    }
}