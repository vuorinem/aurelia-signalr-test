using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models {
    public class Item {
        public string Label { get; internal set; }
        public bool IsDone { get; internal set; }
        public int Id { get; internal set; }
    }
}
