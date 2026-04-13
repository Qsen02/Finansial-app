using Bussines_Logic;
using Data;
using Finansal_app.Screens;
using Finansal_app.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Finansal_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddDbContext<FinansalContext>(options =>
            options.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinancialDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"
            ));
            builder.Services.AddSingleton<TransactionService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<UserViewModel>();
            builder.Services.AddSingleton<TransactionViewModel>();
#endif

            return builder.Build();
        }
    }
}
