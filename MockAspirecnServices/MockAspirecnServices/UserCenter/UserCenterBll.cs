using Aspirecn.Entities.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockAspirecnServices.UserCenter
{
    public class UserCenterBll
    {
        public UserLogonResp RequestLogon(UserLogonReq request)
        {
            UserLogonResp response = new UserLogonResp()
            {
                hRet = 0,
                user_info = new user_info_schema()
                {
                    userId = request.loginName,
                    loginName = request.loginName
                }
            };

            return response;
        }
    }
}