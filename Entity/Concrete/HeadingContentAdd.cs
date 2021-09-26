using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class HeadingContentAdd
    {
        public string HeadingName { get; set; }
        public DateTime HeadingDate { get; set; }
        public int CategoryID { get; set; }
        public int WriterID { get; set; }
        public bool HeadingStatus { get; set; }
        public string ContentValue { get; set; }
        public DateTime ContentDate { get; set; }
        public int HeadingID { get; set; }
        public bool ContentStatus { get; set; }

    }
}
