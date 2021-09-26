using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSozluk.Models.User
{
    public class DisplayViewModel
    {
        public Writer Writer { get; set; }
        
        public List<Content> Content { get; set; }

    }
}