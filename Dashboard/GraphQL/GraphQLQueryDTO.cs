using GraphQL.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dashboard.GraphQL
{
    public class GraphQLQueryDTO
    {
		public string OperationName { get; set; }
		public string NamedQuery { get; set; }
		public string Query { get; set; }

		[JsonConverter(typeof(ObjectDictionaryConverter))]
		public Dictionary<string, object> Variables
		{
			get; set;
		}
	}
}
