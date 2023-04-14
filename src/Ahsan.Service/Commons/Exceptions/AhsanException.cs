namespace Ahsan.Service.Commons.Exceptions
{
    public class AhsanException : Exception
    {
        public int Code { get; set; }
        public AhsanException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
