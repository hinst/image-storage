using MimeTypes;
using System.IO;

namespace image_storage {
    class WebImage {
        public WebImage() {
        }

        public WebImage(ImageObject o) {
            this.Id = o.Id.ToString();
            this.OriginalFileName = o.OriginalFileName;
            this.MimeType = MimeTypeMap.GetMimeType(Path.GetExtension(this.OriginalFileName));
        }

        public string Id { get; set; }
        public string OriginalFileName { get; set; }
        public string MimeType { get; set; }
    }
}