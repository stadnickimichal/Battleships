using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Attributes
{
    /// <summary>
    /// Use this attribute to mark the IValidator<string> classes that are used to validate user input against some rule.
    /// </summary>
    internal class UserInputValidationRules: Attribute
    {
    }
}
