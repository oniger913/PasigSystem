using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasigSystem.Shared
{
    public class PaypalTransactionModel
    {
        public string UserEmail { get; set; }
        public PaypalCheckoutApprovedDataModel Data { get; set; }
        public PaypalCheckoutApprovedDetailsModel Details { get; set; }
    }
}
