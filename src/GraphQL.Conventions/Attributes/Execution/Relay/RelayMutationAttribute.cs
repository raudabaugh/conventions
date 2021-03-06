using System;
using System.Threading.Tasks;
using GraphQL.Conventions.Attributes;
using GraphQL.Conventions.Execution;

namespace GraphQL.Conventions.Relay
{
    [AttributeUsage(Fields, AllowMultiple = true, Inherited = true)]
    public class RelayMutationAttribute : ExecutionFilterAttributeBase
    {
        public override async Task<object> Execute(IResolutionContext context, FieldResolutionDelegate next)
        {
            var output = await next(context).ConfigureAwait(false);
            var input = Unwrap(context.GetArgument("input"));

            var inputObj = input as IRelayMutationInputObject;
            var outputObj = output as IRelayMutationOutputObject;
            if (inputObj != null && outputObj != null)
            {
                outputObj.ClientMutationId = inputObj.ClientMutationId;
            }

            return output;
        }
    }
}