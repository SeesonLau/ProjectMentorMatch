using System.Collections.ObjectModel;
using ProjectMentorMatch.ViewModels;
using UraniumUI.Material.Controls;
using ProjectMentorMatch.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;

namespace ProjectMentorMatch.Views;

public partial class Analytics : ContentPage
{
	Analytics analytics;
	public Analytics()
	{
		InitializeComponent();
		analytics = new Analytics();
	}

	public void CreateChart()
	{
		int profileID = App.ProfileID;
	}

}