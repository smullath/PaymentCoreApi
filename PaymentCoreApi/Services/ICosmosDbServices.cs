using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentCoreApi.Model;

namespace PaymentCoreApi.Services
{
    public interface ICosmosDbServices
    {
        Task<IEnumerable<payment>> GetItemsAsync(string query);
        Task<payment> GetItemAsync(string id);
        Task AddItemAsync(payment item);
        Task UpdateItemAsync(string id, payment item);
        Task DeleteItemAsync(string id);
    }
}
