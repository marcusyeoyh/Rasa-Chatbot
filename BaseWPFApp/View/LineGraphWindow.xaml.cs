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
            DataContext = this; // Set the window as the DataContext to enable data binding

            chart.AxisX.Clear(); // Clear any existing AxisX items before adding a new one
            chart.AxisX.Add(new Axis
            {
                Title = "Transaction Date",
                Labels = new ChartValues<string>()
            });

            // Create series for each unique ProductId
            var productIds = table.AsEnumerable().Select(row => row.Field<string>("ProductId")).Distinct();

            foreach (var productId in productIds)
            {
                var series = new LineSeries
                {
                    Title = productId,
                    Values = new ChartValues<ObservableValue>(),
                    DataLabels = true
                };

                // Add data points to the series based on the specified ProductId
                var dataPoints = table.AsEnumerable()
                    .Where(row => row.Field<string>("ProductId") == productId)
                    .Select(row => new
                    {
                        TransactionDate = row.Field<DateTime>("TransactionDate").ToString("yyyy-MM-dd"),
                        Quantity = row.Field<int>("Quantity")
                    });

                foreach (var dataPoint in dataPoints)
                {
                    series.Values.Add(new ObservableValue(dataPoint.Quantity));
                    chart.AxisX[0].Labels.Add(dataPoint.TransactionDate);
                }

                chart.Series.Add(series);
            }
        }

        // Custom label formatter for X-axis to format dates
        public Func<double, string> Formatter { get; } = value => new DateTime((long)value).ToString("yyyy-MM-dd");
    }
}
