using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.ViewModels
{
    public class DogView
    {
        public int DogId { get; set; }
        public int CustomerId { get; set; }
        public string DogName { get; set; }
        public string Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Vet { get; set; }
        public float Weight { get; set; }
        public string Color { get; set; }
        public string OkWithOtherDogs { get; set; }
        public string HasAllergies { get; set; }
        public string OnMedication { get; set; }
        public string TakeApik { get; set; }
        public string Owner { get; set; }
        public string Notes { get; set; }

    }
}
