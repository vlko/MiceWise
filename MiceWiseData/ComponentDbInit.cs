using System;
using MiceWiseData.Indexes;
using MiceWiseData.Roots;
using Newtonsoft.Json;
using Raven.Client;
using Raven.Client.Document;
using vlko.core.RavenDB;
using vlko.core.Roots;

namespace vlko.BlogModule.RavenDB
{
	public class ComponentDbInit : IComponentDbInit
	{
		/// <summary>
		/// Lists the of model types.
		/// </summary>
		/// <returns>List of model types.</returns>
		public Type[] ListOfModelTypes()
		{
			return new[]
					   {
                           typeof(Device),
                           typeof(Celebrity)
					   };
		}

		/// <summary>
		/// Registers the indexes.
		/// </summary>
		/// <param name="documentStore">The document store.</param>
		public void RegisterIndexes(IDocumentStore documentStore)
		{
            new CelebrityViewIndex().Execute(documentStore);
            new CelebritySortIndex().Execute(documentStore);
            new DeviceSortIndex().Execute(documentStore);
		}

		/// <summary>
		/// Customizes the document store.
		/// </summary>
		/// <param name="documentStore">The document store.</param>
		public void CustomizeDocumentStore(IDocumentStore documentStore)
		{

		}
	}
}
