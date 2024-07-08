using NUnit.Framework.Internal;
using TestProject1.Steps;

namespace TestProject1.Tests;

public class ValidateGlobalSearch:BaseTest
{
    [Test]
    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void ValidateGlobalSearchTest( string keyWord)
    {
        var globalSearch = new GlobalSearch();
        logger.Log.Info("Test " + keyWord);
        globalSearch.Search(keyWord);
        var resultItems =  globalSearch.GetSearchResults(); 
        var itemsWithText = globalSearch.GetSearchResultsWithKeyWord(keyWord);
        Assert.That(resultItems.All(itemsWithText.Contains), $"Total search results: {resultItems.Count}, results containing keyword: {itemsWithText.Count}");
    } 

}