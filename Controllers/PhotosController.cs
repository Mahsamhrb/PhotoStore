using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoStore.Application.Features.Photos.Commands.ArchivePhoto;
using PhotoStore.Application.Features.Photos.Commands.DeletePhoto;
using PhotoStore.Application.Features.Photos.Commands.UpdatePhoto;
using PhotoStore.Application.Features.Photos.Commands.UploadPhoto;
using PhotoStore.Application.Features.Photos.Queries.GetAllPhotos;
using PhotoStore.Application.Features.Photos.Queries.GetPhotoById;
using PhotoStore.Controllers.Requests;

namespace PhotoStore.Controllers;

[ApiController]
[Route("api/photos")]
public class PhotosController : ControllerBase
{
    private readonly  IMediator _mediator;

    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPhotosQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPhotoByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]UploadPhotoCommand command)
    {

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new {id = result.Id},
            result);
    }

    [HttpPut("{id:guid}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        await _mediator.Send(
            new ArchivePhotoCommand(id));

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdatePhotoRequest request)
    {
       var command = new UpdatePhotoCommand(
        id,
        request.Title,
        request.Price,
        request.File
    );
        
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeletePhotoCommand(id));
        
        return NoContent();
    }
}