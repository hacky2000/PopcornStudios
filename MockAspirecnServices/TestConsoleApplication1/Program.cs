using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace TestConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main1(args);
            Main3(args);
        }

        static void Main1(string[] args)
        {
            do
            {
                try
                {
                    //Database.SetInitializer<Aspirecn.Entities.Cssp.CsspEntities>(
                    //   new System.Data.Entity.DropCreateDatabaseIfModelChanges<
                    //        Aspirecn.Entities.Cssp.CsspEntities>());

                    //System.Data.Entity.DbModelBuilder builder =
                    //    new DbModelBuilder(DbModelBuilderVersion.Latest);
                    //builder.Build(

                    //Database.SetInitializer<Aspirecn.Entities.Cssp.CsspEntities>(
                    //   new System.Data.Entity.CreateDatabaseIfNotExists<
                    //        Aspirecn.Entities.Cssp.CsspEntities>());

                    //RegisterRoutes(RouteTable.Routes);
                    //string url = "http://localhost:59212/MockAspirecnServices/uc.ashx";

                    string content = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<req>
  <head>
<msgType>UserLogon</msgType>
<msgPlace>100</msgPlace>
  </head>
  <body>
<parameter>
      <name>loginName</name>
      <value>13500000000</value>
</parameter>
<parameter>
      <name>password</name>
      <value>12345</value>
</parameter>
<parameter>
      <name>userType</name>
      <value>1</value>
</parameter>
  </body>
</req>
";

                    RestClient client = new RestClient("http://localhost:59212/MockAspirecnServices");
                    string result = client.Post(content, "uc.ashx");
                    Console.WriteLine("Posted: " + result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
            } while (true);
        }

        static void Main3(string[] args)
        {
            do
            {
                try
                {
                    using (Aspirecn.Entities.Cssp.CsspEntitiesContainer
                    entities = new Aspirecn.Entities.Cssp.CsspEntitiesContainer())
                    {
                        var c = entities.ServiceAccessReqEntities.Create();
                    }

                    //Database.SetInitializer<Aspirecn.Entities.Cssp.CsspEntities>(
                    //   new System.Data.Entity.DropCreateDatabaseIfModelChanges<
                    //        Aspirecn.Entities.Cssp.CsspEntities>());

                    //System.Data.Entity.DbModelBuilder builder =
                    //    new DbModelBuilder(DbModelBuilderVersion.Latest);
                    //builder.Build(

                    //Database.SetInitializer<Aspirecn.Entities.Cssp.CsspEntities>(
                    //   new System.Data.Entity.CreateDatabaseIfNotExists<
                    //        Aspirecn.Entities.Cssp.CsspEntities>());

                    //RegisterRoutes(RouteTable.Routes);
                    //string url = "http://localhost:59212/MockAspirecnServices/uc.ashx";

                    string content = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ServiceAccesssReq>
  <Head>
    <Dest_Address>
      <Address_Info>
        <DeviceID>cssp</DeviceID>
        <DeviceType>200</DeviceType>
      </Address_Info>
    </Dest_Address>
    <Send_Address>
      <Address_Info>
        <DeviceID>MOPPS</DeviceID>
        <DeviceType>106</DeviceType>
      </Address_Info>
    </Send_Address>
    <Version>1.0.0</Version>
    <MsgType>ServiceAccesssReq</MsgType>
  </Head>
  <Body>
    <Request type=""4"">
      <SPID>100060</SPID>
      <SPServiceID>1300001301</SPServiceID>
      <ChannelID>5</ChannelID>
      <ContentID>300000000025</ContentID>
      <MSISDN>13911118888</MSISDN>
      <FeeMSISDN>13911118888</FeeMSISDN>
      <Params>
        <Pager>
          <BeginIndex></BeginIndex>
          <EndIndex></EndIndex>
        </Pager>
        <Property>
          <ParamName>PayWay</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>1</ParamValue>
        </Property>
        <Property>
          <ParamName>ContentURL</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue></ParamValue>
        </Property>
        <Property>
          <ParamName>ProductCode</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>100000926100820100000008044300000004082</ParamValue>
        </Property>
        <Property>
          <ParamName>UA</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>NOKIA</ParamValue>
        </Property>
        <Property>
          <ParamName>OrderType</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>0</ParamValue>
        </Property>
        <Property>
          <ParamName>CPCode</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue></ParamValue>
        </Property>
        <Property>
          <ParamName>SalesChannelID</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>8899</ParamValue>
        </Property>
        <Property>
          <ParamName>ActionType</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>0</ParamValue>
        </Property>
        <Property>
          <ParamName>AppName</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>mmusic</ParamValue>
        </Property>
        <Property>
          <ParamName>Softplat</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>MM3.0.0.001.02_CTAndroid_JT</ParamValue>
        </Property>
        <Property>
          <ParamName>OndemandType</ParamName>
          <CompareOp>=</CompareOp>
          <ParamValue>1</ParamValue>
        </Property>
      </Params>
    </Request>
  </Body>
</ServiceAccesssReq>
";

                    RestClient client = new RestClient("http://localhost:59212/MockAspirecnServices");
                    string result = client.Post(content, "as.ashx");
                    Console.WriteLine("Posted: " + result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
            } while (true);
        }
    }
}
