using System;
using System.Globalization;
using fancyzebra.net.sdk.core.Services;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.forms
{
    public class FancyBuilder
    {
        public string AppId { get; private set; }
        public CultureInfo Culture { get; internal set; }
        public ViewDetails Details { get; private set; }
        public Application App { get; private set; }
        public IStringLocalizer StringLocalizer { get; private set; }
        public IPrivacyService PrivacyService { get; private set; }

        private FancyBuilder()
        {
            this.PrivacyService = new PrivacyService();
            this.StringLocalizer = new EnglishStringLocalizer();
        }

        /// <summary>
        /// Start build a new GracePrivacy
        /// </summary>
        /// <returns></returns>
        public static FancyBuilder New()
        {
            return new FancyBuilder();
        }
        

        #region Builder

        /// <summary>
        /// Add Fancy App Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FancyBuilder WithAppId(string id)
        {
            this.AppId = id;
            return this;
        }

        /// <summary>
        /// Override Culture request.
        /// Default value is CurrentCulture
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public FancyBuilder WithCulture(CultureInfo cultureInfo)
        {
            this.Culture = cultureInfo;
            return this;
        }

        /// <summary>
        /// Override View properties
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public FancyBuilder WithViewDetails(ViewDetails details)
        {
            this.Details = details;
            return this;
        }

        /// <summary>
        /// Use Application
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public FancyBuilder WithApp(Application app)
        {
            this.App = app;
            return this;
        }

        /// <summary>
        /// Define a custom string localizer
        /// </summary>
        /// <param name="stringLocalizer"></param>
        /// <returns></returns>
        public FancyBuilder WithIStringLocalizer(IStringLocalizer stringLocalizer)
        {
            this.StringLocalizer = stringLocalizer;
            return this;
        }

        public IFancyForms Build()
        {
            if (string.IsNullOrEmpty(this.AppId))
            {
                throw new Exception("Cannot build IFancyForms without an AppId");
            }

            return new FancyForms(this);
        }

        #endregion
    }
}