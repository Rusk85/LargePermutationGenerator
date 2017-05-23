using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StorageLayer.Context;
using StorageLayer.Model;

using static StorageLayer.Context.PermutationContextFactory;


namespace LargePermutationsGenerator
{

    internal class DatabaseOutputBuffer : IOutputBuffer
    {

        private List<byte[]> _buffer;

        private readonly int _maxBufferSize;

        public DatabaseOutputBuffer(int maxBufferSize)
        {
            _maxBufferSize = maxBufferSize;
            _buffer = new List<byte[]>();
        }


        private byte[] ConvertToBytes(char[] permutation)
        {
            byte[] resultAsBytes = Encoding.ASCII.GetBytes(permutation);
            return resultAsBytes;
        }

        private void FlushBuffer()
        {
            Stopwatch sw = Stopwatch.StartNew();
            IEnumerable<LargePermutation> localPerms = _buffer
                .Select(perm => new LargePermutation
                                {
                                    Permutation = Encoding.ASCII.GetString(perm),
                                    BytePermutation = perm
                                });
            Debug.WriteLine($"T1: {sw.ElapsedMilliseconds}");
            sw.Restart();
            Task task = new TaskFactory().StartNew(() =>
            {
                Stopwatch saveChangeStopwatch = Stopwatch.StartNew();
                RunSaveChangesAsync(localPerms);
                Debug.WriteLine($"Save Changes finished: {saveChangeStopwatch.ElapsedMilliseconds}");
            });
            
            Debug.WriteLine($"T2: {sw.ElapsedMilliseconds}");
            _buffer = new List<byte[]>();
        }


        private void RunSaveChangesAsync(IEnumerable<LargePermutation> largePermutations)
        {
            PermutationContext ctx = NewContext;
            ctx.LargePermutations.AddRange(largePermutations);
            ctx.SaveChanges();
        }


        public void AddToBuffer(string element)
        {
            AddToBuffer(element.ToCharArray());
        }

        public void AddToBuffer(char[] element)
        {
            byte[] elementAsBytes = ConvertToBytes(element);
            if (_buffer.Count >= _maxBufferSize)
            {
                FlushBuffer();
            }
            _buffer.Add(elementAsBytes);
        }

    }

}