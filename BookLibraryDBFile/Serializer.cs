using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BazhkoTarchyla.BookLibrary.DAO
{ 
#pragma warning disable SYSLIB0011
    class Serializer
    {
        public static void Serialize<T>(string fileName, List<T> libraries)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, libraries);
            }
        }

        public static List<T> Deserialize<T>(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return (List<T>)new BinaryFormatter().Deserialize(fs);
            }
        }
    }
}