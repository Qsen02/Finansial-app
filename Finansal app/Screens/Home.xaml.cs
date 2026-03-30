using Finansal_app.ViewModels;

namespace Finansal_app.Screens
{
    public partial class Home : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private bool _loaded;
        public Home(HomeViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_loaded) return;
            _loaded = true;
            await _viewModel.LoadUserAsync();
        }

        public void GoToExpenseForm(object sender, EventArgs e) 
        {

        }
        public void GoToIncameForm(object sender, EventArgs e) 
        {

        }
        public void MakeReport(object sender, EventArgs e) 
        {

        }
    }
}
