using MeanCards.Common.Helpers;
using System;

namespace MeanCards.Common.RandomCodeProvider
{
    public class GuidRandomCodeGenerator : IRandomCodeGenerator
    {
        private readonly GuidEncoder guidEncoder;

        public GuidRandomCodeGenerator(GuidEncoder guidEncoder)
        {
            this.guidEncoder = guidEncoder;
        }

        public string Generate()
        {
            var code = guidEncoder.EncodeGuid(Guid.NewGuid());

            return code;
        }
    }
}
