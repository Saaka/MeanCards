using MeanCards.Common.Helpers;
using System;
using Xunit;

namespace MeanCards.Tests.Unit.CommonTests
{
    public class GuidEncoderShould
    {
        [Fact]
        public void EncodeValidGuid()
        {
            var guid = new Guid("3dd28bf2-7e4b-48fe-b976-2c858a65a404");
            var encoder = new GuidEncoder();

            var encoded = encoder.EncodeGuid(guid);

            Assert.Equal("8ovSPUt-_ki5diyFimWkBA", encoded);
        }

        [Fact]
        public void DecodeValidString()
        {
            var encoded = "8ovSPUt-_ki5diyFimWkBA";
            var encoder = new GuidEncoder();

            var guid = encoder.DecodeGuid(encoded);

            Assert.Equal("3dd28bf2-7e4b-48fe-b976-2c858a65a404", guid.ToString());
        }
    }
}
