using Dashboard.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
		private readonly ISchema _schema;
		private readonly IDocumentExecuter _executer;
		private readonly IDocumentWriter _writer;
		private readonly ILogger<DashboardController> _logger;

		public DashboardController(ILogger<DashboardController> logger, ISchema schema, IDocumentExecuter executer, IDocumentWriter writer)
		{
			_schema = schema;
			_executer = executer;
			_writer = writer;
			_logger = logger;
		}

		[HttpPost]
		[Route("graphql")]
		public async Task<IActionResult> Post([FromBody] GraphQLQueryDTO query)
		{
			try
			{
				// Introspection is the ability to query which resources are available in the current API schema.
				// Given the API, via introspection, we can see the queries, types, fields, and directives it supports.
				// Ignore IntrospectionQuery here. This can be handled by the GraphQL middleware on a different endpoint.

				if (!string.IsNullOrWhiteSpace(query.OperationName) && query.OperationName.Equals("IntrospectionQuery"))
					return Ok();

				var result = await _executer.ExecuteAsync(_ =>
				{
					_.Schema = _schema;
					_.Query = query.Query;
					_.Inputs = query.Variables?.ToInputs();
				});

				if (result.Errors?.Count > 0)
				{
					return BadRequest();
				}
				var json = await _writer.WriteToStringAsync(result);

				Response.StatusCode = 200;
				Response.ContentType = "application/json";

				var jsonResponse = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true, PropertyNameCaseInsensitive = true, MaxDepth = 10, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

				return Ok(jsonResponse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error executing GraphQL query: [{ex.Message}]");
			}
		}
	}
}
