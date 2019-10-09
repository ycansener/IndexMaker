using System.Collections.Generic;

namespace IndexMaker.Models
{
    public class FolderModel : IDirectoryItem
    {
        public string Name { get; }
        public string CompletePath { get; }
        public FolderModel ParentFolder { get; }

        private List<FolderModel> SubFolders;
        private List<FileModel> Files;

        public FolderModel(string name, string completePath, FolderModel parentFolder)
        {
            Name = name;
            CompletePath = completePath;
            ParentFolder = parentFolder;
            SubFolders = new List<FolderModel>();
            Files = new List<FileModel>();
        }

        public void AddSubFolder(FolderModel subFolder)
        {
            SubFolders.Add(subFolder);
        }

        public IEnumerable<FolderModel> GetSubFolders()
        {
            return SubFolders;
        }

        public void AddFile(FileModel fileModel)
        {
            Files.Add(fileModel);
        }

        public IEnumerable<FileModel> GetFiles()
        {
            return Files;
        }
    }
}
