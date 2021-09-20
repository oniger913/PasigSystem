using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasigSystem.Server.Data;
using PasigSystem.Server.Models;
using PasigSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasigSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalTransactionController : ControllerBase
    {
        private DataContext _dataContext;

        public PaypalTransactionController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post(PaypalTransactionModel paypalTransaction,CancellationToken cancellationToken)
        {
            await this._dataContext.PaypalTransaction.AddAsync(new Models.PaypalTransaction()
            {
                OrderAmount = Convert.ToDecimal(paypalTransaction.Details.purchase_units[0].amount.value),
                OrderId = paypalTransaction.Data.orderID,
                RowCreationDateTime = DateTimeOffset.UtcNow,
                UserEmail = paypalTransaction.UserEmail
            });
            await this._dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
