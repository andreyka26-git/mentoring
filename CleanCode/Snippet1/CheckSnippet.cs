namespace Snippet1
{
    public class CheckSnippet
    {
        private const char ApostrophesSign = '\'';
        private readonly string _mUser;
        private readonly string _mUrl;
        private readonly string _mUrlPath;
        private readonly bool _mIsAddNamespaceReservationOption;
        private readonly Validator _validator;
        private readonly int _mPort;
        private readonly Config _mConf;

        public CheckSnippet(string mUser, string mUrl, string mUrlPath, bool mIsAddNamespaceReservationOption, Validator validator, int mPort, Config mConf)
        {
            _mUser = mUser;
            _mUrl = mUrl;
            _mUrlPath = mUrlPath;
            _mIsAddNamespaceReservationOption = mIsAddNamespaceReservationOption;
            _validator = validator;
            _mPort = mPort;
            _mConf = mConf;
        }

        private static bool IsNullOrEmpty(string s) => s is {Length: > 0};

        private static string TrimApostrophes(string path) => path.Trim(ApostrophesSign);

        private bool CheckAddNamespaceReservationParameters()
        {
            var isUserParamSet = !IsNullOrEmpty(_mUser);
            var isUrlParamSet = !IsNullOrEmpty(_mUrl);
            var isUrlPathParamSet = !IsNullOrEmpty(_mUrlPath);

            if (!_mIsAddNamespaceReservationOption || !isUrlParamSet || !isUserParamSet || !isUrlPathParamSet)
                return false;

            var isValid = _validator.ValidateUrl(_mUrl) && _validator.ValidateUser(_mUser)
                                                        && _validator.ValidateUrlPath(_mUrlPath) && _validator.ValidatePort(_mPort);
            if (!isValid) return
                false;

            _mConf.Option = ExecutionOption.AddNamespace;
            _mConf.User = _mUser;
            _mConf.Url = _mUrl;
            _mConf.Port = _mPort;
            _mConf.UrlPath = TrimApostrophes(_mUrlPath);

            return true;
        }
    }
}
