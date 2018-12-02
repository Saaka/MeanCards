using System;
using System.Diagnostics;

namespace MeanCards.Common.Helpers
{
    public static class RandomFactory
    {
        public static Random Create()
        {
            return new Random(Process.GetCurrentProcess().Id ^ Environment.TickCount);
        }
    }
}
