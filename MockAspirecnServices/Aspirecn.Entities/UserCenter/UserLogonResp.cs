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
    public class UserLogonResp
    {
        [DataMember]
        public int hRet
        {
            get;
            set;
        }

        // [DataMember]
        public t_user_info user_info
        {
            get;
            set;
        }

        public string ToXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(UserLogonResp));
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            ser.Serialize(writer, this);
            writer.Flush();
            writer.Close();
            string xml = builder.ToString();

            XElement e = XElement.Parse(xml);

            return e.ToString();

            //return xml;
        }
    }
}
