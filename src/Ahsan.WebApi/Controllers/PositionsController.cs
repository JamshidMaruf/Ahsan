using Ahsan.Domain.Configurations;
using Ahsan.Service.DTOs.Positions;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

#pragma warning disable
public class PositionsController : BaseController
{
    private readonly IPositionService positionService;
    public PositionsController(IPositionService positionService)
    {
        this.positionService = positionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostPositionAsync(PositionForCreationDto dto)
       => Ok(new
       {
           Code = 200,
           Error = "Success",
           Data = await this.positionService.CreateAsync(dto)
       });

    [HttpPut("update")]
    public async Task<IActionResult> PutPositionAsync(PositionForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.positionService.UpdateAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeletePositionAsync(long id)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.positionService.DeleteAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.positionService.GetByIdAsync(id)
        });

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllPositions(PaginationParams @params = null, string search = null)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.positionService.GetAllAsync(x => x.Id > 0,search, @params)
        });
}

