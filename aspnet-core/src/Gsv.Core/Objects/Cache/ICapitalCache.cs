using System.Collections.Generic;

namespace Gsv.Objects.Cache
{
    public interface ICapitalCache
    {
        List<Capital> GetList();

        Capital GetById(int id);
    }
}