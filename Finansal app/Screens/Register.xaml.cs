using Bussines_Logic;
using Data.Models;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class Register : ContentPage
{
	private readonly UserViewModel _viewModel;
	public Register(UserViewModel viewModel)
	{
        _viewModel = viewModel;
		InitializeComponent();
	}

    private async void OnRegister(object sender, EventArgs e) 
	{
		try
		{
			string name = NameEntry.Text;
			string email = EmailEntry.Text;
			string password = PasswordEntry.Text;
			string repass = RepassEntry.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repass))
			{
				await DisplayAlert("Error", "All fields required!", "OK");
				return;
			}
			if (repass != password)
			{
				await DisplayAlert("Error", "Password must match!", "OK");
				return;
			}

			User newUser=await _viewModel.Register(name, email, password);
			await SecureStorage.SetAsync("userId", newUser.Id.ToString());
            await Shell.Current.GoToAsync("//Home");
		}
		catch (Exception err) 
		{
            await DisplayAlert("Error", err.Message, "OK");
			return;
        }
    }

	private async void GoToLogin(object sender, EventArgs e) 
	{
		await Shell.Current.GoToAsync("//Login");
	}
}