using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageLayer;

namespace LargePermutationsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.TestStorageLayer();
            p.GenerateLargePermutations();
        }

        private void GenerateLargePermutations()
        {
            IOutputBuffer buffer = new OutputBuffer(100000);
            char[] alphabetList = Enumerable.Range('A', 26).Select(x => (char) x).ToArray();
            string alphabet = null;
            foreach (char c in alphabetList)
            {
                alphabet += Convert.ToString(c);
            }
            string[] zeroToNineArr = Enumerable.Range(0, 10).Select(n => Convert.ToString(n)).ToArray();
            string zeroToNine = null;
            foreach (string numAsString in zeroToNineArr)
            {
                zeroToNine += numAsString;
            }
            string permutationContent = alphabet + zeroToNine; 
            PermutationGenerator pg = new PermutationGenerator(buffer, permutationContent, 64);
            pg.StartPermuting();
        }

        private void TestStorageLayer()
        {
            Core core = new Core();
            core.UpradeDatabase();
        }
    }
}
