using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MiceWiseData.Roots;
using vlko.core.Repository;
using vlko.core.Repository.RepositoryAction;

namespace MiceWiseData.Commands
{
    [InheritedExport]
    public interface IDeviceCommands : ICommandGroup<Device>, ICrudCommands<Device>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All devices.</returns>
        IQueryResult<Device> GetAll();
    }
}
