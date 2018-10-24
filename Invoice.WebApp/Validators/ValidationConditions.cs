using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Validators
{
    public class ValidationConditions
    {
        public static bool IsValidType<T>(T variable)
        {
            return !variable.Equals(default(T));
        }
    }
}
