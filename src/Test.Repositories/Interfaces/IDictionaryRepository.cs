using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository.Interfaces
{
    interface IDictionaryRepository
    {
        Task<string[]> GetDictionaryWords();
    }
}
