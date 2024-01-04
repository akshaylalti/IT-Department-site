using DeparmentAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DeparmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly UserContext _dbContext;
        private int _counter = 0;

        public FileUploadController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            string customUserId = $"2030{_counter:D1}";
            var file = Request.Form.Files[0];
            var fileName = Path.GetRandomFileName();
            var filePath = Path.Combine("C:\\Users\\aksha\\Pictures\\database", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileEntity = new FileModel
            {
                Id = customUserId,
                FileName = fileName,
                FilePath = filePath,
                UploadDate = DateTime.Now
            };
            _counter++;
            _dbContext.Files.Add(fileEntity);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                message = "File uploaded successfully",
                fileEntity.Id,
                fileEntity.FileName,
                fileEntity.FilePath,
                fileEntity.UploadDate
            });
        }

        [HttpGet("getAllImages")]
        public async Task<IActionResult> GetAllImages()
        {
            var files = await _dbContext.Files.ToListAsync();

            // You can project the data as needed, e.g., select specific properties
            var imageInfoList = files.Select(file => new
            {
                file.Id,
                file.FileName,
                file.FilePath,
                file.UploadDate
            });

            return Ok(imageInfoList);
        }

        [HttpGet("getImage/{id}")]
        public IActionResult GetImage(int id)
        {
            var file = _dbContext.Files.Find(id.ToString());

            if (file == null)
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(file.FilePath);
            return File(imageBytes, "image/jpeg"); // Adjust the content type based on your image type
        }
    }
}
