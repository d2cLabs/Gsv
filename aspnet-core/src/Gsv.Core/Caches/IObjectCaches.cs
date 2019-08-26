using Gsv.Objects;

namespace Gsv.Caches
{
    public interface IPlaceCache : IEntityListCache<Place, Place, Place>
    {
    }
    public interface IShelfCache : IEntityListCache<Shelf, Shelf, Shelf>
    {
    }

    public interface ICargoTypeCache : IEntityListCache<CargoType, CargoType, CargoType>
    {
    }

    public interface ICapitalCache : IEntityListCache<Capital, Capital, Capital>
    {
    }

    public interface IObjectCache : IEntityListCache<Object, Object, Object>
    {
    }


}