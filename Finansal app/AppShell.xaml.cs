namespace Finansal_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckUser();
        }
        private async void CheckUser()
        {
            await Task.Delay(100);

            string userId = await SecureStorage.GetAsync("userId");

            if (string.IsNullOrEmpty(userId))
            {
                await Shell.Current.GoToAsync("//Register");
            }
        }

    }
}
