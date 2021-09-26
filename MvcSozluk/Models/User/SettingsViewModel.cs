using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSozluk.Models.User
{
    public class SettingsViewModel
    {
        public Writer Writer { get; set; }

        public string NewPassword { get; set; }

    }
}