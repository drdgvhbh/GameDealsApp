using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Messages {
    using System.Runtime.Serialization;

    [DataContract]
    public class AccessToken {
        public static implicit operator string(AccessToken v) {
            return v.ToString();
        }

        public AccessToken() : this(string.Empty) {
        }

        public AccessToken(string token) {
            this.Token = token;
        }

        [DataMember]
        public string Token { get; set; }

        public override string ToString() {
            return this.Token;
        }
    }
}
