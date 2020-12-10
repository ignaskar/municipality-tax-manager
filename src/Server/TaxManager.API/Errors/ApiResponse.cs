namespace TaxManager.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        
        public int StatusCode { get; }
        public string Message { get; }
        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request has been made.",
                401 => "You are not authorized to view this resource.",
                403 => "Access to this resource is forbidden.",
                404 => "Requested resource was not found.",
                500 => "We have encountered an issue on our side. Please try again later.",
                _ => null
            };
        }
        
    }
}