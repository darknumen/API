using BoDi;
using TechTalk.SpecFlow;


namespace SimpleFramework.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected IObjectContainer objectContainer;
        protected readonly ScenarioContext scenarioContext;
        protected readonly FeatureContext featureContext;


        protected BaseSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.objectContainer = objectContainer;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }
    }
}
