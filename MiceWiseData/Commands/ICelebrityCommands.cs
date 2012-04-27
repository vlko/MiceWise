using System.ComponentModel.Composition;
using MiceWiseData.Roots;
using MiceWiseData.ViewModel;
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

        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <returns>Active celebrities.</returns>
        IQueryResult<CelebrityView> GetActive();

        /// <summary>
        /// Registers the specified register model.
        /// </summary>
        /// <param name="registerModel">The register model.</param>
        void Register(CelebrityRegisterViewModel registerModel);
    }
}