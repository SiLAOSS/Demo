using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SiLA.Provider
{
    /// <summary>
    /// Contains some code to be executed in ExecuteAsync method
    /// </summary>
    /// <param name="evaluateOnly">if set to <c>true</c> the method must return the expected duration</param>
    /// <returns>
    /// SiLAReturnValue
    /// </returns>
    public delegate SiLAReturnValue SiLAAction(bool evaluateOnly);

    /// <summary>
    /// Describes a device
    /// </summary>
    public class SiLADevice
    {
        #region Members

        private object _lock = new object();

        #endregion //Members	
    
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLADevice"/> class.
        /// </summary>
        public SiLADevice()
        {
            this.SubStates = new List<CommandDescription>();
            this.Requests = new List<SiLARequest>();
        }
        
        #endregion //Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the device identification.
        /// </summary>
        public SiLA_DeviceIdentification DeviceIdentification { get; protected set; }

        /// <summary>
        /// Gets or sets the device state.
        /// </summary>
        public Status State { get; protected set; }

        /// <summary>
        /// Gets or sets the device sub states.
        /// </summary>
        public List<CommandDescription> SubStates { get; protected set; }

        /// <summary>
        /// Gets or sets the device ID.
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// Gets or sets the lock ID.
        /// </summary>
        public string LockID { get; protected set; }

        /// <summary>
        /// Gets or sets the PMS id.
        /// </summary>
        public string PMSid { get; protected set; }

        /// <summary>
        /// Gets or sets the event receiver URI.
        /// </summary>
        public string EventReceiverURI { get; protected set; }

        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        /// <value>
        /// The request id.
        /// </value>
        public int RequestId { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the device is in simulation mode.
        /// </summary>
        public bool SimulationMode { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the device is locked.
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return !string.IsNullOrEmpty(this.LockID);
            }
        }

        /// <summary>
        /// Gets or sets the duration of the lock.
        /// </summary>
        public TimeSpan LockDuration { get; protected set; }

        /// <summary>
        /// The time of last received SiLA request.
        /// </summary>
        protected DateTime LastRequestTime { get; set; }

        /// <summary>
        /// Gets or sets the time remaining when device is paused.
        /// </summary>
        protected TimeSpan WaitInterval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Abort is requested.
        /// </summary>
        protected bool AbortRequested { get; set; }

        /// <summary>
        ///A list of SiLA requests.
        /// </summary>
        protected List<SiLARequest> Requests { get; set; }

        #endregion //Properties

        #region Methods

        #region Reset

        /// <summary>
        /// Resets the SiLA Service Provider and changes State to State.standby
        /// </summary>
        /// <returns>SiLAReturnValue</returns>
        public SiLAReturnValue Reset()
        {
            this.State = Status.resetting;

            // All queues shall be emptied without sending response events of already started or queued commands
            this.Requests.Clear();
            this.Requests.Add(new SiLARequest(this.RequestId, this.LockID, "Reset"));

            // executes ResetDevice in a new thread
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, this.ResetDevice);

            return returnValue;
        }

        /// <summary>
        /// Resets the device.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> evaluate only.</param>
        /// <returns></returns>
        protected virtual SiLAReturnValue ResetDevice(bool evaluateOnly)
        {
            SiLAReturnValue returnValue;
            if (evaluateOnly)
            {
                // returns AsynchronousCommandAccepted
                returnValue = this.CreateReturnValue();
            }
            else
            {
                // returns AsynchronousCommandHasFinished
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished);
            }

            return returnValue;
        }

        #endregion //Reset

        #region Initialize

        /// <summary>
        /// Initializes the SiLA Service Provider and changes State from State.standby to State.idle
        /// </summary>
        /// <returns>SiLAReturnValue</returns>
        public SiLAReturnValue Initialize()
        {
            if (this.State != Status.standby)
            {
                // It shall only be possible to be invoked when the device is in the “standby” state.
                return new SiLAReturnValue(
                    (int)ReturnCode.CommandNotAllowedInThisState,
                    "Initialize only be possible to be invoked when the device is in the standby state",
                    Tools.ZeroDuration,
                    this.DeviceIdentification.SiLADeviceClass);
            }

            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, this.InitializeDevice);
            this.State = Status.resetting;
            return returnValue;
        }

        /// <summary>
        /// Initializes the device.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> [evaluate only].</param>
        /// <returns></returns>
        protected virtual SiLAReturnValue InitializeDevice(bool evaluateOnly)
        {
            SiLAReturnValue returnValue;
            if (evaluateOnly)
            {
                // returns AsynchronousCommandAccepted
                returnValue = this.CreateReturnValue();
            }
            else
            {
                // returns AsynchronousCommandHasFinished
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished);
            }

            return returnValue;
        }
        
        #endregion //Initialize

        #region Lock

        /// <summary>
        /// Locks the device for exclusive use,
        /// </summary>
        /// <param name="lockId">The lock id</param>
        /// <param name="PMSId">The PMS id</param>
        /// <param name="eventReceiverURI">The event receiver URI</param>
        /// <param name="lockTimeout">The lock timeout</param>
        /// <returns>SiLAReturnValue</returns>
        public virtual SiLAReturnValue Lock(string lockId, string PMSId, string eventReceiverURI, string lockTimeout)
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error",0);
                }

                this.LockDuration = TimeSpan.FromMilliseconds(0);
                this.LockID = lockId;
                this.PMSid = PMSid;
                this.EventReceiverURI = eventReceiverURI;
                this.LockDuration = Tools.ToTimeSpan(lockTimeout);

                // must unlock the device automatically
                // if lockTimeout parameter has value
                this.AutoUnlock();

                return this.CreateReturnValue();
            });

            return returnValue;
        }
        
        #endregion //Lock	    

        #region Unlock

        /// <summary>
        /// Unlocks the device
        /// </summary>
        /// <returns></returns>
        public virtual SiLAReturnValue Unlock()
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", 0);
                }

                this.LockDuration = TimeSpan.FromMilliseconds(0);
                this.LockID = string.Empty;
                return this.CreateReturnValue();
            });

            return returnValue;
        }
        
        #endregion //Unlock	    

        #region PrepareForReset

        /// <summary>
        /// Prepares for executing of Reset
        /// </summary>
        /// <param name="deviceId">The device id</param>
        /// <param name="PMSId">The PMS id</param>
        /// <param name="eventReceiverURI">The event receiver URI</param>
        /// <param name="timeout">The timeout</param>
        /// <param name="simulationMode">True if run in simulation mode</param>
        public void PrepareForReset(string deviceId, string PMSId, string eventReceiverURI, string timeout, bool simulationMode)
        {
            this.ID = deviceId;
            this.PMSid = PMSid;
            this.EventReceiverURI = eventReceiverURI;
            this.SimulationMode = simulationMode;
        }

        #endregion //PrepareForReset	
    
        #region ProcessRequest

        /// <summary>
        /// Processes the request
        /// </summary>
        /// <param name="requestId">The request id</param>
        /// <param name="lockId">The lock id</param>
        /// <param name="commandName">Name of the command.</param>
        /// <returns>
        /// SiLAReturnValue with ReturnCode.Success or ReturnCode.ErrorOnXXX code
        /// </returns>
        public SiLAReturnValue ProcessRequest(int requestId, string lockId, string commandName)
        {
            this.Requests.Add(new SiLARequest(requestId, lockId, commandName));

            this.LastRequestTime = DateTime.Now;


            SiLAReturnValue returnValue = this.CreateReturnValue();

            if (this.IsLocked && this.LockID != lockId)
            {
                returnValue.ReturnCode = (int)ReturnCode.ErrorOnLockId;
                returnValue.Message = "There is an error in LockId parameter.";
            }
            else
            {
                returnValue.ReturnCode = (int)ReturnCode.Success;
            }

            this.RequestId = requestId;

            Tools.WriteLogLine("Request for Device {0} [{6}]. RetVal: {7} ReqID:'{1}' Lock:'{2}' ID:'{3}' PMS:'{4}' EventURI:'{5}'",
                this.DeviceIdentification.DeviceName, requestId, lockId, this.ID, this.PMSid, this.EventReceiverURI, commandName, (ReturnCode)returnValue.ReturnCode);

            return returnValue;
        }

        #endregion //ProcessRequest	
    
        #region ExecuteAsync

        /// <summary>
        /// Creates the return value.
        /// </summary>
        /// <param name="retCode">The return code.</param>
        /// <param name="retText">The message.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        protected SiLAReturnValue CreateReturnValue(ReturnCode retCode = ReturnCode.AsynchronousCommandAccepted, string retText="No Error", int duration = 0)
        {
            SiLAReturnValue retValue;
            retValue = new SiLAReturnValue()
            {
                ReturnCode = (int)retCode,
                Message = retText,
                Duration = Tools.ToDuration(TimeSpan.FromMilliseconds(duration)),
                DeviceClass = this.DeviceIdentification.SiLADeviceClass,
            };

            return retValue;
        }

        /// <summary>
        /// Executes a method in a new thread
        /// </summary>
        /// <param name="requestId">The request id</param>
        /// <param name="action">Some action</param>
        /// <returns>
        /// SiLAReturnValue
        /// </returns>
        protected SiLAReturnValue ExecuteAsync(int requestId, SiLAAction action)
        {
            Tools.WriteLogLine("Executing {0} ", this.Requests.FirstOrDefault(r => r.RequestId == requestId));
            Thread thread = new Thread(() =>
            {
                Tools.WriteLogLine("... {0} thread", this.Requests.FirstOrDefault(r => r.RequestId == requestId));
                SiLAReturnValue retValue = action(false);
                Tools.WriteLogLine("... {0} completed", this.Requests.FirstOrDefault(r => r.RequestId == requestId));

                try
                {
                    SiLARequest request = this.Requests.FirstOrDefault(r => r.RequestId == requestId);
                    if (request != null)
                    {
                        Tools.WriteLogLine("... {0} ResponseEvent", this.Requests.FirstOrDefault(r => r.RequestId == requestId));
                        this.Requests.Remove(request);

                        string responseDataString = Tools.CreateXML(retValue.GetResponseData());

                        EventReceiverClient eventClient = new EventReceiverClient("EventReceiver_EventReceiver");
                        eventClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this.EventReceiverURI);
                        eventClient.ResponseEvent(requestId, retValue, responseDataString);

                        eventClient.Close();
                    }
                }
                catch (Exception ex)
                {
                    retValue.ReturnCode = (int)ReturnCode.FinishedWithWarning;
                    retValue.Message = ex.Message;
                    this.State = Status.errorHandling;
                    
                    Tools.WriteLogLine("ERROR: {0}", ex);
                    if (ex.InnerException != null)
                    {
                        Tools.WriteLogLine(" INNER: {0}", ex.InnerException);
                    }
                }
            });

             // sets result and return value to AsynchronousCommandAccepted
            SiLAReturnValue returnValue = action(true);           
            
            // starts the thread
            thread.Start();

            return returnValue;
        }

        #endregion //ExecuteAsync	
    
        #region AutoUnlock

        /// <summary>
        /// Starts a timer if lock duration was specified in LockDevice request
        /// </summary>
        protected void AutoUnlock()
        {
            if (!this.IsAutoUnLockEnabled())
            {
                return;
            }

            this.StartTimer();
        }

        #endregion //AutoUnlock	    

        #region Continue

        /// <summary>
        /// Enables the continuation of the process (workflow) for a paused system
        /// </summary>
        /// <returns>SiLAReturnValue</returns>
        public SiLAReturnValue Continue()
        {
            // The DoContinue command shall enable the continuation of the process (workflow) for a paused system. 
            // This means that paused asynchronous commands shall be continued and/or 
            // that the system shall start working on the next (queued) command in the process (workflow).

            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, this.ExecuteWork);

            return returnValue;
        }

        /// <summary>
        /// Executes the work.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> [evaluate only].</param>
        /// <returns></returns>
        protected virtual SiLAReturnValue ExecuteWork(bool evaluateOnly)
        {
            if (evaluateOnly)
            {
                return this.CreateReturnValue();
            }

            this.State = Status.idle;
            this.AbortRequested = false;
            return this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished);
        }

        #endregion //Continue	
    
        #region Abort

        /// <summary>
        /// Stops running 
        /// </summary>
        /// <returns>SiLAReturnValue</returns>
        public virtual SiLAReturnValue Abort()
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted, "No Error",0);
                }

                this.AbortRequested = true;
                return this.CreateReturnValue();
            });

            return returnValue; 
        }
        
        #endregion //Abort	
    
        #region Wait

        /// <summary>
        /// Pauses 
        /// </summary>
        /// <param name="duration">The duration</param>
        /// <returns>SiLAReturnValue</returns>
        public virtual SiLAReturnValue Pause(string duration = "")
        {
            this.WaitInterval = Tools.ToTimeSpan(duration);
            if (this.WaitInterval.Ticks == 0)
            {
                this.WaitInterval = TimeSpan.MaxValue;
            }

            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted, "No Error",0);
                }

                this.State = Status.paused;
                return this.CreateReturnValue();
            });

            return returnValue;
        }
        
        #endregion //Wait	    

        #region Delay

        /// <summary>
        /// Delay is used to suspend command execution 
        /// </summary>
        /// <param name="duration">The duration</param>
        /// <returns>SiLAReturnValue</returns>
        public virtual SiLAReturnValue Delay(string duration = "")
        {
            
            TimeSpan interval = Tools.ToTimeSpan(duration);

            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    this.State = Status.busy;
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted, "No Error", (int)interval.TotalMilliseconds);
                }

                Thread.Sleep(interval);

                this.State = Status.idle;

                return this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished, "No Error", 0);
            });

            return returnValue;
        }

        #endregion //Delay

        #region Private methods

        private bool IsItTimeToUnlcok()
        {
            if (this.IsAutoUnLockEnabled())
            {
                return (DateTime.Now - this.LastRequestTime) > this.LockDuration;
            }

            return false;
        }

        private bool IsAutoUnLockEnabled()
        {
            return this.IsLocked && this.LockDuration > TimeSpan.MinValue;
        }

        private void StartTimer()
        {
            Timer t = new Timer(new TimerCallback(TimerProc));
            TimeSpan interval = TimeSpan.FromMilliseconds(1000);
            t.Change(new TimeSpan(), interval);
        }

        private void TimerProc(object state)
        {
            Tools.WriteLogLine("....");
            // The state object is the Timer object.
            Timer t = (Timer)state;
            lock (_lock)
            {
                if (this.IsItTimeToUnlcok())
                {
                    this.Unlock();
                    this.Reset();
                    t.Dispose();
                }
                else if (!this.IsAutoUnLockEnabled())
                {
                    t.Dispose();
                }
            }
        }

        #endregion //Private methods

        #endregion //Methods
    }
}