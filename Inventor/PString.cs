using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Force.Crc32;

namespace Inventor
{
    class PString
    {
        public Dictionary<string, string> cache = new Dictionary<string, string>();

        public string Add(string text)
        {
            string hash = "P" + Crc32Algorithm.Compute(Encoding.ASCII.GetBytes(text));
            if (!cache.ContainsKey(hash)) cache.Add(hash, text);
            return hash;
        }

        public void AddFile(string file)
        {
            if (File.Exists(file))
            {
                Parser parser = new Parser(File.ReadAllText(file), null);
                if (parser.ParseNext(null))
                {
                    foreach (KeyValuePair<string, string> pstring in parser.results)
                    {
                        if (!cache.ContainsKey(pstring.Key)) cache.Add(pstring.Key, pstring.Value);
                    }
                }
            }
        }

        public void AddPath(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    if (file.ToLower().EndsWith(".ms"))
                    {
                        AddFile(file);
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = String.Empty;
            foreach (KeyValuePair<string, string> entry in cache.OrderBy(x => x.Value))
            {
                s += "\"" + entry.Key + "\" \"" + entry.Value + "\"" + Environment.NewLine;
            }
            cache.Clear();
            return s;
        }
    }
}
