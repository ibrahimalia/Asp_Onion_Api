using System.Collections.Generic;

namespace Infrastructure.Data.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public ICollection<string> Roles { get; set; }

        
        
    }
}