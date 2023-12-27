namespace SchoolManagement.Storage;

public class File
{
    public string FileName { get; }

    public Stream Content { get; }

    public File(string fileName, Stream content)
    {
        FileName = fileName;
        Content = content;
    }
}
