using System;
using System.Data;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace BaseWPFApp.View
{
    public partial class LineGraphWindow : Window
    {
        public LineGraphWindow(DataTable table)
        {
            InitializeComponent();
            DataContext = this;

            var productIds = table.AsEnumerable().Select(row => row.Field<string>("ProductId")).Distinct();

            foreach (var productId in productIds)
            {
                var series = new LineSeries
                {
                    Title = productId,
                    Values = new ChartValues<ObservableValue>(),
                    DataLabels = true
                };

                var dataPoints = table.AsEnumerable()
                    .Where(row => row.Field<string>("ProductId") == productId)
                    .Select(row => new
                    {
                        TransactionDate = row.Field<DateTime>("TransactionDate"),
                        Quantity = row.Field<int>("Quantity")
                    })
                    .OrderBy(dataPoint => dataPoint.TransactionDate);

                foreach (var dataPoint in dataPoints)
                {
                    series.Values.Add(new ObservableValue(dataPoint.Quantity));
                }

                chart.Series.Add(series);
            }
        }

        // Custom label formatter for X-axis to format dates
        public Func<double, string> Formatter { get; } = value => new DateTime((long)value).ToString("yyyy-MM-dd");
    }
}
