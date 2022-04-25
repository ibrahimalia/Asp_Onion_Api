using System.Collections;

namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, IEnumerable data = null, string message = null,
        string details=null) 
        : base(statusCode, data, message)
        {
            Details=details;
        }
        public string Details { get; set; }
        
        
    }
}