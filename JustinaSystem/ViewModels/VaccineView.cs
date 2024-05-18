using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.ViewModels
{
    public class VaccineView
    {
        public int VaccineId { get; set; }
        public int DogId { get; set; }
        public string VaccineName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}
