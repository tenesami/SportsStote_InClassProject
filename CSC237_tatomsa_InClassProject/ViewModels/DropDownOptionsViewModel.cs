using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.ViewModels
{
    public class DropDownOptionsViewModel
    {
        public Dictionary<string, string> Items { get; set; }

        public string SelectdValue { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultText { get; set; }

        public bool HasDefalut => !string.IsNullOrEmpty(DefaultText);
    }
}
