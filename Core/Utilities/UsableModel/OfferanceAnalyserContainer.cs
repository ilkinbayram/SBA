using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.UsableModel
{
    public class OfferanceAnalyserContainer
    {
        public OfferanceAnalyserContainer()
        {
            ResultList = new List<string>();
        }

        public int CountFound
        {
            get
            {
                return ResultList.Count;
            }
        }
        public int Percentage
        {
            get
            {
                int moreValue = ResultList.GroupBy(x => x).Select(g => new { Text = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList()[0].Count;
                return moreValue * 100 / CountFound;
            }
        }
        public string Name { get; set; }
        public string MostValue
        {
            get
            {
                return ResultList.GroupBy(x => x).Select(g => new { Text = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList()[0].Text;
            }
        }
        public List<string> ResultList { get; set; }
    } 
}
