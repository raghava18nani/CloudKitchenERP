using CloudKitchenERP.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CloudKitchenERP.Infrastructure.Invoice;

public class InvoiceDocument : IDocument
{
    private readonly Order _order;

    public InvoiceDocument(Order order)
    {
        _order = order;
    }

    public DocumentMetadata GetMetadata()
    {
        return DocumentMetadata.Default;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.Header()
                .Text("Telugu Inti Ruchulu")
                .FontSize(24)
                .Bold()
                .FontColor(Colors.Red.Medium);

            page.Content().Column(column =>
            {
                column.Spacing(10);

                column.Item().Text($"Invoice No : {_order.OrderNumber}");
                column.Item().Text($"Order Date : {_order.OrderDate:dd-MMM-yyyy}");
                column.Item().Text($"Customer : {_order.User.FirstName} {_order.User.LastName}");
                column.Item().Text($"Email : {_order.User.Email}");

                column.Item().LineHorizontal(1);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Item").Bold();
                        header.Cell().Text("Qty").Bold();
                        header.Cell().Text("Price").Bold();
                        header.Cell().Text("Total").Bold();
                    });

                    foreach (var item in _order.OrderItems)
                    {
                        table.Cell().Text(item.MenuItem.Name);
                        table.Cell().Text(item.Quantity.ToString());
                        table.Cell().Text(item.UnitPrice.ToString("C"));
                        table.Cell().Text(item.TotalPrice.ToString("C"));
                    }
                });

                column.Item().LineHorizontal(1);

                column.Item().AlignRight().Text($"Subtotal : {_order.SubTotal:C}");
                column.Item().AlignRight().Text($"Delivery : {_order.DeliveryCharge:C}");
                column.Item().AlignRight().Text($"Tax : {_order.Tax:C}");

                column.Item().AlignRight()
                    .Text($"Grand Total : {_order.GrandTotal:C}")
                    .Bold()
                    .FontSize(16);

                column.Item().PaddingTop(20);

                column.Item().Text($"Payment : {_order.PaymentMethod}");
                column.Item().Text($"Status : {_order.Status}");
            });

            page.Footer()
                .AlignCenter()
                .Text("Thank you for ordering from Telugu Inti Ruchulu ❤️");
        });
    }
}