using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class AboutSettingViewModel
    {
        [Display(Name = "درباره ما")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }
    }
}
