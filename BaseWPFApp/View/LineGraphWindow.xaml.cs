using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace BaseWPFApp.View
{
    public partial class LineGraphWindow : Window
    {
        public LineGraphWindow(DataTable table)
        {
            InitializeComponent();
            DisplayLineGraph(table);
        }

        private void DisplayLineGraph(DataTable table)
        {
            // Clear existing series from the chart
            chart.Series.Clear();

            // Create a dictionary to store the LineSeries for each unique ProductID
            var seriesDictionary = new Dictionary<string, LineSeries>();

            // Configure the mapper to extract DateTime and int values from the data table
            var mapper = Mappers.Xy<DateModel>()
                .X(dateModel => dateModel.DateTime.Ticks)
                .Y(dateModel => dateModel.Value);

            Charting.For<DateModel>(mapper);

            foreach (DataRow row in table.Rows)
            {
                string productId = row["ProductID"].ToString();
                if (!string.IsNullOrEmpty(productId))
                {
                    // Check if LineSeries for the current ProductID already exists
                    if (!seriesDictionary.ContainsKey(productId))
                    {
                        // Create a new LineSeries for the current ProductID
                        var lineSeries = new LineSeries
                        {
                            Title = "ProductID " + productId,
                            Values = new ChartValues<DateModel>(),
                            PointGeometry = DefaultGeometries.Circle, // Set the dot shape
                            PointGeometrySize = 6, // Set the dot size
                            LineSmoothness = 0, // Disable line smoothing
                            Fill = Brushes.Transparent // Set the dot fill color to transparent
                        };

                        // Add the LineSeries to the dictionary
                        seriesDictionary.Add(productId, lineSeries);
                    }

                    // Retrieve the LineSeries for the current ProductID from the dictionary
                    var series = seriesDictionary[productId];

                    if (int.TryParse(row["Quantity"].ToString(), out int quantity) &&
                        DateTime.TryParse(row["TransactionDate"].ToString(), out DateTime transactionDate))
                    {
                        // Create a new DateModel instance with the extracted values
                        var dateModel = new DateModel
                        {
                            DateTime = transactionDate,
                            Value = quantity
                        };

                        // Add the DateModel to the LineSeries values
                        series.Values.Add(dateModel);
                    }
                }
            }

            // Add all LineSeries to the chart
            foreach (var series in seriesDictionary.Values)
            {
                chart.Series.Add(series);
            }
        }
    }

    public class DateModel
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}
