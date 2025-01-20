using BeautySalon.DB;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageReportStaff : UserControl
    {
        private readonly AppDatabase _DB;
        private readonly ReportPDFFactory _reportFactory;
        private readonly System.Timers.Timer _throttleTimer;
        private Action _pendingAction;
        private List<ReportServiceModel> _reportsService = new List<ReportServiceModel>();
        private List<ReportMaterialModel> _reportsMaterial = new List<ReportMaterialModel>();

        public PageReportStaff()
        {
            _DB = AppDatabase.GetInstance();
            _reportFactory = ReportPDFFactory.GetInstance();

            _throttleTimer = new System.Timers.Timer(1000);
            _throttleTimer.AutoReset = false;
            _throttleTimer.Elapsed += (sender, args) =>
            {
                if (_pendingAction != null)
                {
                    Invoke(_pendingAction);
                    _pendingAction = null;
                }
            };

            InitializeComponent();

            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);

            InitStaffs();
        }

        private async void InitStaffs()
        {
            using (MySqlCommand command = new MySqlCommand("SELECT `id`, `full_name` FROM `staffs`", _DB.Connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        comboBox1.Items.Add(new ComboBoxItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                    if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void ReportCreate(Document doc, ReportPDFFactoryParams fp)
        {
            Paragraph title = new Paragraph($"Отчет по сотруднику\n{((ComboBoxItem)comboBox1.SelectedItem).Text}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetFont(fp.FontBold)
                .SetMarginBottom(10);

            Paragraph metadata = new Paragraph($"Период: {dateTimePicker1.Value:dd.MM.yyyy} - {dateTimePicker2.Value:dd.MM.yyyy}")
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(12)
               .SetFont(fp.FontRegular)
               .SetMarginBottom(20);

            Table tableService = _reportFactory.CreateTableData(new float[] { 1, 2, 3, 1 })
                .SetMarginTop(10);

            string[] headersService = new string[] { "№", "Дата", "Услуги", "Цена" };

            foreach (string header in headersService)
            {
                Cell cell = _reportFactory.CreateCellHeader(fp, header);
                tableService.AddHeaderCell(cell);
            }

            float totalService = 0;
            for (int i = 0; i < _reportsService.Count; i++)
            {
                tableService.AddCell(_reportFactory.CreateCell(fp, (i + 1).ToString()));
                tableService.AddCell(_reportFactory.CreateCell(fp, _reportsService[i].Time.ToString("dd.MM.yyyy")));
                tableService.AddCell(_reportFactory.CreateCell(fp, _reportsService[i].Name));
                tableService.AddCell(_reportFactory.CreateCell(fp, _reportsService[i].Price.ToString("0.00 руб")));

                totalService += _reportsService[i].Price;
            }

            Paragraph summaryService = new Paragraph($"Общая цена услуг: {totalService:0.00 руб}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(14)
                .SetFont(fp.FontBold)
                .SetMarginTop(20);

            Table tableMaterial = _reportFactory.CreateTableData(new float[] { 1, 2, 3, 1, 1 })
                .SetMarginTop(10);

            string[] headersMaterial = new string[] { "№", "Дата", "Материал", "Кол-во", "Цена" };

            foreach (string header in headersMaterial)
            {
                Cell cell = _reportFactory.CreateCellHeader(fp, header);
                tableMaterial.AddHeaderCell(cell);
            }

            float totalMaterial = 0;
            for (int i = 0; i < _reportsMaterial.Count; i++)
            {
                tableMaterial.AddCell(_reportFactory.CreateCell(fp, (i + 1).ToString()));
                tableMaterial.AddCell(_reportFactory.CreateCell(fp, _reportsMaterial[i].Time.ToString("dd.MM.yyyy")));
                tableMaterial.AddCell(_reportFactory.CreateCell(fp, _reportsMaterial[i].Name));
                tableMaterial.AddCell(_reportFactory.CreateCell(fp, _reportsMaterial[i].Amount.ToString("0 шт")));
                tableMaterial.AddCell(_reportFactory.CreateCell(fp, _reportsMaterial[i].Price.ToString("-0.00 руб")));

                totalMaterial += _reportsMaterial[i].Price * _reportsMaterial[i].Amount;
            }

            Paragraph summaryMaterial = new Paragraph($"Общая цена материалов: {totalMaterial:-0.00 руб}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(14)
                .SetFont(fp.FontBold)
                .SetMarginTop(20);

            Paragraph signature = new Paragraph($"\n\nДата формирования: {fp.CreateAt:dd.MM.yyyy}\nПодпись ответственного лица: _____________________________")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
                .SetFont(fp.FontRegular)
                .SetMarginTop(30);

            Paragraph footer = new Paragraph("М.П.")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(12)
                .SetFont(fp.FontRegular)
                .SetMarginTop(10);

            doc.Add(title);
            doc.Add(metadata);
            doc.Add(tableService);
            doc.Add(summaryService);
            doc.Add(tableMaterial);
            doc.Add(summaryMaterial);
            doc.Add(signature);
            doc.Add(footer);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pendingAction = () => UpdateData();
            _throttleTimer.Stop();
            _throttleTimer.Start();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _pendingAction = () => UpdateData();
            _throttleTimer.Stop();
            _throttleTimer.Start();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            _pendingAction = () => UpdateData();
            _throttleTimer.Stop();
            _throttleTimer.Start();
        }

        private async void ButtonReport_Click(object sender, EventArgs e)
        {

            string reportPath = _reportFactory.CreateDocument("ReportStaff", ReportCreate);

            Process openProcess = new Process();
            openProcess.StartInfo = new ProcessStartInfo
            {
                FileName = reportPath,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            openProcess.Start();
        }

        private async void UpdateData()
        {
            textBox1.Clear();
            try
            {
                string staff = ((ComboBoxItem)comboBox1.SelectedItem)?.Text ?? "";
                DateTime timeStart = dateTimePicker1.Value;
                DateTime timeEnd = dateTimePicker2.Value;

                textBox1.AppendText("\r\n\r\n");
                textBox1.AppendText($"Сотрудник: {staff}");
                textBox1.AppendText("\r\n");
                textBox1.AppendText($"Период: {timeStart:d} - {timeEnd:d}");
                textBox1.AppendText("\r\n\r\n");

                string queryClient = @"
                    SELECT
                        COUNT(`id`)
                    FROM `orders`
                    WHERE `time` >= @time_start AND `time` <= @time_end AND `staff_id` = @staff_id";
                using (MySqlCommand command = new MySqlCommand(queryClient, _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            textBox1.AppendText($"Ослужено поситителей: {reader.GetInt32(0)}");
                            textBox1.AppendText("\r\n");
                        }
                    }
                }

                string queryService = @"
                SELECT
                    `orders`.`time`,
                    `services`.`name`,
                    `services_fulfilled`.`price`
                FROM `orders`
                INNER JOIN `services_fulfilled` ON `orders`.`id` = `services_fulfilled`.`order_id`
                INNER JOIN `services` ON `services`.`id` = `services_fulfilled`.`service_id`
                WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end AND `orders`.`staff_id` = @staff_id;";
                using (MySqlCommand command = new MySqlCommand(queryService, _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        _reportsService.Clear();
                        while (await reader.ReadAsync())
                        {
                            _reportsService.Add(new ReportServiceModel
                            {
                                Time = reader.GetDateTime(0),
                                Name = reader.GetString(1),
                                Price = reader.GetFloat(2)
                            });
                        }
                    }
                }

                float priceStaff = _reportsService.Sum((report) => report.Price);
                textBox1.AppendText($"Сумма оказаных услуг: {priceStaff:0.00 руб}");
                textBox1.AppendText("\r\n");

                string queryMaterial = @"
                SELECT
                    `orders`.`time`,
                    `materials`.`name`,
                    `materials_consumption`.`amount`,
                    `materials_consumption`.`price`
                FROM
                    `orders`
                INNER JOIN `materials_consumption` ON `orders`.`id` = `materials_consumption`.`order_id`
                INNER JOIN `materials` ON `materials`.`id` = `materials_consumption`.`material_id`
                WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end AND `orders`.`staff_id` = @staff_id;";
                using (MySqlCommand command = new MySqlCommand(queryMaterial, _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        _reportsMaterial.Clear();
                        while (await reader.ReadAsync())
                        {
                            _reportsMaterial.Add(new ReportMaterialModel
                            {
                                Time = reader.GetDateTime(0),
                                Name = reader.GetString(1),
                                Amount = reader.GetInt32(2),
                                Price = reader.GetFloat(3)
                            });
                        }
                    }
                }

                float priceMaterial = _reportsMaterial.Sum((report) => report.Price * report.Amount);
                textBox1.AppendText($"Сумма израсходаванных материалов: {priceMaterial:0.00 руб}");
            }
            catch (Exception ex)
            {
                textBox1.Clear();
                AlertBox.Error("Ошибка формирования отчета:\n" + ex.Message);
            }
        }
    }

    class ReportServiceModel
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }

    class ReportMaterialModel
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
    }
}
