using BattleshipCore.Interfaces;
using BattleshipCore.Lookups;
using BattleshipCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Test.Utils
{
    public class UserInputValidatorTest
    {
        private UserInputValidator _validator;
        public UserInputValidatorTest()
        {
            _validator = new UserInputValidator();
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("A10")]
        [InlineData("j1")]
        [InlineData("j10")]
        [InlineData("f5")]
        public void Validate_CorrectInput_Success(string input)
        {
            var output = _validator.Validate(input);
            Assert.True(output.IsValid);
        }

        [Theory]
        [InlineData("Z1")]
        [InlineData("Z10")]
        [InlineData("a11")]
        [InlineData("a0")]
        public void Validate_CorrectButOutOfRangeInput_Failure(string input)
        {
            List<char> allowedCharacters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            var allowed = string.Join(", ", allowedCharacters.ToArray());
            var output = _validator.Validate(input);
            Assert.True(!output.IsValid);
            Assert.True(UserInputValidationMessagesLookup.RowRangeValidatorErrorMessage == output.ValidationMessage ||
                string.Format(UserInputValidationMessagesLookup.ColumnRangeValidatorErrorMessage, allowed) == output.ValidationMessage);
        }

        [Theory]
        [InlineData("bbb")]
        [InlineData("aaa")]
        public void Validate_IncorrectInput_Failure(string input)
        {
            var output = _validator.Validate(input);
            Assert.True(!output.IsValid);
            Assert.Equal(UserInputValidationMessagesLookup.ErrorTryingToProcesInput, output.ValidationMessage);
        }

        [Fact]
        public void Validate_NullInput_Failure()
        {
            var output = _validator.Validate(null);
            Assert.True(!output.IsValid);
            Assert.Equal(UserInputValidationMessagesLookup.NullOrEmptyValidatorErrorMessage, output.ValidationMessage);
        }

        [Fact]
        public void Validate_EmptyInput_Failure()
        {
            var output = _validator.Validate("");
            Assert.True(!output.IsValid);
            Assert.Equal(UserInputValidationMessagesLookup.NullOrEmptyValidatorErrorMessage, output.ValidationMessage);
        }
    }
}
