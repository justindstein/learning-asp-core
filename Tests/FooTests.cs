using Microsoft.AspNetCore.Authorization.Infrastructure;
using Xunit;

namespace learning_asp_core.Tests
{
    public class FooTests
    {
        [Fact]
        public void Foo()
        {
            // arange
            var foo = new List<String>();

            // act
            foo.Add("Foo");

            // assert
            Assert.Single(foo);
            Assert.Equal("Foo", foo.First());
        }
    }
}
