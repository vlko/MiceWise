using MiceWiseData.Indexes;
using MiceWiseData.Roots;
using vlko.core.RavenDB.Repository;
using vlko.core.RavenDB.Repository.RepositoryAction;
using vlko.core.Repository;

namespace MiceWiseData.Commands
{
    public class CelebrityCommands : CrudCommands<Celebrity>, ICelebrityCommands
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All devices.
        /// </returns>
        public IQueryResult<Celebrity> GetAll()
        {
            return new QueryLinqResult<Celebrity>(SessionFactory<Celebrity>.IndexQuery<CelebritySortIndex>());
        }
    }
}