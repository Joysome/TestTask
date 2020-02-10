using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Interfaces
{
    public interface IWordBreaker
    {
        string[] BreakWord(string word);
    }
}
