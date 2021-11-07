using System;

namespace BillaCheckout.Core.Util
{
    public class ArgumentValidation
    {
        public static T EnsureNotNull<T>(T arg, string name)
        {
            if (arg == null)
                throw new ArgumentNullException(name);

            return arg;
        }
    }
}
