using Web3.Module.Models;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;

namespace Web3.Module.Handlers
{
    public class PersonPart2Handler : ContentPartHandler<PersonPart2>
    {
        public override Task UpdatedAsync(UpdateContentContext context, PersonPart2 instance)
        {
            context.ContentItem.DisplayText = instance.Name;
            return Task.CompletedTask;
        }
    }
}
