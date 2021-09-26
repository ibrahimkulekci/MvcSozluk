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
    class EfHeadingContentAddDal: GenericRepository<HeadingContentAdd>, IHeadingContentAddDal
    {
    }
}
