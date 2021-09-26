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
    public class ContentManager : IContentService
    {
        IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public void ContentAdd(Content content)
        {
            _contentDal.Insert(content);
        }

        public void ContentDelete(Content content)
        {
            throw new NotImplementedException();
        }

        public void ContentUpdate(Content content)
        {
            throw new NotImplementedException();
        }

        public Content GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Content> GetListOrderByDesc()
        {
            return (List<Content>)_contentDal.List().OrderByDescending(x => x.ContentID).ToList();
        }

        public List<Content> GetList()
        {
            return _contentDal.List();
        }

        public List<Content> GetListByHeadingID(int id)
        {
            return _contentDal.List(x => x.HeadingID == id);
        }

        public List<Content> GetListByWriter(int id)
        {
            return (List<Content>)_contentDal.List().Where(x => x.WriterID == id).OrderByDescending(x => x.ContentID).ToList();
            //return _contentDal.List(x => x.WriterID == id);
        }

        public List<Content> GetListOrderByDistinct()
        {
            return (List<Content>)_contentDal.List().Distinct().OrderByDescending(x => x.ContentID).Take(10).ToList();
            //burası dünzenlecek-31.08.2021
        }
    }
}
