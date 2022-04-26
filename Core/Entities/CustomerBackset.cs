using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBackset
    {
        public CustomerBackset()
        {
        }

        public CustomerBackset(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public ICollection<BacksetItem> Items { get; set; } = new List<BacksetItem>();
        
        
        
    }
}