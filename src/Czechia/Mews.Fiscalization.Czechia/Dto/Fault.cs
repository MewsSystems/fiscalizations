namespace Mews.Eet.Dto
{
    public class Fault
    {
        public Fault(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Message { get; }

        public int Code { get; }
    }
}
