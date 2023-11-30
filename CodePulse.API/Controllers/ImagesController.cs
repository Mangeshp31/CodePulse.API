using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewPoint.API.Models.Domain;
using ViewPoint.API.Models.DTO;
using ViewPoint.API.Repositories.Interface;

namespace ViewPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //post action method : {apibaseurl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if(ModelState.IsValid)
            {
                //File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };
                blogImage = await imageRepository.Upload(file, blogImage);

                // Convert Domain Model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url
                };

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        //for validations
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file formate");
            }
            //check the size of the image - 10mb= number
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size should be less than 10 MB");
            }
            

        }


        //Get method for image
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            //call image repository to get all images
            var images = await imageRepository.GetAll();

            //convert domain model to dto
            var response = new List<BlogImageDto>();
            foreach(var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url

                });
            }
            return Ok(response);

        }
    }
}
