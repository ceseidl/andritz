using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
    public class Shifter
    {
        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            StreamReader streamReader = new StreamReader(input, encoding);
            StreamWriter streamWriter = new StreamWriter(output, encoding);

            int count = 0;
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (int.TryParse(line.Substring(0, line.Length), out count))
                {
                    streamWriter.WriteLine(count.ToString()); 
                    count++;
                    continue;

                }
                else
                {
                    if (line.IndexOf(":") == 2)
                    {
                        TimeSpan OriginalInicio = TimeSpan.Parse(line.Substring(0, line.Length - line.IndexOf("-->") - 3).Trim().Replace(",", "."));
                        TimeSpan OriginalFim = TimeSpan.Parse(line.Substring(line.IndexOf("-->") + 3, line.Length - line.IndexOf("-->") - 3).Trim().Replace(",", "."));

                        TimeSpan TargetInicio = OriginalInicio.Add(timeSpan);
                        TimeSpan TargetFim = OriginalFim.Add(timeSpan);

                        string lineToOutput = TargetInicio.ToString(@"hh\:mm\:ss\.fff") + " --> " + TargetFim.ToString(@"hh\:mm\:ss\.fff");

                        streamWriter.WriteLine(lineToOutput);
                    }
                    else
                        streamWriter.WriteLine(line);
                }


            }
            streamWriter.Flush();
            StreamReader stream = new StreamReader(streamWriter.BaseStream);
            output = Util.GetStream(stream.ReadToEnd());



        }
       
    }
}
