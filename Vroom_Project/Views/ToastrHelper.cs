using Microsoft.AspNetCore.Mvc;

namespace Vroom_Project.Views.Test
{
    public static class ToastrHelper
    {
        public static void AddToastrMessage(this Controller controller, string type, string message, string title = "")
        {
            var toastrMessages = controller.TempData["ToastrMessages"] as List<ToastrMessage> ?? new List<ToastrMessage>();

            toastrMessages.Add(new ToastrMessage
            {
                Type = type,
                Message = message,
                Title = title
            });

            controller.TempData.Put("ToastrMessages", toastrMessages);

        }
    }
}
