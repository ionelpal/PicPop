using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic
{
    internal class CustomSerializer
    {
        public static T Deserialize<T>(MemoryStream stream)
        {
            stream.Position = 0;
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            return (T)dcs.ReadObject(stream);
        }

        public static MemoryStream Serialize<T>(T value)
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            dcs.WriteObject(stream, value);
            return stream;
        }
    }
}
