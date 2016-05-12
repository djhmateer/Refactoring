using System.Linq;
using Xunit;

namespace RefactoringLSCC
{
    public class PalindromeTester
    {
        public bool Test(string strInput)
        {
            string strTrimmed = strInput.Replace(" ", "");
            string strReversed = new string(strTrimmed.Reverse().ToArray());
            return strReversed.Equals(strReversed);
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
            var result = p.Check("hello");
            Assert.False(result);
        }

        [Fact]
        public void A_palindrome()
        {
            var p = new PalindromeTester();
            var result = p.Check("noon");
            Assert.True(result);
        }

        [Theory]
        [InlineData("hello world", false)]
        [InlineData("noon", true)]
        [InlineData("eva can i stab bats in a cave", true)]
        //Mr.Owl ate my metal worm
        //    Was it a car or a cat I saw?
        //    A nut for a jar of tuna
        //    Do geese see God?
        //    On a clover, if alive erupts a vast pure evil, a fire volcano
        //    Dammit, I'm mad!
        //    A Toyota's a Toyota
        //    Go hang a salami, I'm a lasagna hog
        //    A Santa lived as a devil at NASA
        public void Palindrome_sentences(string input, bool expected)
        {
            var p = new PalindromeTester();
            var result = p.IsPalindrome(input);
            Assert.Equal(expected, result);
        }
    }
}
