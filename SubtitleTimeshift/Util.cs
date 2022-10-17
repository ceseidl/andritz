using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubtitleTimeshift
{
    public static  class Util
    {
        public static Stream GetStream(string s)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(s);
            streamWriter.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
