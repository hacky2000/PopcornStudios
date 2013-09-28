using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDataReading.old_test;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program1.Main1(args);
            Program1.Main2(args);
            Program1.Main3(args);
            return;

            FlightBinaryDataDefinition definition = new FlightBinaryDataDefinition()
            {
                #region init
                HeaderDefinition = new FlightBinaryDataHeaderDefinition()
                {
                    BytesCount = 1024,
                    Segments = new FlightBinaryDataContentSegmentDefinition[]
                  {
                       //文件头
                      new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 20,
                           DataTypeStr = DataTypeConverter.STRING,
                            SegmentName = "OPName"},
                            new FlightBinaryDataContentSegmentDefinition(){
                                 BytesCount = 20,
                                  DataTypeStr = DataTypeConverter.STRING,
                                   SegmentName = "BTime"},
                                   new FlightBinaryDataContentSegmentDefinition(){
                                       BytesCount = 4,
                                        DataTypeStr = DataTypeConverter.INT32,
                                         SegmentName = "FlyPlanAddr"},
                                   new FlightBinaryDataContentSegmentDefinition(){
                                       BytesCount = 4,
                                        DataTypeStr = DataTypeConverter.INT32,
                                         SegmentName = "ParaListAddr"},
                                         new FlightBinaryDataContentSegmentDefinition(){
                                              BytesCount = 4,
                                               DataTypeStr = DataTypeConverter.INT32,
                                                SegmentName = "PhyValueAddr"},
                                                //偏移
                                         //new FlightBinaryDataContentSegmentDefinition(){
                                         //     BytesCount = 4,
                                         //      DataTypeStr = DataTypeConverter.STRING,
                                         //       SegmentName = "NULL"},

                        //机型机号信息表：
                        new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 8,
                            DataTypeStr = DataTypeConverter.STRING,
                            SegmentName = "AircrfName"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 12, // 估计是8位而不是12位 BytesCount = 12,
                                 DataTypeStr = DataTypeConverter.STRING,
                                 SegmentName = "AircrfNum"},
                                 new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 20,
                                     DataTypeStr = DataTypeConverter.DATETIME, SegmentName = "StartTime"},
                                     new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 20,
                                          DataTypeStr = DataTypeConverter.DATETIME, SegmentName = "EndTime"},
                                                //偏移
                                         new FlightBinaryDataContentSegmentDefinition(){
                                              BytesCount = 256,
                                               DataTypeStr = DataTypeConverter.STRING,
                                                SegmentName = "(NULL)"},
                                          
                        //工程值参数表：
                                          new FlightBinaryDataContentSegmentDefinition() { BytesCount = 2,
                                                DataTypeStr = DataTypeConverter.STRING, SegmentName = "PNum"},
                                                new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 2,
                                                    DataTypeStr = DataTypeConverter.STRING, SegmentName = "SWNum"},
                        //组参数
                        new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4,
                            DataTypeStr = DataTypeConverter.LONG, SegmentName = "PIndex"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 16,
                                DataTypeStr = DataTypeConverter.STRING, SegmentName = "PName"},
                                new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 34,
                                    DataTypeStr = DataTypeConverter.STRING, SegmentName="PCaption"},
                                    new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 8, //换回8 BytesCount = 7,
                                        DataTypeStr = DataTypeConverter.STRING , SegmentName = "Unit"},
                                         new FlightBinaryDataContentSegmentDefinition(){
                                              BytesCount = 4 , DataTypeStr = DataTypeConverter.FLOAT,
                                              SegmentName = "Ymax"},
                                              new FlightBinaryDataContentSegmentDefinition(){
                                                  BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                                  SegmentName = "Ymin"},
                        new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4,
                            DataTypeStr = DataTypeConverter.LONG, SegmentName = "Freq"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4,
                                DataTypeStr = DataTypeConverter.LONG, SegmentName = "Type"},
                                new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4,
                                    DataTypeStr = DataTypeConverter.LONG, SegmentName = "OutType"},
                                    new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 36,
                                         DataTypeStr = DataTypeConverter.STRING, SegmentName = "SysType"},

                    //单开关量参数表
                                          new FlightBinaryDataContentSegmentDefinition() { BytesCount = 4,
                                              DataTypeStr = DataTypeConverter.LONG , SegmentName = "sWIndex"},
                                              new FlightBinaryDataContentSegmentDefinition() { BytesCount = 16,
                                                  DataTypeStr = DataTypeConverter.STRING, SegmentName = "sWName"},
                        new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 34,
                            DataTypeStr = DataTypeConverter.STRING, SegmentName = "sWCaption"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 16,
                                DataTypeStr = DataTypeConverter.STRING, SegmentName = "PName"},
                                new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4 ,
                                    DataTypeStr = DataTypeConverter.STRING, SegmentName = "Pos"},
                                    new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 33,
                                        DataTypeStr = DataTypeConverter.STRING, SegmentName = "sWSysType"}

                  }
                },
                FrameDefinition = new FlightBinaryDataContentFrameDefinition()
                {
                    BytesCount = 1024,
                    Segments = new FlightBinaryDataContentSegmentDefinition[]
                        { 
                        }
                }

                #endregion
            };

            string path = @"D:\home\110624_01_0004.phy";

            FlightDataReading.FlightDataReadingHandler handler =
                new FlightDataReading.FlightDataReadingHandler(path);

            handler.PreSetAircraftModelName = "A0004";
            handler.MongoDBConnectionString = "mongodb://localhost/?w=1";

            handler.Read();

            FlightDataReading.old_test.FlightRawDataWrapper wrapper = new FlightDataReading.old_test.FlightRawDataWrapper(path) { Definition = definition };
            wrapper.Open();

            FormatOutput(wrapper.Header);

        }

        private static void FormatOutput(FlightDataReading.old_test.FlightDataHeader flightDataHeader)
        {
            StringBuilder b = new StringBuilder();
            int i = 1;
            foreach (var seg in flightDataHeader.Segments)
            {
                b.Append(seg.Value);

                if (i % 8 == 0)
                    b.AppendLine();
                else b.Append('\t');

                i++;
            }
        }
    }
}
