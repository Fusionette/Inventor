using System;
using System.Collections.Generic;

namespace Inventor
{
    // Very simple key/value parser. Ignores comments and lines
    // that do not contain a key/value pair separated by a space.
    // Stores objects in a dictionary, one at a time; the token
    // string in the constructor indicates the end of each object.
    class Parser
    {
        string[] parseData;
        string endToken;
        int loc;
        public Dictionary<string, string> results = new Dictionary<string, string>();
        
        public Parser(string data, string token)
        {
            parseData = data.Split('\r', '\n');
            endToken = token;
            loc = -1;
        }

        public bool ParseNext(Dictionary<string, string> translations)
        {
            results.Clear();
            while (loc < parseData.Length - 1)
            {
                loc++;
                int i = parseData[loc].IndexOf("//");
                if (i == 0) continue;
                if (i > 0) parseData[loc] = parseData[loc].Substring(0, i);
                parseData[loc] = parseData[loc].Trim();
                if (!String.IsNullOrEmpty(endToken) && parseData[loc].Equals(endToken)) break;

                i = parseData[loc].IndexOf(" ");
                if (i > 0)
                {
                    string k = parseData[loc].Substring(0, i).Trim();
                    string v = parseData[loc].Substring(i).Trim();
                    if (k[0].Equals('"') && k[k.Length - 1].Equals('"')) k = k.Substring(1, k.Length - 2);
                    if (v[0].Equals('"') && v[v.Length - 1].Equals('"')) v = v.Substring(1, v.Length - 2);
                    if ((translations != null) && translations.ContainsKey(v)) v = translations[v];
                    if (!results.ContainsKey(k)) results.Add(k, v);
                }
            }

            return results.Count > 0;
        }
    }
}
