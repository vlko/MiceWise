using System;
using System.IO;
using MiceWiseData.Indexes;
using MiceWiseData.Roots;
using MiceWiseData.ViewModel;
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

        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <returns></returns>
        public IQueryResult<CelebrityView> GetActive()
        {
            return new ProjectionAsQueryResult<Celebrity, CelebrityView>(SessionFactory<Celebrity>.IndexQuery<CelebrityViewIndex>())
                .AddSortMapping(item => item.Priority, item => item.Priority);
        }

        /// <summary>
        /// Registers the specified register model.
        /// </summary>
        /// <param name="registerModel">The register model.</param>
        public void Register(CelebrityRegisterViewModel registerModel)
        {
            var celebrity = new Celebrity
                                {
                                    Name = registerModel.Name,
                                    Email = registerModel.Email,
                                    Place = registerModel.Place,
                                    Portfolio = registerModel.Portfolio,
                                    Mouse = registerModel.Mouse,
                                    Tablet = registerModel.Tablet,
                                    Picture = registerModel.Picture,
                                    RegisterTime = DateTime.UtcNow
                                };
            SessionFactory<Celebrity>.Store(celebrity);
        }
    }
}