namespace IndexMaker.Domain.Entities
{
    public interface IDirectoryItem
    {
        string Name { get; }
        string CompletePath { get; }
        FolderModel ParentFolder { get; }

    }
}
