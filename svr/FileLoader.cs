using System.IO;
using MongoDB.Driver;
using NLog;

namespace image_storage {

    class FileLoader {
        Logger Log = LogManager.GetCurrentClassLogger();
        
        public string Dir;
        public string DbConnectionString;
        IMongoCollection<ImageObject> Images;

        public void Run() {
            Images = new ImageDB(this.DbConnectionString).Images;
            var files = Directory.GetFiles(Dir);
            LoadFiles(files);
        }

        void LoadFiles(string[] files) {
            foreach (var file in files) {
                LoadFile(file);
            }
        }

        void LoadFile(string filePath) {
            var document = new ImageObject();
            document.Data = File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            document.OriginalFileName = fileName;
            document.LoadHash();
            var existing = Images.Find(x => x.DataHash == document.DataHash);
            if (existing.Any()) {
                Log.Debug("Already have: " + fileName);
            } else {
                Log.Debug("Inserting " + fileName + " " + document.Data.Length);
                Images.InsertOne(document);
            }
        }

    }
}