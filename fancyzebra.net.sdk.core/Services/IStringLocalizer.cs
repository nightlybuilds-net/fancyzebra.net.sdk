namespace fancyzebra.net.sdk.core.Services
{
    public interface IStringLocalizer
    {
        public string Accept { get; }
        public string Request { get; }
        public string MandatoryClausesMissingMessage { get; }
        public string SuccessMessage { get; }
        string NoConnectionMessage { get; set; }
        string GenericError { get; set; }
        string Error { get; set; }
        string Ok { get; set; }
    }
}