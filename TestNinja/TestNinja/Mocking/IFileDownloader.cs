namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string destFileName);
    }
}