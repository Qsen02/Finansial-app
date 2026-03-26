using Bussines_Logic;

namespace Finansal_app.Screens;

public partial class Login : ContentPage
{
    private readonly UserService _userService;
	public Login(UserService userService)
	{
        _userService = userService;
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

            await _userService.Login(name, password);
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