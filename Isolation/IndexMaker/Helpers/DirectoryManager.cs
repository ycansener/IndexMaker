using IndexMaker.Models;
using System.IO;
using System.Linq;

namespace IndexMaker.Helpers
{
    public class DirectoryManager
    {
        public FolderModel _rootItem { get; }

        public DirectoryManager(FolderModel rootItem)
        {
            _rootItem = rootItem;
        }

        public void Investigate()
        {
            Investigate(_rootItem);
        }

        private void Investigate(FolderModel selectedFolder)
        {
            string path = selectedFolder.CompletePath;
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (var folder in folders)
            {
                string name = GetName(folder);

                FolderModel folderModel = new FolderModel(folder, folder, selectedFolder);
                Investigate(folderModel);
                selectedFolder.AddSubFolder(folderModel);
            }

            foreach (var file in files)
            {
                string name = GetName(file);

                FileModel fileModel = new FileModel(name, file, selectedFolder);
                selectedFolder.AddFile(fileModel);
            }
        }

        public static string GetName(string completePath)
        {
            string[] parts = completePath.Split('/');
            string name = completePath;

            if (parts != null && parts.Length > 0)
                name = parts.Last();

            return name;
        }


    }
}
