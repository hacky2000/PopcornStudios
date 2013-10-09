using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FlightDataReading.AircraftModel1
{
    public class FlightRawDataExtractorFactory
    {
        public static IFlightRawDataExtractor CreateFlightRawDataExtractor(StorageFile file)
        {
            if (file == null)
                return null;

            var readStreamTask = file.OpenStreamForReadAsync();
            readStreamTask.Wait();
            BinaryReader reader = new BinaryReader(readStreamTask.Result);

            return new FlightDataReadingHandler(reader);
        }
    }
}
