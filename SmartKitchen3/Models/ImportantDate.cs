using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class ImportantDate
    {
        [Key, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int CalendatId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationStartDateTime { get; set; }
        public string Image { get; set; }

        public Calendar Calendar { get; set; }

    }
}