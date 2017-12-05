using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InterfaceMVC.Models
{
    public class EventLog
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string DataBefore { get; set; }
        public string DataAfter { get; set; }
    }
}