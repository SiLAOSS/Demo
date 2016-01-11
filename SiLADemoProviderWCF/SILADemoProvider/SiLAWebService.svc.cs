using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SiLA.Provider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SiLAWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SiLAWebService.svc or SiLAWebService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(Namespace = "http://sila-standard.org")]
    public class SiLAWebService : ISiLAWebService
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

            string wsdl = string.Format("http://{0}:{1}/{2}.svc?singleWsdl",
                Properties.Settings.Default.Server, Properties.Settings.Default.Port, Properties.Settings.Default.ServiceName);

            this.Device.DeviceIdentification.Wsdl = wsdl;
        }

        #endregion //Constructor

        #region Mandatory
        
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
        
        #endregion
    }
}
