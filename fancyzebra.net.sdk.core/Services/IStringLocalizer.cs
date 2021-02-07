namespace fancyzebra.net.sdk.core.Services
{
    public interface IStringLocalizer
    {
        public string Accept { get; }
        public string Request { get; }
        public string MandatoryClausesMissingMessage { get; }
        public string SuccessMessage { get; }
        string NoConnectionMessage { get; }
        string GenericError { get; }
        string Error { get;}
        string Ok { get;}
        string Mandatory { get;}
    }
}