using Web3.Module.Models;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Modules;
using System.Linq;
using System.Threading.Tasks;
using Web3.Module.Models;
using YesSql;
using Web3.Module.Indexes;

namespace Web3.Module.Controllers
{
    public class Person2Controller : Controller
    {
        private readonly ISession _session; // Tenemos que inyectar
        private readonly IContentManager _contentManager;
        private readonly IClock _clock;

        public Person2Controller(ISession session, IContentManager contentManager, IClock clock)
        {
            _session = session;
            _contentManager = contentManager;
            _clock = clock;
        }


        public async Task<string> List()
        {
            var threshold = _clock.UtcNow.AddYears(-40); // Ponemos -40 respecto al tiempo actual del modulo de clock o noseque

            // Con esto lo que hacemos es una query de un contentItem que se encuentra en el contentIndex de acuerdo al content type de person2Page
            var person2Pages = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "PersonPage2") // Esto es para hacer una busqueda en el Content
                .With<PersonPart2Index>(personPart2Index => personPart2Index.BirthDateUtc < threshold)
                .ListAsync();

            // USAR: .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "Person2Page")

            foreach (var Person2page in person2Pages)
            {
                await _contentManager.LoadAsync(Person2page);
            }

            return string.Join(", ", person2Pages.Select(Person2page => Person2page.As<PersonPart2>().Name));
        }
    }
}
