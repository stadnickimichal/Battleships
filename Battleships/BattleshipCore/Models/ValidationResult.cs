using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Models
{
    public class ValidationResult
    {
        public ValidationResult(bool isValid)
        {
            IsValid = isValid;
        }
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
    }
}
