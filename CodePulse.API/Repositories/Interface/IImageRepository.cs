using ViewPoint.API.Models.Domain;

namespace ViewPoint.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        //IEnurable because it is a list
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
