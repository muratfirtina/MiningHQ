namespace Application.Storage;

public class Base64FileResult
{
    public string Base64 { get; set; }
    public string MimeType { get; set; }
    public long FileSize { get; set; }
}