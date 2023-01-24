using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class TermsSettingViewModel
    {
        [Display(Name = "شرایط و قوانین استفاده")]
        [DataType(DataType.MultilineText)]
        public string Terms { get; set; }
    }
}
