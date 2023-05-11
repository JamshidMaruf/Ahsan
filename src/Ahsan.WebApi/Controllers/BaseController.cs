using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace Ahsan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{ }
