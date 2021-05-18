namespace Mews.Fiscalization.Uniwix.Communication.Dto
{
    internal class Response<TResponse>
    {
        public int Code { get; set; }

        public TResponse Result { get; set; }
    }
}