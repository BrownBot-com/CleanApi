using StringToExpression.GrammerDefinitions;
using StringToExpression.LanguageDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clean.Api.Helpers.Queries
{
    public class ExtendedODataFilterLanguage : ODataFilterLanguage
	{
		protected override IEnumerable<GrammerDefinition> TypeDefinitions()
		{
			var defs = new List<GrammerDefinition>(base.TypeDefinitions());

			defs.RemoveAll(d => d.Name == "DATE" || d.Name == "TIME");

			defs.InsertRange(0, new[] {
				//new OperandDefinition(
				//	name: "DATETIMEOFFSET",
				//	regex: @"\d{4}-\d{1,2}-\d{1,2}T\d{1,2}:\d{1,2}:\d{1,2}\.\d{1,3}Z",
				//	expressionBuilder: x => Expression.Constant(DateTimeOffset.Parse(x))
				//),
				new OperandDefinition(
					name: "DATE",
					regex: @"\d{4}-\d{1,2}-\d{1,2}",
					expressionBuilder: x => Expression.Constant(DateTime.Parse(x))
				),
				new OperandDefinition(
					name: "TIME",
					regex: @"\d{1,2}:\d{1,2}:\d{1,2}",
					expressionBuilder: x => Expression.Constant(TimeSpan.Parse(x))
				)

			});

			return defs;
		}


	}
}
