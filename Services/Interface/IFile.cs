namespace ChodoidoUTE.Services.Interface
{
    public interface IFile
    {
        public Task<string> SaveFile(IFormFile formFile, string FolderName);
        public Task<bool> DeleteFile(string filePath);
    }
}
