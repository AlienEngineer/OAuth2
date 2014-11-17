using System;

namespace Credential
{
    static class Helper
    {

        public static Uri Append(this Uri uri, String segment)
        {
            return new Uri(uri, segment);
        }

    }
}
