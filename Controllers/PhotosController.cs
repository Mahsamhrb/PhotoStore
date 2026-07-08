using Microsoft.AspNetCore.Mvc;
using PhotoStore.Application.DTOs.Photos;
using PhotoStore.Application.Interfaces;

namespace PhotoStore.Controllers;

[ApiController]
[Route("api/photos")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetAll()
    {
        var result = await _photoService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PhotoDto>> GetById(int id)
    {
        var result = await _photoService.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PhotoDto>> Upload([FromForm]UploadPhotoDto dto)
    {

        var result = await _photoService.Upload(dto);

        return CreatedAtAction(
            nameof(GetById),
            new {id = result.Id},
            result);
    }

    [HttpPut("{id:int}/archive")]
    public async Task<IActionResult> Archive(int id)
    {
        await _photoService.Archive(id);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PhotoDto>> Update(int id, [FromForm] UpdatePhotoDto dto)
    {
        var result = await _photoService.Update(id, dto);

        return Ok(result);
    }
}