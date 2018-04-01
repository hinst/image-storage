using System.Security.Cryptography;

namespace image_storage {

    class ImageObject {
        string OriginalFileName;
        byte[] Data;
        string DataHash;

        public void LoadHash() {
            using (var hasher = MD5.Create()) {
                DataHash = System.Convert.ToBase64String(hasher.ComputeHash(Data));
            }
        }
    }

}