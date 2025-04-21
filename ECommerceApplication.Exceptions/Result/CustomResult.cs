namespace ECommerceApplication.ExceptionsAndResults.Result
{
    public class CustomResult
    {
        public required bool IsSuccess { get; set; }
        public required string Message { get; set; }
        public object ResultDetails { get; set; } = new object();

        public CustomResult() { }

        public CustomResult(bool IsSuccess, string Message, object ResultDetails) 
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.ResultDetails = ResultDetails;
        }

        public CustomResult(bool IsSuccess, string Message)
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
        }
    }
}
