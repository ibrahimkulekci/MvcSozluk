using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IContentService
    {
        List<Content> GetList();
        List<Content> GetListOrderByDesc();
        List<Content> GetListOrderByDistinct();
        List<Content> GetListByWriter(int id);
        List<Content> GetListByHeadingID(int id);
        void ContentAdd(Content content);
        Content GetByID(int id);
        void ContentUpdate(Content content);
        void ContentDelete(Content content);

        
    }
}
