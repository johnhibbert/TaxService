using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService.WebClients;

namespace TaxService.UnitTests
{
    [TestClass]
    public class WebClientTests
    {
        /*
         * Why just the constructor tests?  Why no GetRequest tests?
         * 
         * First off, the GetRequest and PostRequestmethod have a cyclomatic complexity of 1.  There are no ifs or loops, so there is little to test.
         * 
         * Secondly, HttpClient doesn't itself have an interface and cannot itself be properly mocked without something like a wrapper or factory
         * or something.  The WebClient is something of a wrapper, so that its consumers can be mocked properly, but that doesn't quite extend to
         * the class itself.
         * 
         * I could make a factory that makes up a new HTTP client each time or soemthing, but the docs say that it is 'intended to be instantiated 
         * once and re-used throughout the life of an application' so that might be sub-ideal as well.
         * https://docs.microsoft.com/en-gb/dotnet/api/system.net.http.httpclient?view=netframework-4.7.1
         * 
         * Perhaps this is my relatively casual education in unit tests showing here, but at this deep layer I am unsure how how to square this circle
         * between get greater code coverage / dependency inversion and utilizing the class as intended.  I'm inclined to feel okay about this code as
         * it is, since it's fairly simple, I would be very interested to learn more if this is incorrect or inadequate.
         * 
         * Also, while considering this and doing a little internet searching, I see that some people don't write tests for these so-called
         * "guard clauses" which is probably fair.
         * https://blog.ploeh.dk/2019/12/09/put-cyclomatic-complexity-to-good-use/
         */

        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ApiKeyNull_ShouldFail()
        {
            new WebClient(null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ApiKeyEmptyString_ShouldFail()
        {
            new WebClient(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ApiKeyWhitespace_ShouldFail()
        {
            new WebClient(" ");
        }

        #endregion 
    }
}
