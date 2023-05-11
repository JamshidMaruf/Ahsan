namespace Ahsan.Service.Exceptions;

public class CustomException : Exception
{
    public int Code { get; set; }
    public CustomException(int code, string message)
        : base(message)
    {
        this.Code = code;
    }
}
