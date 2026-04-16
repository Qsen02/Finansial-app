using Finansal_app.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Finansal_app.Utils
{
    public class PdfGenerator
    {
        public static byte[] Generate(ReportViewModel model)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        // 🟦 Title
                        col.Item().AlignCenter().Text("Monthly Report")
                            .FontSize(22).Bold();

                        col.Item().PaddingBottom(20);

                        // 🟦 Main section (2 колони)
                        col.Item().AlignCenter().Row(row =>
                        {
                            // 🟩 INCOMES
                            row.RelativeItem().Column(left =>
                            {
                                left.Item().Text("Incomes").FontSize(18).Bold();

                                left.Item().Text($"Food: {model.Food.Incames:F2} €");
                                left.Item().Text($"Technologies: {model.Technologies.Incames:F2} €");
                                left.Item().Text($"Clothes: {model.Clothes.Incames:F2} €");
                                left.Item().Text($"Travels: {model.Travels.Incames:F2} €");
                                left.Item().Text($"Education: {model.Education.Incames:F2} €");

                                left.Item().PaddingTop(10);
                                left.Item().Text($"Total: {model.AllIncames:F2} €").Bold();
                            });

                            // 🟥 EXPENSES
                            row.RelativeItem().Column(right =>
                            {
                                right.Item().Text("Expenses").FontSize(18).Bold();

                                right.Item().Text($"Food: {model.Food.Expenses:F2} €");
                                right.Item().Text($"Technologies: {model.Technologies.Expenses:F2} €");
                                right.Item().Text($"Clothes: {model.Clothes.Expenses:F2} €");
                                right.Item().Text($"Travels: {model.Travels.Expenses:F2} €");
                                right.Item().Text($"Education: {model.Education.Expenses:F2} €");

                                right.Item().PaddingTop(10);
                                right.Item().Text($"Total: {model.AllExpenses:F2} €").Bold();
                            });
                        });

                        // 🟦 Difference
                        col.Item().PaddingTop(30);
                        col.Item().AlignCenter().Text(
                            $"Your income for this month is: {model.Difference:F2} €"
                        ).FontSize(16).Bold();
                    });
                });
            }).GeneratePdf();
        }
    }
}
