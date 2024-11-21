using CatalogApi.Contracts.Dto.WebEntities.Catalog;
using CatalogApi.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Web.Controllers;

[ApiController]
[Route("api/catalogs")]
public class CatalogController(
    ICatalogService catalogService
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CatalogResponseDto>> GetAllCatalogs()
    {
        var catalogResponseDtos = await catalogService.GetAllAsync();
        return Ok(catalogResponseDtos);
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<CatalogResponseDto>> GetCatalog(long id)
    {
        var catalogResponseDto = await catalogService.GetByIdAsync(id);
        return Ok(catalogResponseDto);
    }
    
    [HttpGet]
    [Route("{id:long}/children")]
    public async Task<ActionResult<CatalogResponseDto>> GetChildren(long id)
    {
        var catalogResponseDtos = await catalogService.GetChildrenAsync(id);
        return Ok(catalogResponseDtos);
    }

    [HttpPost]
    public async Task<ActionResult<CatalogResponseDto>> CreateCatalog(
        [FromBody] CreateCatalogRequestDto catalogRequestDto)
    {
        var catalogResponseDto = await catalogService.AddAsync(catalogRequestDto);
        return Created(nameof(CreateCatalog), catalogResponseDto);
    }

    [HttpPatch]
    [Route("{id:long}")]
    public async Task<ActionResult<CatalogResponseDto>> UpdateCatalog(
        long id, [FromBody] UpdateCatalogRequestDto catalogRequestDto)
    {
        var catalogResponseDto = await catalogService.UpdateAsync(id, catalogRequestDto);
        return Ok(catalogResponseDto);
    }

    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult> DeleteCatalog(long id)
    {
        await catalogService.DeleteAsync(id);
        return NoContent();
    }
}