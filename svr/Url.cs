namespace image_storage {

    static class Url {
        public static string EncodeB64(byte[] data) {
            return System.Convert.ToBase64String(data)
                .Replace('+', '-').Replace('/', '_');
        }

        static readonly char[] base64padding = { '=' };
    }
}