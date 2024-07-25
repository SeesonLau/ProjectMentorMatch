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
	ChartEntry[] entries = new[]
	{
		new ChartEntry(212)
		{
			Label = "Windows",
			ValueLabel = "112",
		},
        new ChartEntry(248)
        {
            Label = "Android",
            ValueLabel = "648",
        },
        new ChartEntry(128)
        {
            Label = "iOS",
            ValueLabel = "428",
        },
    };

    Analytics analytics;
	public Analytics()
	{
		InitializeComponent(); 
		chartView.Chart = new BarChart
		{
			Entries = entries
		};
// 		analytics = new Analytics();
	}

	/*
	public void CreateChart()
	{
		int userID = App.UserID;
	}*/
}