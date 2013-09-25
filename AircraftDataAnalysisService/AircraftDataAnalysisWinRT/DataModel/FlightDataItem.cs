using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class FlightDataItem : DataCommon
    {
        public FlightDataItem(String uniqueId, String title, String subtitle,
            String imagePath, String description, String content, AircraftModelDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private AircraftModelDataGroup _group;
        public AircraftModelDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }
    }
}
