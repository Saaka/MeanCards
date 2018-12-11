using System.Linq;

namespace Xunit
{
    public class TestHelper
    {
        public static int GetNumberOfProperties<T>()
            where T : class
        {
            return typeof(T).GetProperties().Count();
        }

        public static void AssertNumberOfFields<T>(int numberOfFields)
            where T : class
        {
            Assert.Equal(numberOfFields, GetNumberOfProperties<T>());
        }
    }
}
