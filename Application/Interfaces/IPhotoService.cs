using PhotoStore.Application.DTOs.Photos;

namespace PhotoStore.Application.Interfaces;
public interface IPhotoService
{
    Task<List<PhotoDto>> GetAll();
    Task<PhotoDto?> GetById(int id);
    Task<PhotoDto> Upload(UploadPhotoDto dto);
    Task<bool> Delete(int id);
    Task<PhotoDto?> Update(int id, UpdatePhotoDto dto);
}