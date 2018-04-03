using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace image_storage
{

    class ImageObject {
        public ObjectId Id { get; set; }
        public string OriginalFileName { get; set; }
        public byte[] Data { get; set; }
        public string DataHash { get; set; }

        public void LoadHash() {
            using (var hasher = MD5.Create()) {
                DataHash = System.Convert.ToBase64String(hasher.ComputeHash(Data));
            }
        }

        public JObject ToJsonBrief() {
            var o = new JObject();
            o["Id"] = Id.ToString();
            return o;
        }
    }

}