namespace Snippet1
{
    #region Sample classes
    public class Validator
    {
        public bool ValidateUrl(string url)
        {
            return true;
        }

        public bool ValidateUser(string user)
        {
            return true;
        }

        public bool ValidateUrlPath(string urlPath)
        {
            return true;
        }

        public bool ValidatePort(int port)
        {
            return true;
        }
    }

    public class Config
    {
        public string User { get; set; }
        public string Url { get; set; }
        public string UrlPath { get; set; }
        public int Port { get; set; }
        public ExecutionOption Option { get; set; }
    }

    public enum ExecutionOption
    {
        AddNamespace
    }
    #endregion
}

