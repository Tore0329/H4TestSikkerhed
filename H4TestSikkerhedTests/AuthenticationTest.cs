using Bunit;
using Bunit.TestDoubles;
using H4TestSikkerhedApp.Components.Pages;

namespace H4TestSikkerhedTests
{
    public class AuthenticationTest
    {
        [Fact]
        public void AuthenticationView()
        {
            //Arange
            var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("");

            //Act
            var cut = ctx.RenderComponent<Home>();

            //Assert
            cut.MarkupMatches("<h1>Hello there [^_^]</h1>\r\n<h2>You are NOT admin</h2>\r\n<br>");
        }

        [Fact]
        public void UnauthenticationView()
        {
            //Arange
            var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();

            //Act
            var cut = ctx.RenderComponent<Home>();

            //Assert
            cut.MarkupMatches("<div>\r\n\t<h1>You must log in to see the content.</h1>\r\n</div>\r\n<br>");
        }
    }
}
