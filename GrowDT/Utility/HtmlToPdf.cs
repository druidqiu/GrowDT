using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace GrowDT.Utility
{
    //http://demo.itextsupport.com/xmlworker/itextdoc/flatsite.html
    //http://www.cnblogs.com/wit13142/p/3690341.html

    public static class HtmlToPdf
    {
        private enum PdfFont
        {
            //ZhongS,
            Arial
        }

        public static bool GeneratePdf(string htmlText, string fileFullName)
        {
            return GeneratePdf(htmlText, fileFullName, "", PdfFont.Arial);
        }

        public static bool GeneratePdfWithWatermark(string htmlText, string fileFullName, string watermarkText)
        {
            return GeneratePdf(htmlText, fileFullName, watermarkText, PdfFont.Arial);
        }

        public static void AddWatermarkToPdf(string fileFullName, string watermarkText)
        {
            string tempPdfFileName = fileFullName + ".temp.pdf";
            File.Copy(fileFullName, tempPdfFileName);
            SetWatermark(tempPdfFileName, fileFullName, watermarkText);
            File.Delete(tempPdfFileName);
        }

        private static bool GeneratePdf(string htmlText, string fileFullName, string watermarkText, PdfFont font)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return false;
            }

            htmlText = "<p>" + htmlText + "</p>";

            var document = new Document();
            var writer = PdfWriter.GetInstance(document, new FileStream(fileFullName, FileMode.Create));
            if (!string.IsNullOrEmpty(watermarkText))
            {
                writer.PageEvent = new PdfWatermarkPageEvent(watermarkText);
            }

            document.Open();

            //pipeline
            var htmlContext = new HtmlPipelineContext(null);
            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
            htmlContext.SetImageProvider(new ChannelImageProvider());

            htmlContext.SetCssAppliers(new CssAppliersImpl(GetFontProviderBy(font)));
            var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
            var pipeline = new CssResolverPipeline(cssResolver,
                new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));

            //parse
            byte[] data = Encoding.UTF8.GetBytes(htmlText);
            var msInput = new MemoryStream(data);
            var worker = new XMLWorker(pipeline, true);
            var parser = new XMLParser(worker);
            parser.Parse(msInput); //XMLWorkerHelper.GetInstance().ParseXHtml(..)
            var pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);
            var action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);

            //close
            document.Close();
            msInput.Close();

            return true;
        }

        private static IFontProvider GetFontProviderBy(PdfFont font)
        {
            IFontProvider fontProvider = null;
            if (font == PdfFont.Arial)
            {
                fontProvider = new CustomFontProvider();
            }

            return fontProvider;
        }

        private static void SetWatermark(string originalPdfFileName, string finalFileName, string watermarkText)
        {
            using (var pdfReader = new PdfReader(originalPdfFileName))
            {
                using (var newOutputStream = new FileStream(finalFileName, FileMode.Create))
                {
                    using (PdfStamper stamper = new PdfStamper(pdfReader, newOutputStream))
                    {
                        int pageCount = pdfReader.NumberOfPages;
                        var layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                        for (int i = 1; i <= pageCount; i++)
                        {
                            var rect = pdfReader.GetPageSize(i);
                            var watermarkContent = stamper.GetUnderContent(i);
                            //PdfContentByte watermarkContent = stamper.GetOverContent(i);
                            watermarkContent.BeginLayer(layer);
                            watermarkContent.SetFontAndSize(
                                BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 80);

                            var gState = new PdfGState { FillOpacity = 0.6f };
                            watermarkContent.SetGState(gState);

                            watermarkContent.SetColorFill(BaseColor.LIGHT_GRAY);
                            watermarkContent.BeginText();
                            watermarkContent.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, rect.Width / 2,
                                rect.Height / 2, 45f);
                            watermarkContent.EndText();

                            watermarkContent.EndLayer();
                        }
                    }
                }
            }
        }
    }

    public class ChannelImageProvider : AbstractImageProvider
    {
        public override string GetImageRootPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }

    public class CustomFontProvider : FontFactoryImp
    {
        private static readonly string ArialFontPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialuni.ttf");

        public override Font GetFont(string fontname, string encoding, bool embedded, float size,
            int style, BaseColor color, bool cached)
        {
            BaseFont baseFont = BaseFont.CreateFont(ArialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            return new Font(baseFont, size, style, color);
        }
    }

    public class PdfWatermarkPageEvent : PdfPageEventHelper
    {
        private readonly string _watermarkText;
        private readonly float _fontSize;
        private readonly float? _xPosition;
        private readonly float? _yPosition;
        private readonly float _angle;

        public PdfWatermarkPageEvent(string watermarkText, float fontSize = 80f,
            float angle = 45f, float? xPosition = null, float? yPosition = null)
        {
            _watermarkText = watermarkText;
            _fontSize = fontSize;
            _xPosition = xPosition;
            _yPosition = yPosition;
            _angle = angle;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            var watermarkContent = writer.DirectContentUnder;
            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            watermarkContent.BeginText();
            var gState = new PdfGState
            {
                FillOpacity = 0.5f,
                OverPrintStroking = false
            };
            watermarkContent.SetGState(gState);
            watermarkContent.SetColorFill(BaseColor.LIGHT_GRAY);
            watermarkContent.SetFontAndSize(baseFont, _fontSize);
            var rect = document.PageSize;
            watermarkContent.ShowTextAligned(PdfContentByte.ALIGN_CENTER, _watermarkText,
                _xPosition ?? rect.Width / 2, _yPosition ?? rect.Height / 2, _angle);
            watermarkContent.EndText();
        }
    }
}
