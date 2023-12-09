namespace Er.Models.ValueObjects
{
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
