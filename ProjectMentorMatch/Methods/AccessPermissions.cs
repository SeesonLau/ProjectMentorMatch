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

        public static async Task RequestStoragePermissionAsync()
        {
            var readPermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var writePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (readPermissionStatus != PermissionStatus.Granted)
            {
                readPermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (writePermissionStatus != PermissionStatus.Granted)
            {
                writePermissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
        }
    }
}
