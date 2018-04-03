using System.Linq;
using MongoDB.Driver;

namespace image_storage {

    class ImageDB {
        IMongoDatabase DB;
        const string dbName = "h-image-storage";
        const string imagesCollectionName = dbName;

        public ImageDB() {
            var client = new MongoClient();
            DB = client.GetDatabase(dbName);
        }

        public IMongoCollection<ImageObject> Images {
            get {
                var collection = DB.GetCollection<ImageObject>(imagesCollectionName);
                collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<ImageObject>().Ascending(x => x.DataHash));
                collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<ImageObject>().Ascending(x => x.OriginalFileName));
                return collection;
            }
        }

        public ImageObject[] Headers {
            get {
                var documents = Images.Find(x => true).Project(Builders<ImageObject>.Projection.Exclude(x => x.Data)).ToList();
                return documents.Select(document => {
                    var o = new ImageObject();
                    o.Id = document["_id"].AsObjectId;
                    o.OriginalFileName = document["OriginalFileName"].AsString;
                    o.DataHash = document["DataHash"].AsString;
                    return o;
                }).ToArray();
            }
        }

    }

}