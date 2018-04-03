using System.Security.Cryptography;
using MongoDB.Bson.Serialization.Attributes;

namespace image_storage {

    class ImageObject {
        public string OriginalFileName { get; set; }
        public byte[] Data { get; set; }
        public string DataHash { get; set; }

        public void LoadHash() {
            using (var hasher = MD5.Create()) {
                DataHash = System.Convert.ToBase64String(hasher.ComputeHash(Data));
            }
        }
    }

}