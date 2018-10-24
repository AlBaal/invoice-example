using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Validators
{
    public class ValidationMessages
    {
        public const string Required = "This field is required";
        public const string InvalidFormat = "Value is not in valid format";
        public const string MaxLength = "Max length of field is {MaxLength} characters. You've entered {TotalLength} characters";
        public const string NumberGreaterThen = "Value must be greater then {ComparisonValue}";
        public const string NumberLessThen = "Value must be smaller then {ComparisonValue}";

        public static string MustBe(string name)
        {
            return $"Field must be {name}.";
        }
    }
}
