using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class UserCalendar
    {
        [Key]
        public int UserCalendarId { get; set; }
        public int UserId { get; set; }
        public int CalendarId { get; set; }

        public User User { get; set; }
        public Calendar Calendar { get; set; }
    }
}