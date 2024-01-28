using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Core.Statics
{
    public class ValidationResult
    {
        public string Message { get; set; }
        public bool IsValid { get; set; }

        public ValidationResult(bool isValid, string message)
        {
            Message = message;
            IsValid = isValid;
        }

        public static ValidationResult Success(string message)
        {
            return new ValidationResult(true, message);
        }

        public static ValidationResult Fail(string message)
        {
            return new ValidationResult(false, message);
        }

    }
}
