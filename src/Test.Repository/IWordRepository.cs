using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain;

namespace Test.Repository
{
    public interface IWordRepository
    {
        Task<Dictionary<Language, string[]>> GetWords();
    }
}
