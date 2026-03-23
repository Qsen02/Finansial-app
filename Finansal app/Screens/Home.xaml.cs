using Microsoft.IdentityModel.Tokens;

namespace Finansal_app
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckForUser();
        }
        private async Task CheckForUser() 
        {
            string userId = await SecureStorage.GetAsync("userId");
            if (userId.IsNullOrEmpty())
            {
                await Shell.Current.GoToAsync("//Register");
            }
        }
    }
}
