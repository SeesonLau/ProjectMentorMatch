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
using System.Collections.Generic;
using System.Linq;

namespace ProjectMentorMatch.Views
{

    public partial class Analytics : ContentPage
    {
        public Analytics()
        {
            InitializeComponent();
            LoadRankings();
            try
            {
                var profileIDs = Account.GetAnalyticsProfileID(App.ProfileID);
                if (!profileIDs.Any())
                {
                    chartView.IsVisible = false;
                    noDataMessage.IsVisible = true;
                }
                else
                {
                    var entries = new List<ChartEntry>();
                    entries.AddRange(GetChartEntries(App.ProfileID));
                    chartView.Chart = new LineChart()
                    {
                        Entries = entries,
                        BackgroundColor = SKColors.White,
                        LabelTextSize = 40,
                        PointMode = PointMode.Circle,
                        PointSize = 10
                    };
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                //  Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
        private void LoadRankings()
        {
            AnalyticsModel analyticsModel = new AnalyticsModel();
            List<ProfileRanking> rankings = analyticsModel.GetProfileRankings();

            // Assign ranks
            for (int i = 0; i < rankings.Count; i++)
            {
                rankings[i].Rank = i + 1;
            }

            // Display only top 5 rankings
            rankingsCollectionView.ItemsSource = rankings.Take(5).ToList();
        }

        private IEnumerable<ChartEntry> GetChartEntries(int profileIDs)
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    var query = "SELECT day, brainReact FROM Analytics WHERE ProfileID = @ProfileID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProfileID", profileIDs);

                        using (var reader = command.ExecuteReader())
                        {
                            var chartEntries = new List<ChartEntry>();

                            while (reader.Read())
                            {
                                //var day = reader["day"].ToString();
                                var day = Convert.ToDateTime(reader["day"]).ToString("yyyy-MM-dd");
                                var brainReact = Convert.ToSingle(reader["brainReact"]);

                                chartEntries.Add(new ChartEntry(brainReact)
                                {
                                    Label = day,
                                    ValueLabel = brainReact.ToString(),
                                    Color = SKColor.Parse("#2c3e50") // You can change the color based on your preference
                                });
                            }

                            return chartEntries;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while fetching chart entries: {ex.Message}", "OK");
         //       Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return Enumerable.Empty<ChartEntry>();
            }
        }
    }
}