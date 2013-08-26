using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aspirecn.Entities.UserCenter
{
    [DataContract]
    public class user_info_schema
    {
        [DataMember]
        public string userId
        {
            get;
            set;
        }

        [DataMember]
        public string PuserId
        {
            get;
            set;
        }

        [DataMember]
        public string userType
        {
            get;
            set;
        }

        [DataMember]
        public string loginName
        {
            get;
            set;
        }

        [DataMember]
        public string bindMsisdn
        {
            get;
            set;
        }

        [DataMember]
        public int errorTimes
        {
            get;
            set;
        }

        [DataMember]
        public string lockTime
        {
            get;
            set;
        }

        [DataMember]
        public string nickName
        {
            get;
            set;
        }

        [DataMember]
        public string email
        {
            get;
            set;
        }

        [DataMember]
        public string headImage
        {
            get;
            set;
        }

        [DataMember]
        public string sex
        {
            get;
            set;
        }

        [DataMember]
        public string birthday
        {
            get;
            set;
        }

        [DataMember]
        public string Constellation
        {
            get;
            set;
        }

        [DataMember]
        public string work
        {
            get;
            set;
        }

        [DataMember]
        public string introduce
        {
            get;
            set;
        }

        [DataMember]
        public string favorite
        {
            get;
            set;
        }

        [DataMember]
        public string provinceId
        {
            get;
            set;
        }

        [DataMember]
        public string cityId
        {
            get;
            set;
        }

        [DataMember]
        public string address
        {
            get;
            set;
        }

        [DataMember]
        public string regTime
        {
            get;
            set;
        }

        [DataMember]
        public string regPlace
        {
            get;
            set;
        }

        [DataMember]
        public string lastLogonTime
        {
            get;
            set;
        }

        [DataMember]
        public string lastLogonPlace
        {
            get;
            set;
        }

        [DataMember]
        public string ludTime
        {
            get;
            set;
        }

        [DataMember]
        public string ludPlace
        {
            get;
            set;
        }

        [DataMember]
        public string wwwFirLogonCId
        {
            get;
            set;
        }

        [DataMember]
        public string wwwFirLogonTime
        {
            get;
            set;
        }

        [DataMember]
        public string wwwDeviceId
        {
            get;
            set;
        }

        [DataMember]
        public string moFirLogonCId
        {
            get;
            set;
        }

        [DataMember]
        public string moFirLogonTime
        {
            get;
            set;
        }
        
        [DataMember]
        public string moDeviceId
        {
            get;
            set;
        }

        [DataMember]
        public string wapFirLogonCId
        {
            get;
            set;
        }

        [DataMember]
        public string wapFirLogonTime
        {
            get;
            set;
        }

        [DataMember]
        public string wapDeviceId
        {
            get;
            set;
        }

        [DataMember]
        public int payPasswordStatus
        {
            get;
            set;
        }

        [DataMember]
        public int BlogStat139
        {
            get;
            set;
        }

        [DataMember]
        public int BlogUId139
        {
            get;
            set;
        }

        [DataMember]
        public string FollowAp
        {
            get;
            set;
        }

        [DataMember]
        public int paySwitchStatus
        {
            get;
            set;
        }

        [DataMember]
        public string personalSign
        {
            get;
            set;
        }

        [DataMember]
        public int privacySet
        {
            get;
            set;
        }
    }
}
