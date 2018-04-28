using System;
using Xunit;

namespace CoverScript.Simple.Tests
{
    public class MathHelperTests
    {
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -4)]
        public void BiggerA(int a, int b)
        {
            var result = MathHelper.GetBigger(a, b);
            Assert.Equal(a, result);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(0, 5)]
        [InlineData(-1, 0)]
        [InlineData(-4, -1)]
        public void BiggerB(int a, int b)
        {
            var result = MathHelper.GetBigger(a, b);
            Assert.Equal(b, result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void SameValues(int a, int b)
        {
            var result = MathHelper.GetBigger(a, b);
            Assert.Equal(a, result);
            Assert.Equal(b, result);
        }
    }
}
