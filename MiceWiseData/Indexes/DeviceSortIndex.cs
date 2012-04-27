using System.Linq;
using MiceWiseData.Roots;
using Raven.Client.Indexes;

namespace MiceWiseData.Indexes
{
    public class DeviceSortIndex : AbstractIndexCreationTask<Device>
	{
        public DeviceSortIndex()
		{
			Map = devices => from item in devices
			               select new {item.Id, item.Name, item.Brand};
		}
	}

}
