using FlightDataEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AircraftService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AircraftService.svc 或 AircraftService.svc.cs，然后开始调试。
    public class AircraftService : IAircraftService
    {
        public void DoWork()
        {
            System.Console.WriteLine("Hello world, DoWork.");
        }
        private string m_mongoConnectionString = "mongodb://localhost/?w=1";

        public string[] GetAllAircraftModelNames()
        {

            string[] dbnames = null;
            MongoServer mongoServer = this.GetMongoServer();

            Exception ex = null;

            try
            {
                mongoServer.Connect();
                dbnames = mongoServer.GetDatabaseNames().ToArray();
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                if (mongoServer != null)
                    mongoServer.Disconnect();
            }

            if (ex != null)
                throw ex;
            return dbnames;
        }

        private MongoServer GetMongoServer()
        {
            var mongoUrl = new MongoUrl(this.GetMongoCollectionString());
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
            if (!clientSettings.WriteConcern.Enabled)
            {
                clientSettings.WriteConcern.W = 1; // ensure WriteConcern is enabled regardless of what the URL says
            }
            var mongoClient = new MongoClient(clientSettings);
            return mongoClient.GetServer();
        }

        private string GetMongoCollectionString()
        {
            return this.m_mongoConnectionString;
        }

        public FlightDataEntities.AircraftModel[] GetAllAircraftModels()
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
               MongoDatabase database =  mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
               if (database != null)
               {
                   MongoCollection<AircraftModel> modelCollection
                       = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                   IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                   var result = from one in models
                                orderby one.LastUsed descending
                                select one;

                   return result.ToArray();
               }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL));
        }
    }
}
