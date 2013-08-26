using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspirecn.Entities.UserCenter
{
    public class UserLogonBll
    {
        public UserLogonResp DoWithUserLogon(UserLogonReq request)
        {
            using (AspirecnUserCenterEntities context = new AspirecnUserCenterEntities())
            {
                var users = from one in context.t_user_info
                            where one.LoginName == request.loginName
                            && one.Password == request.password
                            select one;

                if (users != null && users.Count() > 0)
                {
                    var user = users.First();
                    if (user != null)
                    {
                        return this.ParseToUserLogonResp(user);
                    }
                }
            }

            return new UserLogonResp() { hRet = -1 };

           // return null;
        }

        private UserLogonResp ParseToUserLogonResp(t_user_info user)
        {
            UserLogonResp resp = new UserLogonResp()
            {
                hRet = 0,
                user_info = user,
            };
            return resp;
        }
    }
}
