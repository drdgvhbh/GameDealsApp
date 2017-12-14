using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Models.ITAD {
    using Newtonsoft.Json;

    public class Converter {
        public static readonly JsonSerializerSettings Settings =
            new JsonSerializerSettings {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None
            };
    }
}
