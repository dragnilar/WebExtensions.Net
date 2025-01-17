﻿using System.Threading.Tasks;

namespace WebExtensions.Net.Notifications
{
    public partial interface INotificationsApi
    {
        /// <summary>Updates an existing notification.</summary>
        /// <param name="notificationId">Identifier of the notification.</param>
        /// <param name="options">Contents of the notification.</param>
        /// <returns>Indicates whether a matching notification is updated.</returns>
        ValueTask<bool> Update(string notificationId, CreateNotificationOptions options);
    }
}
