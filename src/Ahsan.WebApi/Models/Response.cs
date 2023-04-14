#pragma warning disable

namespace Ahsan.WebApi.Models;

public class Response
{
    public int Code { get; set; }
    public object Data { get; set; }
    public string Error { get; set; }
}
