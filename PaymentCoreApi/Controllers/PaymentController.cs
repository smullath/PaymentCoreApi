using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentCoreApi.Model;
using PaymentCoreApi.Services;

namespace PaymentCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICosmosDbServices _cosmosDbService;
        public PaymentController(ICosmosDbServices cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<payment>>> GetPaymentItems()
        {
            var empget = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            return Ok(empget);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<payment>> GetPaymentItem(string id)
        {
            var paymentItem = await _cosmosDbService.GetItemAsync(id);

            if (paymentItem== null)
            {
                return NotFound();
            }

            return paymentItem;
        }

        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentItem(string id, payment paymentItem)
        {
            if (id != paymentItem.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(id, paymentItem);
            return Ok();
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<payment>> PostPaymentItem(payment paymentItem)
        {
            paymentItem.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(paymentItem);
            return CreatedAtAction(nameof(GetPaymentItem), new { id = paymentItem.Id }, paymentItem);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentItem(string id)
        {           
            var paymentItem = await _cosmosDbService.GetItemAsync(id);
            if (paymentItem == null)
            {
                return NotFound();
            }

            await _cosmosDbService.DeleteItemAsync(id);
            return Ok();
        }
    }
}


    
