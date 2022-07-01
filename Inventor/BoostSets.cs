using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Inventor
{
    public class BoostSets
    {
        public OrderedDictionary list;

        public BoostSets()
        {
            list = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(string name, string text)
        {
            if (list.Contains(name))
            {
                list[name] = text;
            }
            else
            {
                list.Add(name, text);
            }
        }

        public void AddFile(string file)
        {
            // Really, really, REALLY dumb parser for files that have a single non-nesting structure.
            if (File.Exists(file))
            {
                int depth = 0;
                string line;
                string name = String.Empty;
                string text = String.Empty;
                StreamReader sr = new StreamReader(file);
                while ((line = sr.ReadLine()) != null)
                {
                    string l = line.Trim().ToUpper();
                    if (l.Equals("BOOSTSET"))
                    {
                        name = String.Empty;
                        text = String.Empty;
                    }
                    else if (l.Equals("{"))
                    {
                        depth++;
                        if (depth > 1)
                        {
                            text += line + Environment.NewLine;
                        }
                    }
                    else if (l.Equals("}"))
                    {
                        depth--;
                        if (depth > 0)
                        {
                            text += line + Environment.NewLine;
                        }
                        else
                        {
                            Add(name, text);
                        }
                    }
                    else
                    {
                        if (l.IndexOf("NAME ") == 0)
                        {
                            name = l.Substring(5).Trim();
                        }
                        text += line + Environment.NewLine;
                    }
                }

                sr.Close();
            }
        }

        public override string ToString()
        {
            string s = String.Empty;
            foreach (DictionaryEntry recipe in list)
            {
                s += "BoostSet" + Environment.NewLine;
                s += "{" + Environment.NewLine;
                s += recipe.Value;
                s += "}" + Environment.NewLine;
            }
            list.Clear();
            return s;
        }
    }
}
