using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;

namespace ParseBook
{
    public class Parser
    {
        private string _path;
        private char[] _delimiterChars = { ' ', '\n', '.', ',', '!', '?', ':','"', '<','>','/','\\', };
        public Parser(string path)
        {
            if (!Exist(path))
            {
                Console.WriteLine("Ошибка пути файла");
                _path = null;
                return;
            }
            _path = path;
        }

        public IEnumerable<string[]> Parse()
        {
            if (_path == null) { yield break; }

            List<string> result;

            StreamReader reader = new StreamReader(_path);
            string? line;
            string? partialString = null;
            while ((line = reader.ReadLine()) != null)
            {
                result = new List<string>();
                var temp = line.Split(_delimiterChars)
                    .ToList();
                if (partialString != null)
                {
                    temp[0] = partialString + temp[0];
                    partialString = null;
                }

                var lastItem = temp[temp.Count - 1];
                if (lastItem.Length > 1 && lastItem[lastItem.Length-1] == '-')//обработка переноса половины слова на следующую строку
                {
                    partialString = lastItem;
                    temp.Remove(partialString);
                    partialString = partialString.Remove(partialString.Length-1) ;
                    
                }
                result.AddRange(temp);

                yield return result.ToArray();
            }
        }

        private bool Exist(string path)
        {
            if ((path == null) || (path.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            try
            {
                var tempFileInfo = new FileInfo(path);

                if (!tempFileInfo.Exists) { return false; }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
