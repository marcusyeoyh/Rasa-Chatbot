using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace BaseWPFApp.View
{
    public partial class LineGraphWindow : Window
    {
        public LineGraphWindow(DataTable table)
        {
            InitializeComponent();
            DisplayLineGraph(table);
        }

        public void DisplayLineGraph(DataTable table)
        {
            // Clear existing series from the chart
            chart.Series.Clear();

            // Iterate over each unique ProductID
            var productIds = new HashSet<string>();
            foreach (DataRow row in table.Rows)
            {
                string productId = row["ProductID"].ToString();
                if (!string.IsNullOrEmpty(productId) && !productIds.Contains(productId))
                {
                    productIds.Add(productId);

                    // Create a new LineSeries for the current ProductID
                    var lineSeries = new LineSeries
                    {
                        Title = "ProductID " + productId,
                        Values = new ChartValues<int>(),
                        PointGeometry = null // Disable point markers
                    };

                    // Iterate over the rows with the current ProductID and add data points to the LineSeries
                    foreach (DataRow dataRow in table.Rows)
                    {
                        if (dataRow["ProductID"].ToString() == productId)
                        {
                            int quantity;
                            DateTime transactionDate;
                            // Extract the Quantity and TransactionDate values from the DataRow
                            if (int.TryParse(dataRow["Quantity"].ToString(), out quantity) &&
                                DateTime.TryParse(dataRow["TransactionDate"].ToString(), out transactionDate))
                            {
                                // Add data point to the LineSeries
                                lineSeries.Values.Add(quantity);
                            }
                        }
                    }

                    // Add the LineSeries to the chart
                    chart.Series.Add(lineSeries);
                }
            }
        }
    }
}
