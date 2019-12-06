using System.Linq;

namespace IndexMaker.Domain.Entities
{
    public class FileModel : IDirectoryItem
    {
        public string Name { get; }
        public string CompletePath { get; }
        public string Extention { get; }
        public FolderModel ParentFolder { get; }

        public FileModel(string name, string completePath, FolderModel parentFolder)
        {
            Name = name;
            CompletePath = completePath;
            ParentFolder = parentFolder;

            string[] parts = name.Split('.');

            if (parts != null && parts.Length > 0)
            {
                Extention = parts.Last();
            }
        }
    }
}
