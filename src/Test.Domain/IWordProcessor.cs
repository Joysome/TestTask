using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain
{
    public interface IWordProcessor
    {
        Task<string[]> ProcessWord(string word);
    }
}
