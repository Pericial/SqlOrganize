using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WpfAppMy.Windows.TomaPosesionPdf
{

    public class TomaPosesionDocument : IDocument
    {
        public TomaPosesionData Model;
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public TomaPosesionDocument(TomaPosesionData model)
        {
            Model = model;
        }

        public void Compose(IDocumentContainer container)
        {

            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    //page.Content().Image(qrCodeImage_); ;
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);


            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Toma de Posesión CENS 462").Style(titleStyle);
                    column.Item().Image(Model.qrCode);
                });

                //row.ConstantItem(100).Height(50).Image(Model.qrCode);
            });


        }
    }
}
