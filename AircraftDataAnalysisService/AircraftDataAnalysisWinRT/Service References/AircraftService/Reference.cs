﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18051
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 11.0.50727.1
// 
namespace AircraftDataAnalysisWinRT.AircraftService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AircraftModel", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class AircraftModel : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string CaptionField;
        
        private System.DateTime LastUsedField;
        
        private string ModelNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Caption {
            get {
                return this.CaptionField;
            }
            set {
                if ((object.ReferenceEquals(this.CaptionField, value) != true)) {
                    this.CaptionField = value;
                    this.RaisePropertyChanged("Caption");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastUsed {
            get {
                return this.LastUsedField;
            }
            set {
                if ((this.LastUsedField.Equals(value) != true)) {
                    this.LastUsedField = value;
                    this.RaisePropertyChanged("LastUsed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModelName {
            get {
                return this.ModelNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ModelNameField, value) != true)) {
                    this.ModelNameField = value;
                    this.RaisePropertyChanged("ModelName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AircraftService.IAircraftService")]
    public interface IAircraftService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftService/DoWork", ReplyAction="http://tempuri.org/IAircraftService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftService/GetAllAircraftModels", ReplyAction="http://tempuri.org/IAircraftService/GetAllAircraftModelsResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftService.AircraftModel>> GetAllAircraftModelsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftService/AddOrUpdateAircraft", ReplyAction="http://tempuri.org/IAircraftService/AddOrUpdateAircraftResponse")]
        System.Threading.Tasks.Task<string> AddOrUpdateAircraftAsync(AircraftDataAnalysisWinRT.AircraftService.AircraftModel aircraftModel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAircraftServiceChannel : AircraftDataAnalysisWinRT.AircraftService.IAircraftService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AircraftServiceClient : System.ServiceModel.ClientBase<AircraftDataAnalysisWinRT.AircraftService.IAircraftService>, AircraftDataAnalysisWinRT.AircraftService.IAircraftService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AircraftServiceClient() : 
                base(AircraftServiceClient.GetDefaultBinding(), AircraftServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IAircraftService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(AircraftServiceClient.GetBindingForEndpoint(endpointConfiguration), AircraftServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AircraftServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AircraftServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftService.AircraftModel>> GetAllAircraftModelsAsync() {
            return base.Channel.GetAllAircraftModelsAsync();
        }
        
        public System.Threading.Tasks.Task<string> AddOrUpdateAircraftAsync(AircraftDataAnalysisWinRT.AircraftService.AircraftModel aircraftModel) {
            return base.Channel.AddOrUpdateAircraftAsync(aircraftModel);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAircraftService)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAircraftService)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:45240/AircraftService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return AircraftServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IAircraftService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return AircraftServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IAircraftService);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_IAircraftService,
        }
    }
}
