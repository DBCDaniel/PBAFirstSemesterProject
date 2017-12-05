using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InterfaceMVC.Models
{
    public class ClearanceLevel
    {
        [Key]
        public int LevelID { get; set; }

        [Required(ErrorMessage = "Name is Required.")]
        public string Name { get; set; }
        
        public bool UserAdministration { get; set; }
        public bool ClearanceAdminstration { get; set; }
        public bool EventLog { get; set; }
    }
}