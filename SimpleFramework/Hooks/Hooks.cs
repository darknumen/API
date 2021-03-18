using BoDi;
using SimpleFramework.Helpers;
using SimpleFramework.Models;
using System.Configuration;
using TechTalk.SpecFlow;


namespace SimpleFramework.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private readonly IObjectContainer objectContainer;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.objectContainer = objectContainer;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario("Token", Order = 0)]
        public void GetToken()
        {
            var settings = ConfigurationManager.AppSettings;
            var appSettings = new ApplicationSettings();

            RestHelper rest = new RestHelper();
            var token = rest.GetToken(settings["Url"], settings["Username"], settings["Password"]);

            scenarioContext.Set<string>(token, "AuthenticationToken");
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario()
        {
            //Get all appsettings
            var settings = ConfigurationManager.AppSettings;
            var appSettings = new ApplicationSettings();
            appSettings.Url = settings["Url"];

            //Get Token
            //RestHelper rest = new RestHelper();
            //appSettings.Token = rest.GetToken(settings["Url"], settings["Username"], settings["Password"]);

            objectContainer.RegisterInstanceAs<ApplicationSettings>(appSettings, "appsettings");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
