using System.IO;
using MongoDB.Driver;
using NLog;

namespace image_storage {

    class FileLoader {
        Logger log = LogManager.GetCurrentClassLogger();
        
        public string Dir;

        public void Run() {
            var files = Directory.GetFiles(Dir);
            LoadFiles(files);
        }

        void LoadFiles(string[] files) {
            var images = new ImageDB().Images;
            foreach (var file in files) {
            }
        }

        void LoadFile(string filePath) {
        }

    }
}