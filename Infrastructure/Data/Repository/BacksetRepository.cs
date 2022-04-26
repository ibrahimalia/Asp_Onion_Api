using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.IRepository;
using StackExchange.Redis;

namespace Infrastructure.Data.Repository
{
    public class BacksetRepository : IBacksetRepository
    {
        private readonly IDatabase _database;
        public BacksetRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBacksetAsync(string backsetId)
        {
            return await _database.KeyDeleteAsync(backsetId);
        }

        public async Task<CustomerBackset> GetBacksetAsync(string backsetId)
        {
            var data = await _database.StringGetAsync(backsetId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBackset>(data);
        }

        public async Task<CustomerBackset> UpdateBacksetAsync(CustomerBackset customerBackset)
        {
            var create = await _database.StringSetAsync(customerBackset.Id, JsonSerializer.Serialize(customerBackset), TimeSpan.FromDays(30));
            if (!create)
            {
                return null;
            }
            return await GetBacksetAsync(customerBackset.Id);
        }
    }
}