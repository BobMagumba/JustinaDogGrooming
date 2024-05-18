using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.ViewModels
{
    public class AppointmentView
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int DogId { get; set; }
        public int GroomerId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public DateTime Length { get; set; }
        public string Notes { get; set; }

    }
}
