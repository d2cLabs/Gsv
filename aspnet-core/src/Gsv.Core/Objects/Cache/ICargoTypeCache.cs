using System.Collections.Generic;

namespace Gsv.Objects.Cache
{
    public interface ICargoTypeCache
    {
        List<CargoType> GetList();

        CargoType GetById(int id);
    }
}