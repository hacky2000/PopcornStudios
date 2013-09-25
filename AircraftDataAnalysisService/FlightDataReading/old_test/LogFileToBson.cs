using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.old_test
{

    public class LogFileToBson
    {
        private MongoClient m_mongoClient;
        private MongoServer m_mongoServer;
        private MongoDatabase m_mongoDatabase;
        private MongoCollection<MongoDB.Bson.BsonDocument> m_mongodbCollection;

        public MongoCollection<MongoDB.Bson.BsonDocument> MongodbCollection
        {
            get { return m_mongodbCollection; }
            set { m_mongodbCollection = value; }
        }

        public LogFileToBson(string connectionString, string databaseName, string collectionName)
        {
            //var connectionString = 
            //    Environment.GetEnvironmentVariable("CSharpDriverTestsConnectionString")
            //    ?? "mongodb://localhost/?w=1";

            var mongoUrl = new MongoUrl(connectionString);
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
            if (!clientSettings.WriteConcern.Enabled)
            {
                clientSettings.WriteConcern.W = 1; // ensure WriteConcern is enabled regardless of what the URL says
            }

            this.m_mongoClient = new MongoClient(clientSettings);
            this.m_mongoServer = this.m_mongoClient.GetServer();
            this.m_mongoDatabase = this.m_mongoServer.GetDatabase(databaseName);
            this.m_mongodbCollection = m_mongoDatabase.GetCollection(collectionName);

            // connect early so BuildInfo will be populated
            //this.m_mongoServer.Connect();
        }

        //public void Do(string filePath, PropertyDescriptor[] descriptors)
        //{
        //    int capacity = 1000;
        //    List<BsonDocument> docs = new List<BsonDocument>(capacity);

        //    LineSplitLogExtractor extractor = new LineSplitLogExtractor(filePath) { Seperator = this.GetSeperator() };

        //    while (extractor.IsOpen)
        //    {
        //        BsonObjectWrapper wrapper = new BsonObjectWrapper();
        //        wrapper.PropertyDescriptors = descriptors;
        //        wrapper.Extractor = extractor;
        //        wrapper.Read();

        //        var bsonObj = wrapper.BsonObject;
        //        docs.Add(bsonObj);
        //        if (docs.Count >= capacity)
        //        {
        //            this.m_mongodbCollection.InsertBatch(docs);
        //            docs.Clear();
        //        }
        //    }

        //    this.m_mongodbCollection.InsertBatch(docs);
        //    docs.Clear();
        //}

        //private char GetSeperator()
        //{
        //    return (char)0x1F;
        //}

        public void InsertBatch(IEnumerable<BsonDocument> docs)
        {
            this.m_mongodbCollection.InsertBatch(docs);
        }

        public void Connect()
        {
            this.m_mongoServer.Connect();
        }

        public void Disconnect()
        {
            this.m_mongoServer.Disconnect();
        }

        internal void Insert(BsonDocument doc)
        {
            this.m_mongodbCollection.Insert(doc);
        }
    }
}
