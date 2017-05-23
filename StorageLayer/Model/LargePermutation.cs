using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageLayer.Model
{
    public class LargePermutation
    {
        public string Permutation { get; set; }

        [MaxLength(255)]
        public byte[] BytePermutation { get; set; }
    }
}
