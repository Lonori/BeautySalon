using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.IO;

namespace BeautySalon
{
    internal class ReportPDFFactory
    {
        private static ReportPDFFactory _instance;
        private readonly string _reportDir;
        private readonly string _resourcesDir;

        public static ReportPDFFactory GetInstance()
        {
            Init();
            return _instance;
        }

        public static void Init()
        {
            if (_instance == null)
            {
                _instance = new ReportPDFFactory();
            }
        }

        private ReportPDFFactory()
        {
            _reportDir = @".\Reports";
            _resourcesDir = @".\Resources";

            if (!Directory.Exists(_reportDir))
            {
                Directory.CreateDirectory(_reportDir);
            }
        }

        public string CreateDocument(string reportPrefix, Action<Document, ReportPDFFactoryParams> constructor)
        {
            DateTime now = DateTime.Now;
            string path = $"{reportPrefix}_{now:yyyyMMdd_HHmm}.pdf";

            using (PdfWriter writer = new PdfWriter(Path.Combine(_reportDir, path)))
            using (PdfDocument pdf = new PdfDocument(writer))
            using (Document document = new Document(pdf))
            {
                PdfFont fontRegular = PdfFontFactory.CreateFont(Path.Combine(_resourcesDir, "times_new_roman.ttf"), "Identity-H");
                PdfFont fontBold = PdfFontFactory.CreateFont(Path.Combine(_resourcesDir, "times_new_roman_bold.ttf"), "Identity-H");
                constructor(document, new ReportPDFFactoryParams(now, fontRegular, fontBold));
            }
            return Path.Combine(_reportDir, path);
        }

        public Table CreateTableData(float[] pointColumnWidths)
        {
            Table table = new Table(pointColumnWidths).UseAllAvailableWidth();
            return table;
        }

        public Cell CreateCellHeader(ReportPDFFactoryParams factoryParams, string text)
        {
            Paragraph paragraph = new Paragraph(text);
            Cell cell = new Cell()
                .Add(paragraph)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetFont(factoryParams.FontBold)
                .SetPadding(5);

            return cell;
        }

        public Cell CreateCell(ReportPDFFactoryParams factoryParams, string text)
        {
            Paragraph paragraph = new Paragraph(text);
            Cell cell = new Cell()
                .Add(paragraph)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetFont(factoryParams.FontRegular)
                .SetPadding(5);

            return cell;
        }
    }

    internal class ReportPDFFactoryParams
    {
        public DateTime CreateAt { get; private set; }
        public PdfFont FontRegular { get; private set; }
        public PdfFont FontBold { get; private set; }


        public ReportPDFFactoryParams(DateTime createAt, PdfFont fontRegular, PdfFont fontBold)
        {
            CreateAt = createAt;
            FontRegular = fontRegular;
            FontBold = fontBold;
        }
    }
}
