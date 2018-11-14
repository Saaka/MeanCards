using System;

namespace MeanCards.Common.Helpers
{
    public class GuidEncoder
    {
        const string Plus = "+";
        const string Slash = "/";
        const string SlashPlaceholder = "_";
        const string PlusPlaceholder = "-";
        const string Suffix = "==";
        const int EncodedLengthWithouSuffix = 22;

        public string EncodeGuid(string guidString)
        {
            var guid = new Guid(guidString);
            return EncodeGuid(guid);
        }

        public string EncodeGuid(Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray())
                .Replace(Slash, SlashPlaceholder)
                .Replace(Plus, PlusPlaceholder)
                .Substring(0, EncodedLengthWithouSuffix);
        }

        public Guid DecodeGuid(string encoded)
        {
            encoded = encoded
                .Replace(SlashPlaceholder, Slash)
                .Replace(PlusPlaceholder, Plus);

            return new Guid(Convert.FromBase64String($"{encoded}{Suffix}"));
        }
    }
}
