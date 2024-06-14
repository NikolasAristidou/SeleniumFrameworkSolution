namespace SeleniumFramework.Core.Pages
{
    public interface IPage
    {
        string Url { get; }
        void GoToUrl();
    }
}
