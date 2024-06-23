using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages;

public class CarriersPage:BasicPage
{
    public CarriersPage(IWebDriver driver) : base(driver) {}
    public IWebElement? PositionSearch(string keyWord, string country)
    {
        var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };
        
        var fieldKeywords = elementlWait.Until(driver => driver.FindElement(By.Id("new_form_job_search-keyword")));

        //I have to remove the banner first
        var acceptCookies = elementlWait.Until(driver => driver.FindElement(By.Id("onetrust-accept-btn-handler")));
        acceptCookies.Click();

        var clickAndSendKeysWord = new Actions(driver);
        clickAndSendKeysWord
            .Pause(TimeSpan.FromSeconds(2))
            .Click(fieldKeywords)
            .SendKeys(keyWord)
            .Perform();

        var locationFild = driver.FindElement(By.ClassName("recruiting-search__location"));
        locationFild.Click();

        if (country == "All Locations")
        {
            var location = elementlWait.Until(driver => driver.FindElement(By.CssSelector($"[title='{country}'][role='option']")));
            elementlWait.Until(driver => location.Displayed); 
            location.Click();
        }
        else
        {
            var countryInput = driver.FindElement(By.CssSelector($"[aria-label = '{country}']"));
            elementlWait.Until(driver => countryInput.Displayed); //If the country's drop down has already been opened, it will close (as for Spain), this case must be handled separately
            countryInput.Click();
            var cityInput = driver.FindElement(By.CssSelector($"[title= 'All Cities in {country}'][role='option']"));
            elementlWait.Until(driver => cityInput.Displayed);
            cityInput.Click();
        }

        var remote = driver.FindElement(By.XPath("//input[@name = 'remote']/.. "));
        remote.Click();
        var findButton = driver.FindElement(By.CssSelector("button[type = 'submit']"));
        findButton.Click();
        var result = driver.FindElement(By.CssSelector(".search-result__list"));
        elementlWait.Until(driver => result.Displayed);
        return result;
    }
}