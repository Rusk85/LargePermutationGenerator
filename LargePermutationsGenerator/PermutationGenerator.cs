using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargePermutationsGenerator
{
    class PermutationGenerator
    {

        private IOutputBuffer _buffer;
        private string _permutationContent;
        private int _sizeOfPermutations;

        public PermutationGenerator(IOutputBuffer buffer, string permutationContent, int sizeOfPermutations)
        {
            _buffer = buffer;
            _permutationContent = permutationContent;
            _sizeOfPermutations = sizeOfPermutations;
        }

        public void StartPermuting()
        {
            GeneratePermutations();
        }

        private void GeneratePermutations()
        {
            char[] input = _permutationContent.ToCharArray();
            int lengthOfSinglePermutation = _sizeOfPermutations;

            int noOfUniqueValues = _permutationContent.Length;

            int[] indexes = new int[lengthOfSinglePermutation];
            double totalPermutations = Math.Pow(noOfUniqueValues, lengthOfSinglePermutation);

            for (double i = 0; i < totalPermutations; i++)
            {
                char[] tmpSinglePermutation = new char[lengthOfSinglePermutation];
                for (int j = 0; j < lengthOfSinglePermutation; j++)
                {
                    tmpSinglePermutation[j] = (input[indexes[j]]);
                }

                _buffer.AddToBuffer(tmpSinglePermutation);

                for (int j = 0; j < lengthOfSinglePermutation; j++)
                {
                    if (indexes[j] >= noOfUniqueValues - 1)
                    {
                        indexes[j] = 0;
                    }
                    else
                    {
                        indexes[j]++;
                        break;
                    }
                }
            }
        }

    }
}
