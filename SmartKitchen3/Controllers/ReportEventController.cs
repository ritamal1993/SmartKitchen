using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.ViewModel;
using SmartKitchen3.Extensions;
using SmartKitchen3.Models;
using static SmartKitchen3.Models.RecipeEvent;
using static SmartKitchen3.Models.UserEvent;

namespace SmartKitchen3.Controllers
{
    public class ReportEventController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult RecipeAction(int EventType, int RecipeIdentifier)
        {
            if (EventType == 0)
            {
                var newEvent = new RecipeEvent {
                    Type = RecipeEventType.Click,
                    RecipeId = RecipeIdentifier,
                    Date = DateTime.Now
                };
                context.RecipeEvent.Add(newEvent);
                context.SaveChanges();

            }
            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult UserAction(int EventType, int UserIdentifier)
        {
            if (EventType == 0)
            {
                var newEvent = new UserEvent
                {
                    Type = UserEventType.Login,
                    UserId = UserIdentifier,
                    Date = DateTime.Now
                };
                context.UserEvent.Add(newEvent);
                context.SaveChanges();

            }
            else if (EventType == 1)
            {
                var newEvent = new UserEvent
                {
                    Type = UserEventType.Register,
                    UserId = UserIdentifier,
                    Date = DateTime.Now
                };
                context.UserEvent.Add(newEvent);
                context.SaveChanges();

            }
            return new EmptyResult();
        }
    }
}