using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Inventor
{
    class Recipes
    {
        public OrderedDictionary list;

        public Recipes()
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
                string line;
                string name = String.Empty;
                string text = String.Empty;
                StreamReader sr = new StreamReader(file);
                while ((line = sr.ReadLine()) != null)
                {
                    string l = line.Trim();
                    if (l.ToUpper().IndexOf("DETAILRECIPE ") == 0)
                    {
                        name = l.Substring(13).Trim();
                    }
                    else if (l.Equals("{"))
                    {
                        text = String.Empty;
                    }
                    else if (l.Equals("}"))
                    {
                        Add(name, text);
                    }
                    else
                    {
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
                if (!String.IsNullOrEmpty(s)) s += Environment.NewLine;
                s += "DetailRecipe " + recipe.Key + Environment.NewLine;
                s += "{" + Environment.NewLine;
                s += recipe.Value;
                s += "}" + Environment.NewLine;
            }
            list.Clear();
            return s;
        }
    }
}
