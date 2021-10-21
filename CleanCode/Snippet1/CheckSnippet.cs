namespace Snippet1
{
    public class CheckSnippet
    {
        private const char ApostrophesSign = '\'';
        private readonly string _user;
        private readonly string _url;
        private readonly string _urlPath;
        private readonly bool _isAddNamespaceReservationOption;
        private readonly Validator _validator;
        private readonly int _port;
        private readonly Config _conf;

        public CheckSnippet(string user, string url, string urlPath, bool isAddNamespaceReservationOption, Validator validator, int port, Config conf)
        {
            _user = user;
            _url = url;
            _urlPath = urlPath;
            _isAddNamespaceReservationOption = isAddNamespaceReservationOption;
            _validator = validator;
            _port = port;
            _conf = conf;
        }

        private static string TrimApostrophes(string path) => path.Trim(ApostrophesSign);

        private bool CheckAddNamespaceReservationParameters()
        {
            var isAtLeastOneNotEmpty = !string.IsNullOrEmpty(_user) || !string.IsNullOrEmpty(_url) || !string.IsNullOrEmpty(_urlPath);

            if (!isAtLeastOneNotEmpty)
                return false;

            var isValid = _validator.ValidateUrl(_url) &&
                          _validator.ValidateUser(_user) &&
                          _validator.ValidateUrlPath(_urlPath) &&
                          _validator.ValidatePort(_port);

            if (!isValid)
                return false;

            _conf.Option = ExecutionOption.AddNamespace;
            _conf.User = _user;
            _conf.Url = _url;
            _conf.Port = _port;
            _conf.UrlPath = TrimApostrophes(_urlPath);

            return true;
        }
    }
}
