namespace fancyzebra.net.sdk.core.Services
{
    public class EnglishStringLocalizer: IStringLocalizer
    {
        public string Accept { get; }
        public string Request { get; }
        public string MandatoryClausesMissingMessage { get; }
        public string SuccessMessage { get; }
        public string NoConnectionMessage { get; }
        public string GenericError { get; }
        public string Error { get; }
        public string Ok { get; }
        public string Mandatory { get;}

        public EnglishStringLocalizer()
        {
            this.Accept = "I accept";
            this.Request = "Accept privacy";
            this.MandatoryClausesMissingMessage = "All mandatory clauses must be accepted to continue";
            this.SuccessMessage = "Privacy updated with success";
            this.NoConnectionMessage = "Internet connection is required to continue";
            this.GenericError = "An Error has occured, please try later";
            this.Error = "Error";
            this.Ok = "Ok";
            this.Mandatory = "(Mandatory)";
        }
    }
}