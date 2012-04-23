using System.ComponentModel.Composition;
using MiceWiseData.Roots;
using vlko.core.Repository;
using vlko.core.Repository.RepositoryAction;

namespace MiceWiseData.Commands
{
    [InheritedExport]
    public interface ICelebrityCommands : ICommandGroup<Celebrity>, ICrudCommands<Celebrity>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All celebrities.</returns>
        IQueryResult<Celebrity> GetAll();
    }
}