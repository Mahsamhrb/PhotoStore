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

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PhotoDto>> Upload([FromForm]UploadPhotoDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _photoService.Upload(dto);

        return CreatedAtAction(
            nameof(GetById),
            new {id = result.Id},
            result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _photoService.Delete(id);

        if(!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PhotoDto>> Update(int id, [FromForm] UpdatePhotoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _photoService.Update(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
}
}