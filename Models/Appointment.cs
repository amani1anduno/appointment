using System;
using System.ComponentModel.DataAnnotations;
namespace appointment.Models
{
    public class Appointment
    {
        public int id { get; set; } 
        public string name { get; set; }    
        public string email { get; set; }
        public DateTime date { get; set; }
    }
}