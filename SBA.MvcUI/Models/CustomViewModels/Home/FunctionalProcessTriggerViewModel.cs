using System;

namespace SBA.MvcUI.Models.CustomViewModels.Home
{
    public class FunctionalProcessTriggerViewModel
    {
        public FunctionalProcessTriggerViewModel()
        {
            BeforeDateCount = 0;
        }
        public int BeforeDateCount { get; set; }
    }
}
