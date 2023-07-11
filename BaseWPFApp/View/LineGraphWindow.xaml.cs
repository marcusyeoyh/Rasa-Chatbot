using System;
using System.Data;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Configurations;
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

            var mapper = Mappers.Xy<ObservablePoint>()
                .X(point => point.X)
                .Y(point => point.Y);

            Charting.For<ObservablePoint>(mapper);

            Axis xAxis = null; // Variable to store the correct X-axis

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
                    .Select(row => new ObservablePoint(
                        row.Field<DateTime>("TransactionDate").Ticks,
                        row.Field<int>("Quantity")))
                    .OrderBy(point => point.X);

                series.Values.AddRange(dataPoints);

                chart.Series.Add(series);

                // Store the X-axis from the first series
                if (xAxis == null)
                {
                    xAxis = new Axis
                    {
                        Title = "Transaction Date",
                        LabelFormatter = Formatter,
                        Separator = new Separator
                        {
                            Step = TimeSpan.FromDays(1).Ticks * 30, // One month step
                            IsEnabled = true
                        }
                    };

                    chart.AxisX.Add(xAxis);
                }
            }

            // Remove the unwanted axis
            chart.AxisX.Remove(chart.AxisX.First(axis => axis != xAxis));

            chart.AxisY.Add(new Axis
            {
                Title = "Quantity"
            });
        }

        // Custom label formatter for X-axis to format dates
        public Func<double, string> Formatter { get; } = value =>
        {
            if (value < DateTime.MinValue.Ticks)
                return string.Empty;

            var dateTime = new DateTime((long)value);
            return dateTime.ToString("yyyy-MM-dd");
        };
    }
}
