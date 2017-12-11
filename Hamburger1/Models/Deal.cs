using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Models {
    public class Deal {
        public string GameTitle { get; set; }

        public Deal() : this(string.Empty) {

        }

        public Deal(string gameTitle) {
            this.GameTitle = gameTitle;
        }
    }
}
