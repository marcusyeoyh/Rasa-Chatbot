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
                    Values = new ChartValues<ObservablePoint>(),
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
                    series.Values.Add(new ObservablePoint(dataPoint.TransactionDate.Ticks, dataPoint.Quantity));
                }

                chart.Series.Add(series);
            }

            chart.AxisX.Add(new Axis
            {
                Title = "Transaction Date",
                LabelFormatter = Formatter
            });

            chart.AxisY.Add(new Axis
            {
                Title = "Quantity"
            });
        }

        // Custom label formatter for X-axis to format dates
        public Func<double, string> Formatter { get; } = value =>
        {
            var dateTime = new DateTime((long)value);
            return dateTime.ToString("yyyy-MM-dd");
        };
    }
}
