using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;


namespace ProjectMentorMatch.Views;

public partial class Analytics : ContentPage
{

    public Analytics()
    {
        InitializeComponent();

        int profileID = 2; // Placeholder, cannot retrieve profileID HUHU T_T
        var entries = GetChartEntries(profileID);

        chartView.Chart = new LineChart()
        {
            Entries = entries,
            BackgroundColor = SKColors.White
        };
    }
    private ChartEntry[] GetChartEntries(int profileID)
    {
        using (var connection = Database.GetConnection())
        {
            connection.Open();

            var query = "SELECT day, brainReact FROM Analytics WHERE ProfileID = @ProfileID";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);

                using (var reader = command.ExecuteReader())
                {
                    var chartEntries = new List<ChartEntry>();

                    while (reader.Read())
                    {
                        var day = reader["day"].ToString();
                        var brainReact = Convert.ToSingle(reader["brainReact"]);

                        chartEntries.Add(new ChartEntry(brainReact)
                        {
                            Label = day,
                            ValueLabel = brainReact.ToString(),
                            Color = SKColor.Parse("#2c3e50") // You can change the color based on your preference
                        });
                    }

                    return chartEntries.ToArray();
                }
            }
        }
    }
}

    