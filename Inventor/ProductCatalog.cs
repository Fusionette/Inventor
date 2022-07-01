using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Inventor
{
    public class ProductCatalog
    {
        public OrderedDictionary list;

        public ProductCatalog()
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
                    string l = line.Trim().ToUpper();
                    if (l.Equals("CATALOGITEM"))
                    {
                        name = String.Empty;
                        text = String.Empty;
                    }
                    else if (l.Equals("END"))
                    {
                        Add(name, text);
                    }
                    else
                    {
                        if (l.IndexOf("SKU ") == 0)
                        {
                            name = l.Substring(4);
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
            foreach (DictionaryEntry item in list)
            {
                s += "CatalogItem" + Environment.NewLine;
                s += item.Value;
                s += "End" + Environment.NewLine + Environment.NewLine;
            }
            list.Clear();
            return s;
        }
    }
}