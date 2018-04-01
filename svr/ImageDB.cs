using MongoDB.Driver;

namespace image_storage {

    class ImageDB {

        IMongoDatabase DB;
        const string dbName = "h-image-storage";
        const string imagesCollectionName = "images";

        public ImageDB() {
            var client = new MongoClient();
            DB = client.GetDatabase(dbName);
        }

        public IMongoCollection<ImageObject> Images {
            get {
                return DB.GetCollection<ImageObject>(imagesCollectionName);
            }
        }

    }

}