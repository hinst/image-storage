namespace image_storage {
    class WebImage {
        public WebImage() {
        }

        public WebImage(ImageObject o) {
            this.Id = o.Id.ToString();
            this.OriginalFileName = o.OriginalFileName;
            this.Url = image_storage.Url.EncodeB64(o.Data);
        }

        public string Id { get; set; }
        public string OriginalFileName { get; set; }
        public string Url { get; set; }
    }
}