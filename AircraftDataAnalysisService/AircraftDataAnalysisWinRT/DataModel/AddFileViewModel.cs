using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.DataModel
{
    public class AddFileViewModel : DataCommon
    {
        private Windows.Storage.StorageFile file;

        public AddFileViewModel(Windows.Storage.StorageFile file)
            : base("addfile", "导入文件", "导入文件", string.Empty, "从磁盘选择一个文件导入")
        {
            // TODO: Complete member initialization
            this.file = file;

            this.CurrentFileName = this.file.Name;

            this.InitLoadHeader();
        }

        private async void InitLoadHeader()
        {
            if (this.file != null)
            {
                var readFile = await this.file.OpenReadAsync();
                var stream = readFile.AsStreamForRead();
                BinaryReader reader = new BinaryReader(stream);

                FlightDataEntitiesRT.PHYHeader header = FlightDataEntitiesRT.PHYHelper.ReadPHYHeader(reader);

                this.BindHeader(header);
            }
        }

        private string m_description = string.Empty;

        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                this.SetProperty<string>(ref m_description, value);
            }
        }

        private Windows.UI.Xaml.Visibility m_importDataVisibility = Windows.UI.Xaml.Visibility.Collapsed;

        public Windows.UI.Xaml.Visibility ImportDataVisibility
        {
            get { return m_importDataVisibility; }
            set
            {
                this.SetProperty<Windows.UI.Xaml.Visibility>(ref m_importDataVisibility, value);
            }
        }

        private FlightDataEntitiesRT.PHYHeader m_header = null;

        private void BindHeader(FlightDataEntitiesRT.PHYHeader header)
        {
            this.m_header = header;

            if (m_header != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(m_header.AircrfName);
                builder.AppendLine(m_header.AircrfNum);
                builder.AppendLine(m_header.BTime.ToString());
                builder.AppendLine(m_header.EndTime.ToString());
                builder.AppendLine(m_header.FlyPlanAddr.ToString());
                builder.AppendLine(m_header.FlySeconds.ToString());
                builder.AppendLine(m_header.GPSEndTime.ToString());
                builder.AppendLine(m_header.GPSStartTime.ToString());
                builder.AppendLine(m_header.OPName);
                builder.AppendLine(m_header.ParaListAddr.ToString());
                builder.AppendLine(m_header.PhyValueAddr.ToString());
                builder.AppendLine(m_header.PhyValueEndAddr.ToString());
                builder.AppendLine(m_header.PNum.ToString());
                builder.AppendLine(m_header.StartTime.ToString());
                builder.AppendLine(m_header.SWNum.ToString());

                this.Description = builder.ToString();

                this.UniqueId = m_header.AircrfName + "/" + m_header.AircrfNum;
                //Example: F4D/0004
            }
            else
            {
                this.UniqueId = string.Empty;
            }

            if (this.m_header != null)
                this.ImportDataVisibility = Windows.UI.Xaml.Visibility.Visible;
            else this.ImportDataVisibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private string m_currentFileName = string.Empty;
        public string CurrentFileName
        {
            get { return this.m_currentFileName; }
            set { this.SetProperty(ref this.m_currentFileName, value); }
        }

        internal void ImportData()
        {
            throw new NotImplementedException();
        }
    }
}
