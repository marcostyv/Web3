//using Web3.Module.Models;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;
using Web3.module.Models;

namespace Web3.Module.Handlers
{
    public class OrchardPostPartHandler : ContentPartHandler<OrchardPostPart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, OrchardPostPart instance)
        {
            context.ContentItem.DisplayText = instance.TitlePost;
            return Task.CompletedTask;
        }
    }
}
