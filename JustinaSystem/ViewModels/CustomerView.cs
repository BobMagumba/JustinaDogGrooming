using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.ViewModels
{
    public class CustomerView
    {
        public int CustomerId { get; set; }
        [Required (ErrorMessage = "Firts name is required")]
        public string FirstName { get; set; }
        [Required (ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Phone number is required")]
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enrollment date is required")]
        public DateTime EnrollmentDate { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required (ErrorMessage = "City is required")]
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Required (ErrorMessage = "Referral is required")]
        public string Referral { get; set; }
        public string Notes { get; set; }
        public int NumberOfDogs { get; set; }

    }
}
