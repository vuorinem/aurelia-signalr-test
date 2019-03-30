using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using AureliaSignalRTest.Hubs;
using Microsoft.AspNetCore.Mvc;

namespace AureliaSignalRTest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private static List<Item> items = new List<Item>() {
            new Item { Id = 1, Label = "Buy bananas", IsDone = true },
            new Item { Id = 2, Label = "Call mum", IsDone = false },
        };

        private readonly ItemsHub hub;

        public ItemsController(ItemsHub hub) {
            this.hub = hub;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get() {
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id) {
            return items.Where(i => i.Id == id).SingleOrDefault();
        }

        [HttpPost]
        public async Task PostAsync([FromBody] string label) {
            items.Add(new Item {
                Id = (items.Max(i => (int?)i.Id) ?? 0) + 1,
                Label = label,
                IsDone = false,
            });

            await hub.Update();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Item update) {
            var item = items.Where(i => i.Id == id).SingleOrDefault();

            item.Label = update.Label;
            item.IsDone = update.IsDone;

            await hub.Update();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) {
            items.RemoveAll(i => i.Id == id);

            await hub.Update();
        }
    }
}
