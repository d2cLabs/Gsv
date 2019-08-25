using Gsv.Objects;

namespace Gsv.Caches
{
    public interface IPlaceCache : IEntityListCache<Place, Place, Place>
    {
    }
    public interface IPlaceShelfCache : IEntityListCache<PlaceShelf, PlaceShelf, PlaceShelf>
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