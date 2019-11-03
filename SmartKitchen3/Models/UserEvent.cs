using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class UserEvent
    {
        public enum UserEventType
        {
            Login, Register, Logout
        }

        public int UserEventId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public UserEventType Type { get; set; }

        public User User { get; set; }
    }
}