namespace SiLA.Provider
{
    #region interface EventReceiver

    /// <summary>
    /// Event receiver
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://sila-standard.org", ConfigurationName = "EventReceiver")]
    public interface EventReceiver
    {

        /// <summary>
        /// Error event.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="continuationTask">The continuation task.</param>
        /// <returns></returns>
        [System.ServiceModel.OperationContractAttribute(Action = "http://sila-standard.org/ErrorEvent", ReplyAction = "http://sila-standard.org/ErrorEventResponse")]
        SiLAReturnValue ErrorEvent(int requestId, SiLAReturnValue returnValue, ref string continuationTask);

        /// <summary>
        /// Status event.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="eventDescription">The event description.</param>
        /// <returns></returns>
        [System.ServiceModel.OperationContractAttribute(Action = "http://sila-standard.org/StatusEvent", ReplyAction = "http://sila-standard.org/StatusEventResponse")]
        SiLAReturnValue StatusEvent(string deviceId, SiLAReturnValue returnValue, string eventDescription);

        /// <summary>
        /// Response event.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="responseData">The response data.</param>
        /// <returns></returns>
        [System.ServiceModel.OperationContractAttribute(Action = "http://sila-standard.org/ResponseEvent", ReplyAction = "http://sila-standard.org/ResponseEventResponse")]
        SiLAReturnValue ResponseEvent(int requestId, SiLAReturnValue returnValue, string responseData);

        /// <summary>
        /// Data event.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="dataValue">The data value.</param>
        /// <returns></returns>
        [System.ServiceModel.OperationContractAttribute(Action = "http://sila-standard.org/DataEvent", ReplyAction = "http://sila-standard.org/DataEventResponse")]
        SiLAReturnValue DataEvent(int requestId, string dataValue);
    }

    #endregion //interface EventReceiver	
    
    #region interface EventReceiverChannel

    /// <summary>
    /// Event receiver channel
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface EventReceiverChannel : EventReceiver, System.ServiceModel.IClientChannel
    {
    }

    #endregion //interface EventReceiverChannel	
            
    #region EventReceiverClient

    /// <summary>
    /// Event receiver client
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EventReceiverClient : System.ServiceModel.ClientBase<EventReceiver>, EventReceiver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiverClient"/> class.
        /// </summary>
        public EventReceiverClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiverClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public EventReceiverClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiverClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public EventReceiverClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiverClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public EventReceiverClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiverClient"/> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public EventReceiverClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        #endregion //Constructors	    

        #region ErrorEvent

        /// <summary>
        /// The error event shall be fired when the device enters an error state that may be recovered by executing a special task.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="continuationTask">The continuation task.</param>
        /// <returns></returns>
        public SiLAReturnValue ErrorEvent(int requestId, SiLAReturnValue returnValue, ref string continuationTask)
        {
            return base.Channel.ErrorEvent(requestId, returnValue, ref continuationTask);
        }

        #endregion //ErrorEvent	
    
        #region StatusEvent

        /// <summary>
        /// The Status event shall be triggered in an unsolicited fashion. It shall be used to report changes in the device, which are not controlled by the state machine, such as over temperature, errors in keep-alive mechanisms, or errors in motor regulation.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="eventDescription">The event description.</param>
        /// <returns></returns>
        public SiLAReturnValue StatusEvent(string deviceId, SiLAReturnValue returnValue, string eventDescription)
        {
            return base.Channel.StatusEvent(deviceId, returnValue, eventDescription);
        }

        #endregion //StatusEvent	
    
        #region ResponseEvent

        /// <summary>
        /// The response event shall inform the PMS about the successful completion of an asynchronous command.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="responseData">The response data.</param>
        /// <returns></returns>
        public SiLAReturnValue ResponseEvent(int requestId, SiLAReturnValue returnValue, string responseData)
        {
            return base.Channel.ResponseEvent(requestId, returnValue, responseData);
        }

        #endregion //ResponseEvent	
    
        #region DataEvent

        /// <summary>
        /// The data event shall enable the transmission of data generated during an asynchronous command execution to the PMS.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="dataValue">The data value.</param>
        /// <returns></returns>
        public SiLAReturnValue DataEvent(int requestId, string dataValue)
        {
            return base.Channel.DataEvent(requestId, dataValue);
        }

        #endregion //DataEvent
    }

    #endregion //EventReceiverClient	
}
