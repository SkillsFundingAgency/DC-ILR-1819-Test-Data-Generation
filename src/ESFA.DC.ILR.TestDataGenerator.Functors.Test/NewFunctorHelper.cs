using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.TestDataGenerator.Functor
{
    public class TestHelpers
    {
        public static T CreateFunctor<T>() where T : new()
        {
            return new T();
        }
    }
}
