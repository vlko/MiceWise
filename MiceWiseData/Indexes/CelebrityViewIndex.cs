using System.Linq;
using MiceWiseData.Roots;
using Raven.Client.Indexes;

namespace MiceWiseData.Indexes
{
    public class CelebrityViewIndex : AbstractIndexCreationTask<Celebrity>
    {
        public CelebrityViewIndex()
        {
            Map = celebrities => from item in celebrities
                                 where item.Active
                                 select new { item.Id, item.Name, item.Email, item.Place, item.Portfolio, item.MouseId, item.TabletId, Priority = item.Priority ?? 0 };
            TransformResults = (database, celebrities) =>
                               from item in celebrities
                               let mouse = database.Load<Device>(item.MouseId)
                               let tablet = database.Load<Device>(item.TabletId)
                               select new
                                          {
                                              item.Id,
                                              item.Name,
                                              item.Email,
                                              item.Place,
                                              item.Portfolio,
                                              item.Picture,
                                              item.Priority,
                                              Mouse = mouse != null ? mouse.Name : null,
                                              MouseBrand = mouse != null ? mouse.Brand : null,
                                              MouseUrl = mouse != null ? mouse.Url : null,
                                              Tablet = tablet != null ? tablet.Name : null,
                                              TabletBrand = tablet != null ? tablet.Brand : null,
                                              TabletUrl = tablet != null ? tablet.Url : null,
                                          };
        }
    }
}