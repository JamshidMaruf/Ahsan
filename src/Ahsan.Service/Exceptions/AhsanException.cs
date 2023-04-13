namespace Ahsan.Service.Exceptions
{
    public class AhsanException : Exception
    {
        public int Code { get; set; }
        public AhsanException(int code, string message) 
            : base (message) 
        {
            this.Code = code;
        }
    }
}
