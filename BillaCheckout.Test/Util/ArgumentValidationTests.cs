using BillaCheckout.Core.Util;
using System;
using Xunit;

namespace BillaCheckout.Test.Util
{
    public class ArgumentValidationTests
    {
        [Fact(DisplayName = "Check if not null argument succeeds")]
        public void CheckEnsureNotNullFlow()
        {
            var obj = new Object();
            var res = ArgumentValidation.EnsureNotNull(obj, nameof(obj));
            Assert.Equal(obj, res);
        }

        [Fact(DisplayName = "Check if null argument throws")]
        public void CheckEnsureNotNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ArgumentValidation.EnsureNotNull<Object>(null, "Test");
            });
        }
    }
}
