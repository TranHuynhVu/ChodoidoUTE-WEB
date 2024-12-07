using ChodoidoUTE.Services.Interface;

namespace ChodoidoUTE.Services
{
    public class ItemFileService : IFile
    {
        private readonly string _basePath;

        public ItemFileService(IHostEnvironment env)
        {
            
            _basePath = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(_basePath, filePath);

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    return true;
                }
                catch
                {
                    // Log the error if needed
                    return false;
                }
            }

            return false;
        }

        public async Task<string> SaveFile(IFormFile formFile, string folderName)
        {
            try
            {
                if (formFile.Length > 0)
                {
                    // Tạo đường dẫn thư mục
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Tạo tên file duy nhất
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    // Lưu file vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Trả về đường dẫn tương đối
                    return "/" + Path.Combine(folderName, fileName).Replace("\\", "/");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Lỗi khi lưu file: {ex.Message}");
            }
            return null;
        }

    }

}
