using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace BaseWPFApp.View
{
    public partial class LineGraphWindow : Window
    {
        public LineGraphWindow(DataTable table)
        {
            InitializeComponent();
            ChartData = ExtractChartData(table);
            DataContext = this;
        }

        public ChartValues<int> ChartData { get; set; }

        private ChartValues<int> ExtractChartData(DataTable table)
        {
            var chartData = new ChartValues<int>();

            foreach (DataRow row in table.Rows)
            {
                if (int.TryParse(row["Quantity"].ToString(), out int quantity))
                {
                    chartData.Add(quantity);
                }
            }

            return chartData;
        }
    }
}
