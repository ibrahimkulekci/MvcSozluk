using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ICategoryDal:IRepository<Category>
    {
        /* v1 çalışma
        //Burası örnek amaçlı yapılacak bir interface.
        //CRUD
        //Type Name();
        List<Category> List();

        void Insert(Category p);

        void Update(Category p);

        void Delete(Category p);
        */

    }
}
