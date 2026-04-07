using Bussines_Logic;
using Data.Models;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class Login : ContentPage
{
    private readonly UserViewModel _viewModel;
	public Login(UserViewModel viewModel)
	{
        _viewModel = viewModel;
		InitializeComponent();
	}
    private async void OnLogin(object sender, EventArgs e)
    {
        try
        {
            string name = NameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "All fields required!", "OK");
                return;
            }

            User newUser = await _viewModel.Login(name, password);
            await SecureStorage.SetAsync("userId", newUser.Id.ToString());
            await Shell.Current.GoToAsync("//Home");
        }
        catch (Exception err) 
        {
            await DisplayAlert("Error", err.Message, "OK");
            return;
        }
    }

    private async void GoToRegister(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Register");
    }
}