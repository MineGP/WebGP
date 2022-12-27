using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetDiscordByID;

namespace Application.Tests.data
{
    public class GetDiscordTests
    {
        private GetDiscordByIDQuery query;

        [SetUp]
        public void Setup()
        {
            query = new GetDiscordByIDQuery(123456123);
            
        }

        [Test]
        public async Task GetDiscrodByID_With_Right_Data() 
        {
            var handeler = new GetDiscordByIDQueryHandler(new TestDiscrodRepository());
            var res = JObject.FromObject((await handeler.Handle(query, CancellationToken.None)).First().Value);
            Assert.That(res, Is.EqualTo(JObject.FromObject(new DiscordVM() { DiscordID = 123456123, Level = 3, Phone = 0, Role = "loh", Work = "bomj" })));
        }

        [Test]
        public async Task GetDiscordByID_With_Empty_Data()
        {
            var handeler = new GetDiscordByIDQueryHandler(new TestDiscrodRepositoryBad());
            var res = (await handeler.Handle(query, CancellationToken.None));

            Assert.IsTrue(res.Count() == 0);
        }

    }

    internal class TestDiscrodRepository : IDiscordRepository
    {
        public Task<IEnumerable<DiscordVM>> GetDiscordListAsync()
        {
            return Task.FromResult(getList());

            static IEnumerable<DiscordVM> getList()
            {
                yield return new DiscordVM() { DiscordID = 123456123, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
                yield return new DiscordVM() { DiscordID = 123123, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
                yield return new DiscordVM() { DiscordID = 123123, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
                yield return new DiscordVM() { DiscordID = 123123, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
                yield return new DiscordVM() { DiscordID = 141245, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
                yield return new DiscordVM() { DiscordID = 1234124, Level = 3, Phone = 0, Role = "loh", Work = "bomj" };
            }
        }
    }
    internal class TestDiscrodRepositoryBad : IDiscordRepository
    {
        public Task<IEnumerable<DiscordVM>> GetDiscordListAsync()
        {
            return Task.FromResult<IEnumerable<DiscordVM>>(new List<DiscordVM>());
        }
    }
}
