using Data.Models;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class MakeExpense : ContentPage
{
    private readonly UserViewModel _viewModel;
	public MakeExpense(UserViewModel viewModel)
	{
        _viewModel = viewModel;
		InitializeComponent();
	}
    public async void CreateExpense(object sender, EventArgs e)
    {
        string description = Description.Text;

        if (!decimal.TryParse(Price.Text, out decimal price))
        {
            await DisplayAlert("Error", "Enter a valid price", "OK");
            return;
        }

        if (Category.SelectedItem == null)
        {
            await DisplayAlert("Error", "All fields required!", "OK");
            return;
        }

        if (!Enum.TryParse<CategoryType>(Category.SelectedItem.ToString(), out var categoryEnum))
        {
            await DisplayAlert("Error", "Invalid category!", "OK");
            return;
        }
        TypeEnum type = TypeEnum.Expenses;
        await _viewModel.AddTransactionToUser(description, price, categoryEnum, type);
        Description.Text = "";
        Price.Text = "";
        Category.SelectedIndex = -1;
        await Shell.Current.GoToAsync("//Home");
    }
    public async void OnCancel(object sender, EventArgs e)
    {
        Description.Text = "";
        Price.Text = "";
        Category.SelectedIndex = -1;
        await Shell.Current.GoToAsync("//Home");
    }
}