namespace Finansal_app.Screens;

public partial class Reports : ContentPage
{
	public Reports()
	{
		InitializeComponent();
	}
	public async void OnBack(object sender, EventArgs e) 
	{
		await Shell.Current.GoToAsync("//Home");
	}
}