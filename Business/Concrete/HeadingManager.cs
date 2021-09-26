using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class HeadingManager : IHeadingService
    {
        IHeadingDal _headingDal;

        public HeadingManager(IHeadingDal headingDal)
        {
            _headingDal = headingDal;
        }

        public Heading GetByID(int id)
        {
            return _headingDal.Get(x => x.HeadingID == id);
        }

        public List<Heading> GetList()
        {
            return _headingDal.List();
        }

        public List<Heading> GetListByWriter(int id)
        {
            return _headingDal.List(x => x.WriterID == id);
        }
        public List<Heading> GetListByCategory(int id)
        {
            return _headingDal.List(x => x.CategoryID == id);
        }

        public List<Heading> GetListOrderByDesc()
        {
            return _headingDal.List().Where(x=>x.HeadingStatus==true).OrderByDescending(x => x.HeadingID).Take(20).ToList();
        }

        public void HeadingAdd(Heading heading)
        {
            _headingDal.Insert(heading);
        }

        public void HeadingDelete(Heading heading)
        {
            _headingDal.Update(heading);
        }

        public void HeadingUpdate(Heading heading)
        {
            _headingDal.Update(heading);
        }

        public List<Heading> GetListBySearch(string p)
        {
            return _headingDal.List(x => x.HeadingName.Contains(p)).OrderByDescending(y=>y.HeadingID).ToList();
        }
    }
}
