using MiceWiseData.Indexes;
using MiceWiseData.Roots;
using vlko.core.RavenDB.Repository;
using vlko.core.RavenDB.Repository.RepositoryAction;
using vlko.core.Repository;

namespace MiceWiseData.Commands
{
    public class DeviceCommands : CrudCommands<Device>, IDeviceCommands
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All devices.
        /// </returns>
        public IQueryResult<Device> GetAll()
        {
            return new QueryLinqResult<Device>(SessionFactory<Device>.IndexQuery<DeviceSortIndex>());
        }
    }
}