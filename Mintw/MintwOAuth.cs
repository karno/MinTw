using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Std.Tweak;
using Std.Tweak.CredentialProviders;

namespace Mintw
{
    public class MintwOAuth :  OAuth
    {
        protected override string ConsumerKey
        {
            get { return "bEoObs31tR6N81uYADKA"; }
        }

        protected override string ConsumerSecret
        {
            get { return "4c68Fj9gMTZbNxc1DbuPhSUh4Ek2udvo82pZBTxSoh8"; }
        }
    }
}
