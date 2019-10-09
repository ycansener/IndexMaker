namespace IndexMaker.Models
{
    public interface IDirectoryItem
    {
        string Name { get; }
        string CompletePath { get; }
        FolderModel ParentFolder { get; }

    }
}
