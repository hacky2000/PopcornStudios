using System;

namespace FlightDataReading.DataPointTransforms
{
    public interface IFlightDataEntityTransformStrategy
    {
        FlightDataEntities.Level1FlightRecord FromFlightRecordEntityToLevel1FlightRecord(FlightDataEntities.FlightRawData entity);
        FlightDataEntities.FlightRawData FromLevel1FlightRecordToFlightRawData(FlightDataEntities.Level1FlightRecord record);
    }
}
