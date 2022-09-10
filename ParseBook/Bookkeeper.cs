using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseBook
{
    public class Bookkeeper : IEnumerable<string>
    {
        private Parser _parser;
        private Dictionary<string, int> _catalog = new Dictionary<string, int>();
        public Bookkeeper(Parser parser)
        {
            _parser = parser;
        }

        public void Execute() 
        { 
            _catalog.Clear();

            foreach (var items in _parser.Parse())
            {
                foreach (var item in items)
                {
                    if (item == null) { continue; }
                    var result = item.ToLower().Trim();

                    if (result == "") { continue; }

                    if (_catalog.ContainsKey(result))
                    {
                        _catalog[result] = _catalog[result] + 1;
                    }
                    else
                    {
                        _catalog.Add(result, 1);
                    }
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {

            foreach (var item in _catalog.OrderByDescending(x => x.Value))
            {
                yield return $"{item.Key} -\t\t {item.Value}";
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _catalog.OrderByDescending(x => x.Value))
            {
                yield return $"{item.Key} -\t\t {item.Value}";
            }
        }
    }
}
