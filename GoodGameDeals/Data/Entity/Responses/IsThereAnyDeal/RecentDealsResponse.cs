namespace GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public partial class RecentDealsResponse : IEquatable<RecentDealsResponse> {
        [JsonProperty("data")]
        public DataC Data { get; set; }

        [JsonProperty(".meta")]
        public MetaC Meta { get; set; }

        public override bool Equals(object obj) {
            return this.Equals(obj as RecentDealsResponse);
        }

        public bool Equals(RecentDealsResponse other) {
            return other != null &&
                   EqualityComparer<DataC>.Default.Equals(this.Data, other.Data) &&
                   EqualityComparer<MetaC>.Default.Equals(this.Meta, other.Meta);
        }

        public override int GetHashCode() {
            var hashCode = -689254395;
            hashCode = hashCode * -1521134295 + EqualityComparer<DataC>.Default.GetHashCode(this.Data);
            hashCode = hashCode * -1521134295 + EqualityComparer<MetaC>.Default.GetHashCode(this.Meta);
            return hashCode;
        }

        public static bool operator ==(RecentDealsResponse response1, RecentDealsResponse response2) {
            return EqualityComparer<RecentDealsResponse>.Default.Equals(response1, response2);
        }

        public static bool operator !=(RecentDealsResponse response1, RecentDealsResponse response2) {
            return !(response1 == response2);
        }

        public class MetaC : IEquatable<MetaC> {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as MetaC);
            }

            public bool Equals(MetaC other) {
                return other != null &&
                       this.Currency == other.Currency;
            }

            public override int GetHashCode() {
                return 12651974 + EqualityComparer<string>.Default.GetHashCode(this.Currency);
            }

            public static bool operator ==(MetaC c1, MetaC c2) {
                return EqualityComparer<MetaC>.Default.Equals(c1, c2);
            }

            public static bool operator !=(MetaC c1, MetaC c2) {
                return !(c1 == c2);
            }
        }

        public class DataC : IEquatable<DataC> {
            [JsonProperty("count")]
            public long Count { get; set; }

            [JsonProperty("list")]
            public List[] List { get; set; }

            [JsonProperty("urls")]
            public PurpleUrls Urls { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as DataC);
            }

            public bool Equals(DataC other) {
                return other != null &&
                       this.Count == other.Count &&
                       EqualityComparer<List[]>.Default.Equals(this.List, other.List) &&
                       EqualityComparer<PurpleUrls>.Default.Equals(this.Urls, other.Urls);
            }

            public override int GetHashCode() {
                var hashCode = 231048532;
                hashCode = hashCode * -1521134295 + this.Count.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<List[]>.Default.GetHashCode(this.List);
                hashCode = hashCode * -1521134295 + EqualityComparer<PurpleUrls>.Default.GetHashCode(this.Urls);
                return hashCode;
            }

            public static bool operator ==(DataC c1, DataC c2) {
                return EqualityComparer<DataC>.Default.Equals(c1, c2);
            }

            public static bool operator !=(DataC c1, DataC c2) {
                return !(c1 == c2);
            }
        }

        public class PurpleUrls : IEquatable<PurpleUrls> {
            [JsonProperty("deals")]
            public string Deals { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as PurpleUrls);
            }

            public bool Equals(PurpleUrls other) {
                return other != null &&
                       this.Deals == other.Deals;
            }

            public override int GetHashCode() {
                return 1102607220 + EqualityComparer<string>.Default.GetHashCode(this.Deals);
            }

            public static bool operator ==(PurpleUrls urls1, PurpleUrls urls2) {
                return EqualityComparer<PurpleUrls>.Default.Equals(urls1, urls2);
            }

            public static bool operator !=(PurpleUrls urls1, PurpleUrls urls2) {
                return !(urls1 == urls2);
            }
        }

        public class List : IEquatable<List> {
            [JsonProperty("added")]
            public long Added { get; set; }

            [JsonProperty("drm")]
            public string[] Drm { get; set; }

            [JsonProperty("plain")]
            public string Plain { get; set; }

            [JsonProperty("price_cut")]
            public long PriceCut { get; set; }

            [JsonProperty("price_new")]
            public double PriceNew { get; set; }

            [JsonProperty("price_old")]
            public double PriceOld { get; set; }

            [JsonProperty("shop")]
            public Shop Shop { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("urls")]
            public DealUrl Urls { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as List);
            }

            public bool Equals(List other) {
                return other != null &&
                       this.Added == other.Added &&
                       this.Drm.SequenceEqual(other.Drm) &&
                       this.Plain == other.Plain &&
                       this.PriceCut == other.PriceCut &&
                       this.PriceNew == other.PriceNew &&
                       this.PriceOld == other.PriceOld &&
                       EqualityComparer<Shop>.Default.Equals(this.Shop, other.Shop) &&
                       this.Title == other.Title &&
                       EqualityComparer<DealUrl>.Default.Equals(this.Urls, other.Urls);
            }

            public override int GetHashCode() {
                var hashCode = -66589406;
                hashCode = hashCode * -1521134295 + this.Added.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Plain);
                hashCode = hashCode * -1521134295 + this.PriceCut.GetHashCode();
                hashCode = hashCode * -1521134295 + this.PriceNew.GetHashCode();
                hashCode = hashCode * -1521134295 + this.PriceOld.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<Shop>.Default.GetHashCode(this.Shop);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Title);
                hashCode = hashCode * -1521134295 + EqualityComparer<DealUrl>.Default.GetHashCode(this.Urls);
                return hashCode;
            }

            public static bool operator ==(List list1, List list2) {
                return EqualityComparer<List>.Default.Equals(list1, list2);
            }

            public static bool operator !=(List list1, List list2) {
                return !(list1 == list2);
            }
        }

        public class DealUrl : IEquatable<DealUrl> {
            [JsonProperty("buy")]
            public string Buy { get; set; }

            [JsonProperty("game")]
            public string Game { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as DealUrl);
            }

            public bool Equals(DealUrl other) {
                return other != null &&
                       this.Buy == other.Buy &&
                       this.Game == other.Game;
            }

            public override int GetHashCode() {
                var hashCode = -1141505046;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Buy);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Game);
                return hashCode;
            }

            public static bool operator ==(DealUrl urls1, DealUrl urls2) {
                return EqualityComparer<DealUrl>.Default.Equals(urls1, urls2);
            }

            public static bool operator !=(DealUrl urls1, DealUrl urls2) {
                return !(urls1 == urls2);
            }
        }

        public class Shop : IEquatable<Shop> {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            public override bool Equals(object obj) {
                return this.Equals(obj as Shop);
            }

            public bool Equals(Shop other) {
                return other != null &&
                       this.Id == other.Id &&
                       this.Name == other.Name;
            }

            public override int GetHashCode() {
                var hashCode = -1919740922;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Id);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Name);
                return hashCode;
            }

            public static bool operator ==(Shop shop1, Shop shop2) {
                return EqualityComparer<Shop>.Default.Equals(shop1, shop2);
            }

            public static bool operator !=(Shop shop1, Shop shop2) {
                return !(shop1 == shop2);
            }
        }
    }
}