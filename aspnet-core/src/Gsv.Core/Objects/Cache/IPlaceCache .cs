using System.Collections.Generic;

namespace Gsv.Objects.Cache
{
    public interface IPlaceCache
    {
        List<Place> GetList();

        Place GetById(int id);
    }
}