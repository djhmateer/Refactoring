using System.Linq;
using Xunit;
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace RefactoringLSCC
{
    public class PalindromeTester
    {
        public bool Test(string strInput)
        {
            string strTrimmed = strInput.Replace(" ", "");
            string strReversed = new string(strTrimmed.Reverse().ToArray());
            return strTrimmed.Equals(strReversed);
        }

        public bool Check(string input)
        {
            input = input.Replace(" ", "");
            var reversed = new string(input.Reverse().ToArray());
            return reversed.Equals(input);
        }

        public bool IsPalindrome(string input)
        {
            var forwards = input.Replace(" ", "");
            var backwards = new string(forwards.Reverse().ToArray());
            return backwards.Equals(forwards);
        }
    }

    public class Tests
    {
        [Fact]
        public void Not_a_palindrome()
        {
            var p = new PalindromeTester();
            var result = p.IsPalindrome("hello");
            Assert.False(result);
        }

        [Fact]
        public void A_palindrome()
        {
            var p = new PalindromeTester();
            var result = p.IsPalindrome("noon");
            Assert.True(result);
        }

        [Theory]
        [InlineData("hello world", false)]
        [InlineData("noon", true)]
        [InlineData("mr owl ate my metal worm", true)]
        [InlineData("do geese see god", true)]
        public void Palindrome_sentences(string input, bool expected)
        {
            var p = new PalindromeTester();
            var result = p.IsPalindrome(input);
            Assert.Equal(expected, result);
        }
    }
}
