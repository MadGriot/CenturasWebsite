namespace centuras.org.Services
{
    public interface IFileService
    {
        void DeleteFile(string fileName, string directory);
        Task<string> SaveFile(IFormFile file, string directory, string[] allowedExtensions);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFile(IFormFile file, string directory,
            string[] allowedExtensions)
        {
            string wwwPath = webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwPath, directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join
                    (",", allowedExtensions)} extensions are allowed");
            }
            string newFileName = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(path, newFileName);

            using var fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return newFileName;
        }

        public void DeleteFile(string fileName, string directory)
        {
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, directory,
                fileName);
            if (!Path.Exists(fullPath))
            {
                throw new FileNotFoundException($"File {fileName} does not exists");
            }
            File.Delete(fullPath);
        }
    }
}
