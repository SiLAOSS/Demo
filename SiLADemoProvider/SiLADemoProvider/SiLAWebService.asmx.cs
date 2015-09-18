using System;
using System.Web.Services;

namespace SiLA.Provider
{
    /// <summary>
    /// summary description for SiLAWebService
    /// </summary>
    [WebService(Namespace = "http://sila-standard.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SiLAWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// Gets or sets the device
        /// </summary>
        protected IncubatorSiLADevice Device { get; set; }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLAWebService"/> class
        /// </summary>
        public SiLAWebService()
        {
            this.Device = SiLADeviceFactory.ConnectTo<IncubatorSiLADevice>(Properties.Settings.Default.InstrumentName);

            string wsdl = string.Format("http://{0}:{1}/{2}.asmx?wsdl",
                Properties.Settings.Default.Server, Properties.Settings.Default.Port, Properties.Settings.Default.ServiceName);

            this.Device.DeviceIdentification.Wsdl = wsdl;
        }
        
        #endregion //Constructor	    

        #region Mandatory
       
        ///<SiLACommandDescription xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" isCommonCommand="true" estimatedDuration="PT0S" xsi:noNamespaceSchemaLocation="http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd">
        ///   <summary>This command aborts all running and pending asynchronous commands of the device.</summary>
        ///   <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        ///   <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        ///   <response xsi:type="standardResponse" parameterSetCount="0">
        ///      <description>Empty response</description>
        ///   </response>
        ///</SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.AbortDescription)]
        public SiLAReturnValue Abort(int requestId, string lockId)
        {
            // The abort command shall stop all running and queued asynchronous commands (busy state). 
            // If no error occurs, the device shall be in the idle state. 
            // After completion of the abort command no response events 
            // for the aborted asynchronous commands shall be fired any more. 
           
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "Abort");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.Abort();
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary>
        /// This command enables the continuation of the process for a paused system.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId" >This parameter is the identification of the PMS, which has locked the device.</param>
        /// <response xsi:type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.DoContinueDescription)]
        public SiLAReturnValue DoContinue(int requestId, string lockId)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "DoContinue");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.Continue();
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command reports on details of the device.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        /// <param name="deviceDescription">
        /// The Device Identification. It is a SOAP complex type.
        /// </param>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.GetDeviceIdentificationDescription)]
        public SiLAReturnValue GetDeviceIdentification(int requestId, string lockId, out SiLA_DeviceIdentification deviceDescription)
        {
            SiLAReturnValue returnValue = new SiLAReturnValue(
                (int)ReturnCode.Success,
                this.Device.DeviceIdentification.DeviceName + " GetDeviceIdentification",
                "PT0.7S",
                this.Device.DeviceIdentification.SiLADeviceClass);

            deviceDescription = this.Device.DeviceIdentification;
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command reports the status of the device.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="deviceID">The identification the device returns to identify itself at the PMS.</param>
        /// <param name="state">Status of the device</param>
        /// <param name="substates">Substate of the above state.</param>
        /// <param name="locked">Lock state of the device (locked=true, unlocked = false)</param>
        /// <param name="PMSId">Identification of the PMS that locked the device.</param>
        /// <param name="currentTime">Time of reporting status information.</param>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.GetStatusDescription)]
        public SiLAReturnValue GetStatus(int requestId, out string deviceID, out Status state, out CommandDescription[] substates, out Boolean locked, out string PMSId, out DateTime currentTime)
        {
            
            SiLAReturnValue returnValue = new SiLAReturnValue(
                (int)ReturnCode.Success,
                this.Device.DeviceIdentification.DeviceName + "device status is Success",
                Tools.ZeroDuration,
                this.Device.DeviceIdentification.SiLADeviceClass);

            state = this.Device.State;
            deviceID = this.Device.ID;
            substates = null;
            locked = this.Device.IsLocked;
            PMSId = this.Device.PMSid;
            currentTime = DateTime.Now;

            // As value for the subStates null will be returned, 
            // if the device is in the state startup, standby, or idle. 
            if (this.Device.State == Status.startup
                || this.Device.State == Status.standby
                || this.Device.State == Status.idle)
            {
                substates = null;
            }
            else
            {
                substates = this.Device.SubStates.ToArray();
            }

            return returnValue;
        }



        ///<SiLACommandDescription xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" isCommonCommand="true" estimatedDuration="PT0S" xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        ///   <summary>This command initializes the device.</summary>
        ///   <param name="requestId" minValue="1" maxValue="2147483647">This parameter ist the unique identification of this command call.</param>
        ///   <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        ///   <response xsi:type="standardResponse" parameterSetCount="0">
        ///      <description>Empty response</description>
        ///   </response>
        ///</SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.InitializeDescription)]
        public SiLAReturnValue Initialize(int requestId, string lockId)
        {             
            // This command shall initialize the SiLA Service Provider. 
            // It shall only be possible to be invoked when the device is in the “standby” state.             

            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "Initialize");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.Initialize();

            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command locks the device for exclusive use.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId" >This parameter hands over the lock identification of the PMS to the device. The device will only accept further commands, if they use the same lockId.</param>
        /// <param name="lockTimeout" xsi:type="Duration">If this parameter is omitted, no timeout will be set. Otherwise the device will unlock itself if it does not receive any commands within the timeout period.</param>
        /// <param name="eventReceiverURI" >Service URI of the Service Consumer’s event Receiver (used in case of timeout that will issue a reset with it).</param>
        /// <param name="PMSId" >Id of the PMS in order to identify the PMS that locked a device.</param>
        /// <response xsi:type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.LockDeviceDescription)]
        public SiLAReturnValue LockDevice(int requestId, string lockId, string lockTimeout, string eventReceiverURI, string PMSId)
        {
            // If the Service Consumer wants to lock a device for exclusive use, 
            // it shall invoke a LockDevice command with a generated String as value of the parameter “lockId”. 
            // Subsequently the Service Provider shall only accept commands from a Service Consumer, 
            // which uses the same lockId in the command calls.
            // The SiLA Service Consumer MUST ensure the uniqueness of the lockId to avoid conflicts with other SiLA Service Consumers.
            // The lockTimeout parameter is used to unlock the device automatically 
            // after the indicated time period has passed, without receiving commands, 
            // by executing a Reset and the UnlockDevice commands internally on the device. 
            // The device will then be in the state Standby. 
            // If the lockTimeout parameter is 0 or Null, then the locking is permanent until the next UnlockDevice or a power shutdown.

            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "LockDevice");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // lock error
                return returnValue;
            }

            returnValue = this.Device.Lock(lockId, PMSId, eventReceiverURI, lockTimeout);

            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command pauses the process/workflow in order to enable user intervention.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId">This parameter is the identification of the PMS, which has locked the device.</param>
        /// <response xsi:type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.PauseDescription)]
        public SiLAReturnValue Pause(int requestId, string lockId)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "Pause");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.Pause();
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command is used to reset the Service Provider at any time from any state.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId" >This parameter is the identification of the PMS, which has locked the device.</param>
        /// <param name="deviceId" >The identification the device returns to identify itself at the PMS.</param>
        /// <param name="eventReceiverURI" >Connection information of the Service Consumers event Receiver.</param>
        /// <param name="PMSId" >Id of the PMS in order to identify the PMS that locked a device.</param>
        /// <param name="errorHandlingTimeout" xsi:type="Duration">Timeout until an errorhandling state is changed into an error state.</param>
        /// <param name="simulationMode" >Selects simulation mode.</param>
        /// <response xsi:type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.ResetDescription)]
        public SiLAReturnValue Reset(int requestId, string lockId, string deviceId, string eventReceiverURI, string PMSId, string errorHandlingTimeout, Boolean simulationMode)
        {            
            // This command shall reset the SiLA Service Provider. 
            // All queues shall be emptied without sending response events of already started or queued commands. 
            // It shall be callable at any time and from any state of the devices state machine. 
            // It shall also be used to report the connection information of the service consumer’s event receiver available to the 
            // SiLA Service Provider (or to overwrite it in subsequent calls). 
            // Additionally sends the deviceId to the Service Provider so that the service 
            // Provider can identify itself at the Service Consumer in case of a status event.             

            this.Device.PrepareForReset(deviceId, PMSId, eventReceiverURI, errorHandlingTimeout, simulationMode);
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "Reset");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // lock error
                return returnValue;
            }

            returnValue = this.Device.Reset();

            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S"
        /// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        /// xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        /// <summary> 
        /// This command is used to unlock the device.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        /// <response xsi:type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.UnlockDeviceDescription)]
        public SiLAReturnValue UnlockDevice(int requestId, string lockId)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "UnlockDevice");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // lock error
                return returnValue;
            }

            returnValue = this.Device.Unlock();
            return returnValue;
        }
       
        #endregion //Mandatory	    

        #region Required

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S">
        /// <summary> 
        /// The Delay command is used to suspend command execution..
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647"> This parameter is the unique identification of this command call.</param>
        /// <param name="lockId"> This parameter is the identification of the SiLA Service Consumer, which has locked the device.</param>
        /// <param name="time"> This parameter defines suspend duration.</param>
        /// <response type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.DelayDescription)]
        public SiLAReturnValue Delay(int requestId, string lockId, string time)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "Delay");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.Delay(time);
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S">
        /// <summary> 
        /// This command is used to retrieve the available parameter values of the device.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        /// <response type="standardResponse" parameterSetCount="1">
        /// <description>
        /// Returns the parameter set of the device as parameterName, value pair.
        /// </description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.GetParametersDescription)]
        public SiLAReturnValue GetParameters(int requestId, string lockId)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "GetParameters");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.GetParameters();
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S">
        /// <summary> 
        /// This command is used to send new parameter values to the device.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        /// <param name="lockId">This parameter is the identification of the PMS, which has locked the device.</param>
        /// <param name="paramsXML">The xml Document of paramsXML is formatted as stated in the ParameterSet tag.
        /// <ParameterSet>
        /// </ParameterSet>
        /// </param>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.SetParametersDescription)]
        public SiLAReturnValue SetParameters(int requestId, string lockId, string paramsXML)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "SetParameters");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.SetParameters(paramsXML);
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S">
        /// <summary> 
        /// This command is used to store a labware item at a predefined position.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647"> This parameter is the unique identification of this command call.</param>
        /// <param name="lockId"> This parameter is the identification of the PMS, which has locked the device.</param>
        /// <param name="position"> The position parameter is used to specify at which position the labware shall be stored.</param>
        /// <param name="labwareType"> The labwareType parameter may be used to transfer information about the labware that the device might need to access it correctly</param>
        /// <response type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.StoreAtPositionDescription)]
        public SiLAReturnValue StoreAtPosition(int requestId, string lockId, int position, string labwareType)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "StoreAtPosition");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.StoreAtPosition(position, labwareType);
            return returnValue;
        }

        /// <SiLACommandDescription isCommonCommand="true" estimatedDuration="PT0S">
        /// <summary>
        /// This command is used to retrieve a labware item by specifying the position identification.
        /// </summary>
        /// <param name="requestId" minValue="1" maxValue="2147483647"> This parameter is the unique identification of this command call.</param>
        /// <param name="lockId"> This parameter is the identification of the PMS, which has locked the device.</param>
        /// <param name="position"> The position parameter is used to specify which labware item shall be retrieved.</param>
        /// <param name="labwareType"> The labwareType parameter may be used to transfer information about the labware that the device might need to access it correctly</param>
        /// <response type="standardResponse" parameterSetCount="0">
        /// <description>Empty response</description>
        /// </response>
        /// </SiLACommandDescription>
        [WebMethod(Description = SiLAWebMethodDescriptions.RetrieveByPositionIdDescription)]
        public SiLAReturnValue RetrieveByPositionId(int requestId, string lockId, int position, string labwareType)
        {
            SiLAReturnValue returnValue = this.Device.ProcessRequest(requestId, lockId, "RetrieveByPositionId");
            if (returnValue.ReturnCode != (int)ReturnCode.Success)
            {
                // invalid request or lock error
                return returnValue;
            }

            returnValue = this.Device.RetrieveByPositionId(position, labwareType);
            return returnValue;
        } 

        #endregion //Required
    }
}
