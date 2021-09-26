using Data.Abstract;
using Data.Concrete.Repositories;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
    public class EfContentDal: GenericRepository<Content>, IContentDal
    {

        //public List<T> List()
        //{
        //    return _object.ToList();
        //}

    }
}
