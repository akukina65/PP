// Controllers/UploadController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UploadController> _logger;

        public UploadController(
            IWebHostEnvironment env,
            ILogger<UploadController> logger)
        {
            _env = env;
            _logger = logger;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                if (!IsImageFile(file.FileName))
                    return BadRequest("Only image files are allowed");

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/images/{uniqueFileName}";
                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("video")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                if (!IsVideoFile(file.FileName))
                    return BadRequest("Only video files are allowed");

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "videos");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/videos/{uniqueFileName}";
                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading video");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool IsImageFile(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }

        private bool IsVideoFile(string fileName)
        {
            var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv", ".webm" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }
    }
}