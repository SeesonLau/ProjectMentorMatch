using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Methods
{
    //Add Your App Permissions Here (Very Important for Android 11 and above or else the features will not work)
    public class AccessPermissions
    {
        public static void RequestNotificationPermission()
        {

            // Notification Permission
            LocalNotificationCenter.Current.RequestNotificationPermission();
            LocalNotificationCenter.Current.AreNotificationsEnabled();

        }
    }
}
