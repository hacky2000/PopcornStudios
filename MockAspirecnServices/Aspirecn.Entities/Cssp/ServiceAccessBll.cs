using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspirecn.Entities.Cssp
{
    public class ServiceAccessBll
    {
        public ServiceAccesssResp GetResp(ServiceAccesssReq req)
        {
            if (req != null)
            {
                ServiceAccesssReqBody body = null;
                ServiceAccesssReqHead head = null;
                foreach (var item in req.Items)
                {
                    if (item != null && item is ServiceAccesssReqBody)
                        body = item as ServiceAccesssReqBody;
                    else if (item != null && item is ServiceAccesssReqHead)
                        head = item as ServiceAccesssReqHead;
                }

                using (Aspirecn.Entities.Cssp.CsspEntitiesContainer entities
                     = new CsspEntitiesContainer())
                {
                    ServiceAccessReqEntity reqEntity = AddRequestRecords(body, head, entities);
                    Aspirecn.Entities.Cssp.ServiceAccessRespEntity resp
                        = AddResponseRecords(body, head, entities, reqEntity);

                    entities.ServiceAccessReqEntities.Add(reqEntity);

                    entities.SaveChanges();
                    return this.ConvertToResponse(resp, req, head, body);
                }
            }

            return null;
        }

        private ServiceAccesssResp ConvertToResponse(ServiceAccessRespEntity resp, ServiceAccesssReq req, ServiceAccesssReqHead head, ServiceAccesssReqBody body)
        {
            ServiceAccesssResp response = new ServiceAccesssResp()
            {
                Items =
                    new object[]{
                     new ServiceAccesssRespHead()
                     {  Dest_Address = head.Dest_Address,
                          Send_Address = head.Send_Address,
                           TransactionID = resp.Head.TransactionID,
                            Version = resp.Head.Version
                     }
                     , new ServiceAccesssRespBody()
                     {  Response = 
                         new ServiceAccesssRespBodyResponse[]
                         { 
                             new ServiceAccesssRespBodyResponse()
                             {
                                 OrderID = resp.Body.OrderID,
                                  Price = resp.Body.Price,
                                   PushID = resp.Body.PushID,
                                    RetCode = resp.Body.RetCode,
                                     SPID = resp.Body.SPID,
                                      SPServiceID = resp.Body.SPServiceID
                             }
                         }
                     }
                }
            };


            return response;
        }

        private Aspirecn.Entities.Cssp.ServiceAccessRespEntity AddResponseRecords(
            ServiceAccesssReqBody body,
            ServiceAccesssReqHead head, CsspEntitiesContainer entities,
            ServiceAccessReqEntity reqEntity)
        {
            int tranID = this.GenerateRandomNumber();
            string orderpushid = this.GenerateOrderPushIDStr(tranID);

            Aspirecn.Entities.Cssp.ServiceAccessRespEntity resp =
                new ServiceAccessRespEntity()
                {
                    Head = new RespHead()
                    {
                        Dest_Address =
                            reqEntity.Head.Dest_Address,
                        Send_Address = reqEntity.Head.Send_Address,
                        Version = "1.0.0",
                        TransactionID = tranID.ToString()
                    },
                    Body = new ResponseType()
                        {
                            Price = "0.01",
                            RetCode = "0",
                            SPID = reqEntity.Body.Request.SPID,
                            SPServiceID = reqEntity.Body.Request.SPServiceID,
                            OrderID = orderpushid,
                            PushID = orderpushid
                        },
                    // ServiceAccessReqEntity = reqEntity
                };
            //resp.ServiceAccessReqEntity = reqEntity;
            //reqEntity.ServiceAccessRespEntity = resp;
            //entities.ServiceAccessRespEntities.Add(resp);

            return resp;
        }

        private string GenerateOrderPushIDStr(int tranID)
        {
            string prefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            string suffix = tranID.ToString("D6");
            return prefix + suffix;
        }

        private int GenerateRandomNumber()
        {
            Random ran = new Random(System.Environment.TickCount);
            return ran.Next(999999);
        }

        private static Aspirecn.Entities.Cssp.ServiceAccessReqEntity AddRequestRecords(
            ServiceAccesssReqBody body, ServiceAccesssReqHead head, 
            Aspirecn.Entities.Cssp.CsspEntitiesContainer entities)
        {
            Aspirecn.Entities.Cssp.ServiceAccessReqEntity requestEntity =
                new ServiceAccessReqEntity()
                {
                    Head = new Head()
                    {
                        Send_Address = new Address_Info_Schema()
                        {
                            DeviceID = head.Send_Address[0].DeviceID,
                            DeviceType = head.Send_Address[0].DeviceType
                        },
                        Dest_Address = new Address_Info_Schema()
                        {
                            DeviceID = head.Dest_Address[0].DeviceID,
                            DeviceType = head.Dest_Address[0].DeviceType
                        },
                        MsgType = head.MsgType,
                        Version = head.Version
                    },
                    Body = new Body()
                    {
                        Request = new RequestType()
                        {
                            ChannelID = body.Request[0].ChannelID,
                            ContentID = body.Request[0].ContentID,
                            FeeMSISDN = body.Request[0].FeeMSISDN,
                            MSISDN = body.Request[0].MSISDN,
                            SPID = body.Request[0].SPID,
                            SPServiceID = body.Request[0].SPServiceID,
                            Params = new ParamsType()
                            {
                                Pager = new PagerType()
                                {
                                    BeginIndex = body.Request[0].Params[0].Pager[0].BeginIndex,
                                    EndIndex = body.Request[0].Params[0].Pager[0].EndIndex
                                }
                            }
                        }
                    }, 
                };
            //entities.ServiceAccessReqEntities.Add(requestEntity);

            List<Aspirecn.Entities.Cssp.ParamNameEntity> paramNameEntities =
                new List<Aspirecn.Entities.Cssp.ParamNameEntity>();

            foreach (var one in body.Request)
            {
                foreach (var two in one.Params)
                {
                    foreach (var three in two.Property)
                    {
                        var ent = new ParamNameEntity();//entities.ParamNameEntities.Create();

                       // ent.ServiceAccessReqEntity = requestEntity;
                        ent.ParamName = three.ParamName;
                        ent.CompareOp = three.CompareOp;
                        ent.ParamValue = three.ParamValue;

                       // requestEntity.Properties.Add(ent);
                       // paramNameEntities.Add(ent);
                    }
                    break;
                }
                break;
            }

            //foreach (var p in paramNameEntities)
            //{
            //    entities.ParamNameEntities.Add(p);
            //}

            // entities.SaveChanges();

            return requestEntity;
        }
    }
}
