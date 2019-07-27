using System.Collections.Generic;

namespace Gsv.Types.Cache
{
    public interface ICategoryCache
    {
        List<Category> GetList();

        Category GetById(int id);
    }
}