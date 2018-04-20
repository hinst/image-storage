namespace image_storage {
    class CommandLineArgs {
        string[] args;

        public CommandLineArgs(string[] args) {
            this.args = args;
        }

        public string Get(string key) {
            foreach (var arg in args) {
                if (arg.StartsWith("--" + key)) {
                    return arg.Substring(2 + key.Length + 1);
                }
            }
            return null;
        }
    }
}