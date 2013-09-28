using FlightDataEntities;
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

        [Obsolete]
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
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
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

        public string AddOrUpdateAircraftModel(FlightDataEntities.AircraftModel aircraftModel)
        {
            if (aircraftModel == null)
                return "没有机型。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftModel> modelCollection
                        = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                    IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                    var result = from one in models
                                 where one.ModelName == aircraftModel.ModelName
                                 select one;

                    if (result != null && result.Count() > 0)
                    {
                        foreach (var oneModel in result)
                        {//所有Property复制
                            oneModel.LastUsed = aircraftModel.LastUsed;
                            oneModel.Caption = aircraftModel.Caption;
                            modelCollection.Save(oneModel);
                        }
                    }
                    else
                    {
                        modelCollection.Insert(aircraftModel);
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        }

        public string DeleteAircraft(string aircraftModel)
        {
            if (aircraftModel == null)
                return "没有机型。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftModel> modelCollection
                        = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                    IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                    //var result = from one in models
                    //             where one.ModelName == aircraftModel//aircraftModel.ModelName
                    //             select one;
                    MongoDB.Driver.MongoCursor<AircraftModel> cursor = modelCollection.Find(
                          Query.EQ("ModelName", aircraftModel));
                    AircraftModel model = cursor.First();

                    modelCollection.Remove(
                        Query.EQ("ModelName", aircraftModel));

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        }

        public string AddOrUpdateAircraftInstance(AircraftInstance aircraftInstance)
        {
            if (aircraftInstance == null)
                return "没有机号。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();
                    var result = from one in models
                                 where one.AircraftNumber == aircraftInstance.AircraftNumber
                                    && one.AircraftModel.ModelName == aircraftInstance.AircraftModel.ModelName
                                 select one;

                    if (result != null && result.Count() > 0)
                    {
                        foreach (var oneModel in result)
                        {//所有Property复制
                            oneModel.LastUsed = aircraftInstance.LastUsed;
                            oneModel.AircraftModel = aircraftInstance.AircraftModel;
                            modelCollection.Save(oneModel);
                        }
                    }
                    else
                    {
                        modelCollection.Insert(aircraftInstance);
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);
        }

        public AircraftInstance[] GetAllAircraftInstances()
        {
            return GetAllAircraftInstances(string.Empty);
        }

        public AircraftInstance[] GetAllAircraftInstances(string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(
                        AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();

                    if (string.IsNullOrEmpty(modelName))
                        return models.ToArray();

                    var results = from one in models
                                  where one.AircraftModel != null && one.AircraftModel.ModelName == modelName
                                  select one;

                    if (results != null && results.Count() > 0)
                        return results.ToArray();

                    return new AircraftInstance[] { };
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        }

        public AircraftInstance GetAircraftInstance(string aircraftNumber, string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(
                        AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models
                        = modelCollection.AsQueryable<AircraftInstance>();

                    if (string.IsNullOrEmpty(aircraftNumber))
                        return models.First();

                    if (string.IsNullOrEmpty(modelName))
                    {
                        var results = from one in models
                                      where one.AircraftNumber == aircraftNumber
                                      select one;

                        if (results != null && results.Count() > 0)
                            return results.First();
                    }
                    else
                    {
                        var results = from one in models
                                      where one.AircraftNumber == aircraftNumber
                                      && one.AircraftModel != null && one.AircraftModel.ModelName == modelName
                                      select one;

                        if (results != null && results.Count() > 0)
                            return results.First();
                    }

                    return null;
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        }

        public string AddOrUpdateFlyParameter(FlightParameter flightParameter)
        {
            return this.AddOrUpdateFlyParameter(new FlightParameter[] { flightParameter });
        }

        public string AddOrUpdateFlyParameter(FlightParameter[] flightParameter)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<FlightParameter> modelCollection
                        = database.GetCollection<FlightParameter>(
                        AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);

                    foreach (var fp in flightParameter)
                    {
                        MongoCursor<FlightParameter> pms =
                         modelCollection.Find(Query.And(Query.EQ("ParameterID", fp.ParameterID),
                            Query.EQ("ModelName", fp.ModelName)));
                        if (pms != null && pms.Count() > 0)
                        {
                            foreach (var pm in pms)
                            {
                                pm.IsConcerned = fp.IsConcerned;
                                pm.Caption = fp.Caption;
                                pm.Frequence = fp.Frequence;
                                pm.Index = fp.Index;
                                pm.SubIndex = fp.SubIndex;
                                pm.Unit = fp.Unit;
                            }
                        }
                        else
                        {
                            modelCollection.Insert(fp);
                        }
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);
        }

        public IEnumerable<FlightParameter> GetAllFlightParameters(string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<FlightParameter> modelCollection
                        = database.GetCollection<FlightParameter>(
                        AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);

                    IQueryable<FlightParameter> models = modelCollection.AsQueryable<FlightParameter>();

                    var results = from one in models
                                  where one.ModelName == modelName
                                  orderby one.Index, one.SubIndex
                                  select one;

                    if (results != null && results.Count() > 0)
                        return results.ToArray();

                    return new FlightParameter[] { };
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER));
        }
    }
}
