using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Reader
{
    interface IDictionaryReader
    {
        public Task<string[]> Read();
    }
}
