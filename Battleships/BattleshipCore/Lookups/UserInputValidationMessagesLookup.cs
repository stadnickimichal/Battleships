using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Lookups
{
    public static class UserInputValidationMessagesLookup
    {
        public const string NullOrEmptyValidatorErrorMessage = "Input is null or empty. Remember to type column letter and row numer with no delimiter and then hit enter.";
        public const string LengthValidatorErrorMessage = "Typed string is to short. Remember to type column letter and row numer with no delimiter and then hit enter.";
        public const string ColumnRangeValidatorErrorMessage = @"Bad column letter. Columns must be one of the following letters: {0}.";
        public const string RowRangeValidatorErrorMessage = "Bad row number. Row number must be number form 1 to 10.";
        public const string ErrorTryingToProcesInput = "Error trying to proces input.Try agine.";
    }
}
