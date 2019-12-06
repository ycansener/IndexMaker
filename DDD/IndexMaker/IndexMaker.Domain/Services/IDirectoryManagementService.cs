using IndexMaker.Domain.Entities;

namespace IndexMaker.Domain.Services
{
    public interface IDirectoryManagementService
    {
        FolderModel Investigate(FolderModel rootFolder);
        string GetName(string completePath);
    }
}
