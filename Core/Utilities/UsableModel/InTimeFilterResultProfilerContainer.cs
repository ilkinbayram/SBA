using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.UsableModel
{
    public class InTimeFilterResultProfilerContainer
    {
        public string Serial { get; set; }
        public string MatchURL { get; set; }
        public string Away { get; set; }
        public string Home { get; set; }
        public string ZEND_HT_Result { get; set; }
        public string ZEND_FT_Result { get; set; }
        public List<FilterResult> FilterResults { get; set; }
    }
}
