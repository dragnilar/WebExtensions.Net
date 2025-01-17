﻿using System.Threading.Tasks;

namespace WebExtensions.Net.Notifications
{
    public partial class NotificationsApi
    {
        /// <inheritdoc />
        public ValueTask<bool> Update(string notificationId, CreateNotificationOptions options)
        {
            return InvokeAsync<bool>("update", notificationId, options);
        }
    }
}
