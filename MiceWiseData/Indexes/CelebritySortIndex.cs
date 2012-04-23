using System.Linq;
using MiceWiseData.Roots;
using Raven.Client.Indexes;

namespace MiceWiseData.Indexes
{
    public class CelebritySortIndex : AbstractIndexCreationTask<Celebrity>
	{
        public CelebritySortIndex()
		{
			Map = celebrities => from item in celebrities
			               select new {item.Id, item.Name, item.Email, item.Place};
		}
	}
}
