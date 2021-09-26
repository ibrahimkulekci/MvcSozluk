using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHeadingService
    {
        List<Heading> GetList();
        List<Heading> GetListOrderByDesc();
        void HeadingAdd(Heading heading);
        Heading GetByID(int id);
        void HeadingDelete(Heading heading);
        void HeadingUpdate(Heading heading);
        List<Heading> GetListByWriter(int id);
        List<Heading> GetListByCategory(int id);
        List<Heading> GetListBySearch(string p);
        
    }
}
