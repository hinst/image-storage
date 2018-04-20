using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace image_storage {

    class ImageDB {
        IMongoDatabase DB;
        const string dbName = "h-image-storage";
        const string imagesCollectionName = dbName;

        public ImageDB(string connectionString = null) {
            var client = connectionString == null
                ? new MongoClient() 
                : new MongoClient(connectionString);
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

        ProjectionDefinition<ImageObject> NoDataProjection => Builders<ImageObject>.Projection.Exclude(x => x.Data);

        public IEnumerable<ImageObject> Headers {
            get {
                var documents = Images.Find(x => true).Project(NoDataProjection).ToList();
                return documents.Select(document => {
                    var o = new ImageObject();
                    o.Id = document["_id"].AsObjectId;
                    o.OriginalFileName = document["OriginalFileName"].AsString;
                    o.DataHash = document["DataHash"].AsString;
                    return o;
                });
            }
        }

        public ImageObject GetByHash(string hash) {
            return Images.Find(x => x.DataHash == hash).FirstOrDefault();
        }

        public ImageObject GetImageByIdString(string id, bool withData) {
            var objectId = new ObjectId(id);
            if (withData)
                return Images.Find(x => x.Id == objectId).FirstOrDefault();
            else 
                return BsonSerializer.Deserialize<ImageObject>(
                    Images.Find(x => x.Id == objectId).Project(NoDataProjection).FirstOrDefault()
                );
        }

    }

}