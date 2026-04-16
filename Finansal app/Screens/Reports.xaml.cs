using Finansal_app.Utils;
using Finansal_app.ViewModels;

namespace Finansal_app.Screens;

public partial class Reports : ContentPage
{
	private readonly UserViewModel _userViewModel;
	private readonly ReportViewModel _reportViewModel;
	public Reports(UserViewModel userViewModel, ReportViewModel reportViewModel)
	{
		_userViewModel = userViewModel;
		_reportViewModel = reportViewModel;
		InitializeComponent();
        BindingContext = _reportViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _reportViewModel.LoadTransactionsForLastMonth(_userViewModel.UserId);
    }
    public async void OnBack(object sender, EventArgs e) 
	{
		await Shell.Current.GoToAsync("//Home");
	}
    public async void OnDownloadPdf(object sender, EventArgs e)
    {
        var pdfBytes = PdfGenerator.Generate(_reportViewModel);

        var filePath = Path.Combine(FileSystem.AppDataDirectory, "report.pdf");

        File.WriteAllBytes(filePath, pdfBytes);

        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(filePath)
        });
    }
}