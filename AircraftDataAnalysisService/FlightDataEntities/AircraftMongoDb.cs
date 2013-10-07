using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    public class AircraftMongoDb
    {
        public const string DATABASE_COMMON = "AircraftDataAnalysisCommon";

        public const string COLLECTION_AIRCRAFT_MODEL = "AircraftModel";
        public const string COLLECTION_AIRCRAFT_INSTANCE = "AircraftInstance";
        public const string COLLECTION_FLIGHT_PARAMETER = "FlightParameter";
        public const string COLLECTION_FLIGHT_RECORD_LEVEL1 = "FlightRecordLevel1";
        public const string COLLECTION_FLIGHT_RECORD_LEVEL2 = "FlightRecordLevel2";
    }
}
