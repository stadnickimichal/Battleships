using BattleshipCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Interfaces
{
    /// <summary>
    /// Interface for validation.
    /// </summary>
    /// <typeparam name="T">Validated object type</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validate object.
        /// </summary>
        /// <param name="userInput">Object to validate</param>
        /// <returns>Validation result</returns>
        ValidationResult Validate(T userInput);
    }
}
