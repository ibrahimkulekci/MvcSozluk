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
    public class WriterLoginManager: IWriterLoginService
    {
        IWriterDal _writerDal;

        public WriterLoginManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetWriter(string email)
        {
            return _writerDal.Get(x => x.WriterMail == email);
        }
    }
}
