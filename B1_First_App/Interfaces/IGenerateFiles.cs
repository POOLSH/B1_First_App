using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_First_App.Interfaces
{
    public interface IGenerateFiles
    {
        string GenerateRandomString(Random random, int length, string symbs);
        string GenerateRandomDate(Random random);
        void Generate(Random random);

    }
}
