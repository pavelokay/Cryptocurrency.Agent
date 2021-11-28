using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Infrastructure.Utils.JsonPolicies
{
    public static class StringUtils
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && (char.IsUpper(x) || char.IsDigit(x)) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }

}
