//using System.IO;

using System.Text;

namespace System.IO
{
    public static class StreamEx
    {
        public static string ToString(this Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var data = reader.ReadToEnd();
                data = data.ToTitle();
                return data;
            }
        }
        public static Stream ToStream(this string source)
        {
            var byteArray = Encoding.UTF8.GetBytes(source);
            return new MemoryStream(byteArray);
        } 
    }
}