using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Common;
using Clean.Api.Contracts.Users;
using Clean.Api.Helpers.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Filters
{
    public class QueryableList : ActionFilterAttribute
    {
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) return;
            dynamic query = ((ObjectResult)context.Result).Value;

            if (query == null) throw new Exception("Unable to retreive value of IQueryable from context result.");

            var request = context.HttpContext.Request;

            var queryParams = new QueryParameters();

            if (request.QueryString.HasValue)
            {
                queryParams = new QueryParameters(request.Query["filter"], request.Query["sort"], request.Query["pagesize"], request.Query["page"]);
                try
                {
                    query = IQueryableExtensions.GetQueryResults(query, queryParams);
                }
                catch (FormatException e)
                {
                    throw new BadRequestException(e.Message);
                }
                
            }

            var total = queryParams.FilteredCount;

            context.Result = GetPagedResult(GetArray(query), queryParams, total);
        }

        private static T[] GetArray<T>(IEnumerable<T> data)
        {
            return data.ToArray();
        }

        private IActionResult GetPagedResult( object[] data, QueryParameters queryParameters, int total)
        {
            var pagedResult = new PagedResult()
            {
                Data = data,
                Page = queryParameters.Page,
                PageSize = queryParameters.PageSize
            };
            pagedResult.TotalPages = (uint)Math.Ceiling((float)total / (float)pagedResult.PageSize);

            return new OkObjectResult(pagedResult);
        }
    }
}
