using Abp;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Query.WebApi.Controllers
{
    public class HomeController: QueryControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        public HomeController(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }
    }
}
