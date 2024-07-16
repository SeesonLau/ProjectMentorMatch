﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Syncfusion.Maui.Core.Hosting;

namespace ProjectMentorMatch
{
    // To push code to github.
    // go to "Git" found on the top right corner.
    // press "Commit or Stash".
    // a tab named "Git Changes"" will pop up.
    // Enter a message, make sure ur message gives context kung unsa imong gi add/ammend/delete.
    // press "Commit All".
    // Then go to "Git" again and push.

    // To update ur code.
    // Go to "Git" then press sync.0


    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // Add Syncfusion.Maui.Core.Hosting
                .ConfigureSyncfusionCore()
                // Added Notification Functionality
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
