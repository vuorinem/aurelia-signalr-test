using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AureliaSignalRTest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private static List<Item> items = new List<Item>() {
            new Item { Id = 1, Label = "Buy bananas", IsDone = true },
            new Item { Id = 2, Label = "Call home", IsDone = false },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get() {
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id) {
            return items.Where(i => i.Id == id).SingleOrDefault();
        }

        [HttpPost]
        public void Post([FromBody] string label) {
            items.Add(new Item {
                Id = items.Max(i => i.Id) + 1,
                Label = label,
                IsDone = false,
            });
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item update) {
            var item = items.Where(i => i.Id == id).SingleOrDefault();

            item.Label = update.Label;
            item.IsDone = update.IsDone;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            items.RemoveAll(i => i.Id == id);
        }
    }
}
