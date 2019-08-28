using Gsv.Types;

namespace Gsv.Caches
{    public interface ICategoryCache : IEntityListCache<Category, Category, Category>
    {
    }
    public interface ISourceCache : IEntityListCache<Source, Source, Source>
    {
    }
}