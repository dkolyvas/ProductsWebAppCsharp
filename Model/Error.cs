namespace ProductsDBApp.Model
{
    public class Error
    {
        public Error(string code, string message, string field)
        {
            Code = code;
            Message = message;
            Field = field;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }

        public override string? ToString()
        {
            return $"Error: {Code} {Message}, {Field}";
        }
    }
}
