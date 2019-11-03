using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class Calendar
    {
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ICollection<UserCalendar> UserCalendar { get; set; }
        public ICollection<ImportantDate> ImportantDates { get; set; }
    }
}