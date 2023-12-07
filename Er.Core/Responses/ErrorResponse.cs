namespace Er.Core.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; private set; }
    }

    public class Error
    {
        public Error(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; private set; }
        public string Message { get; private set; }
    }
}
