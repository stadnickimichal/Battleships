using BattleshipCore.Attributes;
using BattleshipCore.Interfaces;
using BattleshipCore.Lookups;
using BattleshipCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Utils
{
    /// <summary>
    /// Base class for validator providing methodt for returning ValidationResults
    /// </summary>
    public abstract class BaseUserInputValidator : IValidator<string>
    {
        public abstract ValidationResult Validate(string userInput);
        protected ValidationResult Valid()
        {
            return new ValidationResult(true);
        }

        protected ValidationResult InValid(string userInput)
        {
            return new ValidationResult(false)
            {
                ValidationMessage = userInput
            };
        }
    }

    /// <summary>
    /// Class validating user input
    /// </summary>
    public class UserInputValidator : BaseUserInputValidator
    {
        private List<IValidator<string>> _rules = new List<IValidator<string>>();
        public UserInputValidator()
        {
            var rules = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(UserInputValidationRules)));
            foreach (var rule in rules)
            {
                _rules.Add((IValidator<string>)Activator.CreateInstance(rule));
            }
        }

        public override ValidationResult Validate(string userInput)
        {
            foreach (var rule in _rules)
            {
                var output = rule.Validate(userInput);
                if (!output.IsValid)
                {
                    return output;
                }
            }
            return Valid();
        }

        [UserInputValidationRules]
        private class NullOrEmptyValidator : BaseUserInputValidator
        {
            public override ValidationResult Validate(string userInput)
            {
                if (string.IsNullOrEmpty(userInput))
                {
                    return InValid(UserInputValidationMessagesLookup.NullOrEmptyValidatorErrorMessage);
                }
                return Valid();
            }
        }

        [UserInputValidationRules]
        private class LengthValidator : BaseUserInputValidator
        {
            public override ValidationResult Validate(string userInput)
            {
                if (userInput.Length < 2)
                {
                    return InValid(UserInputValidationMessagesLookup.LengthValidatorErrorMessage);
                }
                return Valid();
            }
        }

        [UserInputValidationRules]
        private class ColumnRangeValidator : BaseUserInputValidator
        {
            private List<char> allowedCharacters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            public override ValidationResult Validate(string userInput)
            {
                var columnString = userInput.Substring(0, 1);
                char columnChar;
                if (!char.TryParse(columnString.ToUpper(), out columnChar))
                {
                    return InValid(UserInputValidationMessagesLookup.ErrorTryingToProcesInput);
                }

                if (!allowedCharacters.Contains(columnChar))
                {
                    return InValid(string.Format(UserInputValidationMessagesLookup.ColumnRangeValidatorErrorMessage, string.Join(", ", allowedCharacters.ToArray())));
                }
                return Valid();
            }
        }

        [UserInputValidationRules]
        private class RowRangeValidator : BaseUserInputValidator
        {
            public override ValidationResult Validate(string userInput)
            {
                var rowNumber = userInput.Substring(1);
                int rowIndex;
                if (!int.TryParse(rowNumber, out rowIndex))
                {
                    return InValid(UserInputValidationMessagesLookup.ErrorTryingToProcesInput);
                }

                if (rowIndex < 1 || rowIndex > 10)
                {
                    return InValid(UserInputValidationMessagesLookup.RowRangeValidatorErrorMessage);
                }
                return Valid();
            }
        }
    }
}
