namespace ECommerceApplication.Exceptions.Exceptions
{
    public class CustomException : Exception
    {
        public override string Message { get; }
        public string fieldName { get; set; }

        public CustomException(string message, string fieldName)
        {
            this.Message = message;
            this.fieldName = fieldName;
        }
    }
}
