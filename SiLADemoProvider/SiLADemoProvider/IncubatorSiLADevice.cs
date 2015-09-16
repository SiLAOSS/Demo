using System;
using System.Collections.Generic;
using System.Threading;

namespace SiLA.Provider
{
    /// <summary>
    /// Incubator SiLA device
    /// </summary>
    public class IncubatorSiLADevice : SiLADevice
    {
        #region Members
       
        #endregion //Members

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="IncubatorSiLADevice"/> class.
        /// </summary>
        public IncubatorSiLADevice()
        {
            this.DeviceIdentification = new SiLA_DeviceIdentification
            {
                DeviceFirmwareVersion = "1.1.100",
                DeviceManufacturer = "Promega",
                DeviceName = "Hatcher",
                DeviceSerialNumber = "H-12",
                SiLADeviceClass = (int)DeviceClass.Incubator,
                SiLADeviceClassVersion = "1.1",
                SiLAInterfaceVersion = "1.1",
                SiLASubDeviceClass = null
            };

            this.ID = this.DeviceIdentification.DeviceName + " " + this.DeviceIdentification.DeviceSerialNumber;
            this.State = Status.startup;
            this.Steps = new List<int>();
        }
        #endregion //Constructor

        #region Properties

        /// <summary>
        /// A set of steps executed by IncubatorSiLADevice
        /// </summary>
        /// <seealso cref="ExecuteWork"/> method
        protected List<int> Steps { get; set; }

        #endregion //Properties

        #region Public methods

        /// <summary>
        /// With this command the device reports the available parameters that can be set during the idle state to the PMS. 
        /// The content of the response data is ParameterSet that contains a list of device parameters.
        /// </summary>
        /// <returns>SiLAReturnValue</returns>
        public SiLAReturnValue GetParameters()
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted, "No Error",100);
                }

                SiLAReturnValue retParam =  this.CreateReturnValue();

                // ResponseData contains a list of device parameters
                ResponseData responseData = this.GetIncubatorParameters();
                retParam.SetResponseData(responseData);

                return retParam;
            });

            return returnValue;
        }

        /// <summary>
        /// Gets the incubator parameters.
        /// </summary>
        /// <returns>ResponseData that contains a list of device parameters</returns>
        private ResponseData GetIncubatorParameters()
        {
            ResponseData data = new ResponseData()
            {
                Items = new List<object>()
            };

            Parameter temperatureParameter = new Parameter()
            {
                 name="Temperature",
                 parameterType = AllowedType.Int32,
                ItemElementName = ItemChoiceType.Int32,
                Item = 33
            };

            Parameter timeParameter = new Parameter()
            {
                name = "TimeInSeconds",
                parameterType = AllowedType.Int32,
                ItemElementName = ItemChoiceType.Int32,
                Item = 1500
            };
            
            ParameterSet parameterSet = new ParameterSet();
            parameterSet.Parameter = new List<Parameter>();
            parameterSet.Parameter.Add(temperatureParameter);
            parameterSet.Parameter.Add(timeParameter);
            
            data.Items.Add(parameterSet);

            return data;
        }

        /// <summary>
        /// The SetParameters command is used to set device class and device specific parameters in the state idle so that the new values are used for the next command invocation.
        /// Only the parameters that need to be changed have to be transmitted to the device.
        /// </summary>
        /// <param name="paramsXML">ParameterSet XML</param>
        /// <returns>SiLAReturnValue</returns>
        public SiLAReturnValue SetParameters(string paramsXML)
        {
            // Example of a valid value for paramsXML:
            //<?xml version="1.0" encoding="utf-8"?>
            //<ParameterSet>
            //  <Parameter name="Temperature">
            //    <Int32>33</Int32>
            //  </Parameter>
            //  <Parameter name="TimeInSeconds">
            //    <Int32>1500</Int32>
            //  </Parameter>
            //</ParameterSet>

            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error",100);
                }

                // The paramsXML represents a ParameterSet
                ParameterSet data = Tools.FromXML<ParameterSet>(paramsXML);
                SiLAReturnValue retParam = this.CreateReturnValue();
                
                // ResponseData contains a list of device parameters
                ResponseData responseData = this.GetIncubatorParameters();
                retParam.SetResponseData(responseData);

                return retParam;
            });

            return returnValue;
        }

        /// <summary>
        /// With this command the labware item is stored at the defined position in the device.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="labwareType">Type of the labware.</param>
        /// <returns></returns>
        public SiLAReturnValue StoreAtPosition(int position, string labwareType)
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                int duration = 1200;
                if (noexec)
                {
                    // calculates the estimated duration and returns AsynchronousCommandAccepted
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error",duration);
                }
                
                // performs a demo action
                Thread.Sleep(duration);

                // calculates the estimated duration and returns AsynchronousCommandAccepted
                return this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished,"No Error",duration);
            }); 
            
            return returnValue;
        }

        /// <summary>
        /// The RetrieveByPositionId enables the retrieval of labware by means of the position identification.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="labwareType">Type of the labware.</param>
        /// <returns></returns>
        public SiLAReturnValue RetrieveByPositionId(int position, string labwareType)
        {
            SiLAReturnValue returnValue = this.ExecuteAsync(this.RequestId, (noexec) =>
            {
                int duration = 550;
                if (noexec)
                {
                    return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", duration);
                }

                // performs a demo action
                Thread.Sleep(duration);

                return this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished,"No Error", duration);
            });

            return returnValue;
        }

        #endregion //Public methods

        #region Protected methods

        /// <summary>
        /// Resets the device.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> evaluate only.</param>
        /// <returns></returns>
        protected override SiLAReturnValue ResetDevice(bool evaluateOnly)
        {
            SiLAReturnValue returnValue;
            int resetTimeInMs = 2400;
            if (evaluateOnly)
            {
                // sets result and return value to AsynchronousCommandAccepted
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", resetTimeInMs);
            }
            else
            {
                // performs a demo action
                Thread.Sleep(resetTimeInMs);
                this.State = Status.standby;

                // sets result and return value to AsynchronousCommandHasFinished
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished,"No Error", resetTimeInMs);
            }

            return returnValue;
        }

        /// <summary>
        /// Initializes the device.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> [evaluate only].</param>
        /// <returns></returns>
        protected override SiLAReturnValue InitializeDevice(bool evaluateOnly)
        {
            SiLAReturnValue returnValue;
            int duration = 1250;
            if (evaluateOnly)
            {
                // sets result and return value to AsynchronousCommandAccepted
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", duration);
            }
            else
            {
                // performs a demo action
                Thread.Sleep(1250);
                this.State = Status.idle;

                // sets result and return value to AsynchronousCommandHasFinished
                returnValue = this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished,"No Error", duration);
            }

            return returnValue;
        }

        /// <summary>
        /// Executes the work.
        /// </summary>
        /// <param name="evaluateOnly">if set to <c>true</c> evaluate only.</param>
        /// <returns></returns>
        protected override SiLAReturnValue ExecuteWork(bool evaluateOnly)
        {
            int stepsCount = SiLA.Provider.Properties.Settings.Default.DemoStepsCount;
            int stepDuration = SiLA.Provider.Properties.Settings.Default.DemoStepDuration;
            bool isStarted = Steps.Count > 0;
            bool isPaused = this.State == Status.paused;

            int duration = isStarted ? Steps.Count * stepDuration : stepsCount * stepDuration;

            Tools.WriteLogLine("... ExecuteWork - {0}. {1} steps remaining.", this.State, Steps.Count);

            if (evaluateOnly)
            {
                // accepts the command and return the expected duration
                return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", duration);
            }

            // continue
            this.State = Status.busy;

            if (isStarted && isPaused)
            {
                return this.CreateReturnValue(ReturnCode.AsynchronousCommandAccepted,"No Error", duration);
            }

            if (!isStarted)
            {
                // initializes workflow tasks (20 seconds simulation)
                for (int i = 0; i < stepsCount; i++)
                {
                    Steps.Add(stepDuration);
                } 
                
                TimeSpan pauseInterval = TimeSpan.FromMilliseconds(250);
                while (Steps.Count > 0)
                {
                    this.SubStates.Add(new CommandDescription
                    {
                        currentState = Substate.processing,
                        commandName = "Step #" + (stepsCount - Steps.Count)
                    });

                    Tools.WriteLogLine("... {0}. {1} steps remaining.", this.State, Steps.Count);

                    if (this.AbortRequested)
                    {
                        // abort
                        Tools.WriteLogLine("... {0} Aborted", this.DeviceIdentification.DeviceName);
                        Steps.Clear();
                        break;
                    }

                    if (this.State == Status.paused)
                    {
                        // paused
                        Thread.Sleep(pauseInterval);
                        if (this.WaitInterval > pauseInterval)
                        {
                            this.WaitInterval = this.WaitInterval.Subtract(pauseInterval);
                            if (this.SubStates.Count < 2)
                            {
                                this.SubStates.Add(new CommandDescription { currentState = Substate.asynchPaused });
                            }
                        }
                        else
                        {
                            this.State = Status.busy;
                            this.SubStates.Clear();
                        }

                        continue;
                    }

                    // executes the next step
                    Thread.Sleep(Steps[0]);
                    Steps.RemoveAt(0);
                    this.SubStates.Clear();
                }

                this.State = Status.idle;
                this.AbortRequested = false;
                return this.CreateReturnValue(ReturnCode.AsynchronousCommandHasFinished,"No Error",duration);
            }

            return base.ExecuteWork(evaluateOnly);
        }

        #endregion //Protected methods
    }
}