namespace fancyzebra.net.sdk.core.Services
{
    public class StringLocalizer: IStringLocalizer
    {
        public string Accept { get; private set; }
        public string Request { get; private set; }
        public string MandatoryClausesMissingMessage { get; private set; }
        public string SuccessMessage { get; private set; }
        public string NoConnectionMessage { get; set; }
        public string GenericError { get; set; }
        public string Error { get; set; }
        public string Ok { get; set; }
        public string Mandatory { get; set; }

        public StringLocalizer()
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