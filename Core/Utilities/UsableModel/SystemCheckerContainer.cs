using Core.Resources.Enums;
using System;
using System.Collections.Generic;

namespace Core.Utilities.UsableModel
{
    public class SystemCheckerContainer
    {
        public SystemCheckerContainer()
        {
            FilterFromDate = DateTime.ParseExact("01.09.2019", "dd.MM.yyyy", null);
            FilterToDate = DateTime.Now;
            IsAnalyseAnyTime = false;
            CountDownMinutes = 15;
        }
        public bool IsCountry_Checked { get; set; }
        public bool IsLeague_Checked { get; set; }
        public bool IsFT_ResultChecked { get; set; }
        public bool IsHT_ResultChecked { get; set; }
        public bool IsFT_15_OU_Checked { get; set; }
        public bool IsFT_25_OU_Checked { get; set; }
        public bool IsFT_35_OU_Checked { get; set; }
        public bool IsHT_15_OU_Checked { get; set; }
        public bool IsGG_NG_Checked { get; set; }
        public bool IsGoalBetween_Checked { get; set; }
        public DateTime FilterFromDate { get; set; }
        public DateTime FilterToDate { get; set; }
        public byte MinimumPercentage { get; set; }
        public byte MinimumFoundMatch { get; set; }
        public string SerialsText { get; set; }
        public bool IsAnalyseAnyTime { get; set; }
        public int CountDownMinutes { get; set; }
        public List<string> SerialsBeforeGenerated { get; set; }
    }
}
