using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Aspirecn.Entities.UserCenter
{
    [DataContract]
    public class UserLogonReq
    {
        public string loginName
        {
            get;
            set;
        }

        public string userType
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }

        public string deviceId
        {
            get;
            set;
        }

        public string channelId
        {
            get;
            set;
        }

        public static UserLogonReq ParseToEntity(System.Xml.Linq.XElement requestElement)
        {
            UserLogonReq req = new UserLogonReq();

            IEnumerable<XElement> elements = requestElement.Descendants("parameter");

            /*
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
      <name>userType</name>
      <value>1</value>
</parameter>
  </body>
</req>
             */

            foreach (XElement e in elements)
            {
                try
                {
                    string name = e.Element("name").Value;
                    string value = e.Element("value").Value;
                    MapToEntity(req, name, value);
                }
                catch
                {
                    continue;
                }
            }

            return req;

            //XmlSerializer ser = new XmlSerializer(typeof(UserLogonReq));
            //StringReader reader = new StringReader(requestElement.ToString());
            //object obj = ser.Deserialize(reader);
            //if (obj != null && obj is UserLogonReq)
            //    return obj as UserLogonReq;

            //return null;
        }

        private static void MapToEntity(UserLogonReq req, string name, string value)
        {
            if (name.Equals("loginName", StringComparison.InvariantCultureIgnoreCase))
            {
                req.loginName = value;
            }

            if (name.Equals("userType", StringComparison.InvariantCultureIgnoreCase))
            {
                req.userType = value;
            }
            
            if (name.Equals("password", StringComparison.InvariantCultureIgnoreCase))
            {
                req.password = value;
            }

            if (name.Equals("deviceId", StringComparison.InvariantCultureIgnoreCase))
            {
                req.deviceId = value;
            }

            if (name.Equals("channelId", StringComparison.InvariantCultureIgnoreCase))
            {
                req.channelId = value;
            }

        }
    }
}
