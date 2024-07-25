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
        
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int userId = App.UserID;
        await CreateLineChart(userId);
    }
    public async Task CreateLineChart(int userId)
    {
        List<ChartEntry> entries = new List<ChartEntry>();
        int profileId;

        using (var connection = Database.GetConnection())
        {
            await connection.OpenAsync();

            // Get the ProfileID from the Profile table
            string profileIdQuery = "SELECT ProfileID FROM Profile WHERE UserID = @UserID";
            using (SqlCommand profileIdCommand = new SqlCommand(profileIdQuery, connection))
            {
                profileIdCommand.Parameters.AddWithValue("@UserID", userId);
                object result = await profileIdCommand.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out profileId))
                {
                    // ProfileID was found, now get data from the Analytics table
                    string analyticsQuery = "SELECT day, brainReact FROM Analytics WHERE ProfileID = @ProfileID";
                    using (SqlCommand analyticsCommand = new SqlCommand(analyticsQuery, connection))
                    {
                        analyticsCommand.Parameters.AddWithValue("@ProfileID", profileId);
                        SqlDataReader reader = await analyticsCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var day = reader.GetDateTime(0);
                            var brainReact = reader.GetInt32(1);

                            entries.Add(new ChartEntry(brainReact)
                            {
                                Label = day.ToString("MM/dd/yyyy"),
                                ValueLabel = brainReact.ToString(),
                                Color = SKColor.Parse("#3498db")
                            });
                        }

                        reader.Close();
                    }
                }
                else
                {
                    entries.Add(new ChartEntry(0)
                    {
                        Label = "No data available",
                        ValueLabel = "0",
                        Color = SKColor.Parse("#e74c3c")
                    });
                }
            }
        }

        chartView.Chart = new LineChart()
        {
            Entries = entries,
            BackgroundColor = SKColors.White,
            LineMode = LineMode.Straight,
            LineSize = 8,
            PointMode = PointMode.Circle,
            PointSize = 18,
        };
    }

}