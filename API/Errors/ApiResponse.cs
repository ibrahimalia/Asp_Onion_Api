using System.Collections;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, IEnumerable data = null, string message = null)
        {
            this.statusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
            Data = data ?? new ArrayList();
        }

        public int statusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable Data { get; set; }


        public string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                201 => "add success",
                200 => "success",
                400 => "A bad Request , you have made",
                401 => "Authorized , you are not ",
                404 => "Resourse found , it was  not ",
                500 => "Errors lead to anger and anger lead to hate",
                _ => null
            };
        }

    }
}