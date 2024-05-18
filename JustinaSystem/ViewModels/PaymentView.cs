using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.ViewModels
{
    public class PaymentView
    {
        public int PaymentId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateTime { get; set; }
        public string MethodOfPayment { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Gst { get; set; }
        public string Notes { get; set; }
    }
}
