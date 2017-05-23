using System.Data.Entity;
using System.Diagnostics;
using System.Text;

using StorageLayer.Context;
using StorageLayer.Model;

namespace StorageLayer
{
    public class Core
    {

        public void RecreateDatabase()
        {
            using (PermutationContext pc = new PermutationContext())
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<PermutationContext>());
                pc.Database.Initialize(true);
            }
        }

        public void TestByteEncoding()
        {
            PermutationContext pc = PermutationContextFactory.NewContext;
            string inputString = "600a8c444a6522c034ba2a042cc6a37196ef4e7215cd7b9bf775ac23a28c1268";
            byte[] inputAsBytes = Encoding.ASCII.GetBytes(inputString);
            string decodedString = Encoding.ASCII.GetString(inputAsBytes);


            if (inputString == decodedString)
            {
                Debug.WriteLine("It Works!");
            }
        }



    }
}
