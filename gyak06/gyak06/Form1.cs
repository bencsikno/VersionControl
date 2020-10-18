using gyak06.Entities;
using gyak06.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace gyak06
{
    public partial class Form1 : Form
    {

        private BindingList<RateData> Rates;
        

        public Form1()
        {
            RefreshData();
        }

        private void RefreshData()
        {
            
            Rates = new BindingList<RateData>();
            InitializeComponent();
            string result = mnbezes();

            dataGridView1.DataSource = Rates;
            xmlezes(result);
            chartozas();
        }

        private static string mnbezes()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            }; 

            var response = mnbService.GetExchangeRates(request);


            var result = response.GetExchangeRatesResult;
            return result;
        }

        private void xmlezes(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {

                var rate = new RateData();
                Rates.Add(rate);


                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
            
        }

        private void chartozas()
        {
            chart1.DataSource = Rates;

            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Rates.Clear();
            RefreshData();

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Rates.Clear();
            RefreshData();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Rates.Clear(); 
            RefreshData();
        }
    }
}
