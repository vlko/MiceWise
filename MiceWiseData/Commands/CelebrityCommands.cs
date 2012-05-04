using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiceWiseData.Indexes;
using MiceWiseData.Roots;
using MiceWiseData.ViewModel;
using Raven.Client.Linq;
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
        /// Searchs the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// Search results.
        /// </returns>
        public CelebrityView[] Search(string searchQuery)
        {
            var result = new List<CelebrityView>();
            // load 50 results
            var searchResults =
                SessionFactory<Celebrity>.Current.Query<CelebritySearchIndex.Result, CelebritySearchIndex>()
                    .Search(item => item.Query, searchQuery).As<object>().Take(20).ToArray();
            foreach (var searchResult in searchResults)
            {
                if (searchResult is Celebrity)
                {
                    var locatedCelebrity = (Celebrity) searchResult;
                    var celebrity = SessionFactory<Celebrity>.IndexQuery<CelebrityViewIndex>().Where(
                        celeb => celeb.Id == locatedCelebrity.Id).As<CelebrityView>().FirstOrDefault();
                    if (celebrity != null && result.All(item => item.Id != celebrity.Id))
                    {
                        result.Add(celebrity);
                    }
                }
                if (searchResult is Device)
                {
                    var locatedDevice = (Device) searchResult;
                    var celebrities = SessionFactory<Celebrity>.IndexQuery<CelebrityViewIndex>().Where(
                        celeb => celeb.MouseId == locatedDevice.Id || celeb.TabletId == locatedDevice.Id).As<CelebrityView>().ToArray();
                    foreach (var celebrity in celebrities)
                    {
                        if (celebrity != null && result.All(item => item.Id != celebrity.Id))
                        {
                            result.Add(celebrity);
                        }
                    }
                }
            }
            return result.Take(50).ToArray();
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