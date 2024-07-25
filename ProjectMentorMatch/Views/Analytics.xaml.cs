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
        int userID = App.UserID;
        await CreateLineChart(userID);
    }

    //Chart not yet working, 
    public static int GetProfileIDByUserID(int userID)
    {
        string query = "SELECT ProfileID FROM Profile WHERE UserID = @UserID";
        using (var connection = Database.GetConnection())
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // Return -1 if no result
            }
        }
    }
    public async Task CreateLineChart(int userID)
    {

        int profileID = GetProfileIDByUserID(userID);

        if (profileID == -1)
        {
            // Handle the case where ProfileID is not found
            // Possibly log an error or show a message to the user
            return;
        }

        List<ChartEntry> entries = new List<ChartEntry>();

        using (var connection = Database.GetConnection())
        {
            await connection.OpenAsync();
            string query = "SELECT day, brainReact FROM Analytics WHERE ProfileID = @ProfileID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
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
                }
            }
        }

        // Step 3: Create and configure the LineChart
        chartView.Chart = new LineChart()
        {
            Entries = entries,
            BackgroundColor = SKColors.White,
            LineMode = LineMode.Straight,
            LineSize = 15,
            PointMode = PointMode.Circle,
            PointSize = 30
        };
    }
}