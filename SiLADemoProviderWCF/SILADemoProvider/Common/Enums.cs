namespace SiLA.Provider
{
    /// <summary>
    /// The different substates
    /// </summary>
    public enum Substate
    {
        /// <summary>
        /// The asynchronous command is paused. If not all subtasks are finished it can be continued with the DoContinue command.
        /// </summary>
        asynchPaused,
        
        /// <summary>
        /// Pause was requested but the command (subtask) has not completed yet.
        /// </summary>
        pauseRequested,
        
        /// <summary>
        /// The asynchronous command is currently processing.
        /// </summary>
        processing,
        
        /// <summary>
        /// Instrument is waiting for the answer from the EventReceiver upon sending of the ResponseEvent.
        /// </summary>
        responseWaiting,

        /// <summary>
        /// Instrument is waiting for the answer from the EventReceiver upon sending of a DataEvent.
        /// </summary>
        dataWaiting
    }

    /// <summary>
    /// The types of device classes
    /// </summary>
    public enum DeviceClass
    {
        /// <summary>
        /// Basic suppliance
        /// </summary>
        BasicSuppliance = 0,
        /// <summary>
        /// Dispenser
        /// </summary>
        Dispenser,
        /// <summary>
        /// Pipettor
        /// </summary>
        Pipettor,
        /// <summary>
        /// Washer
        /// </summary>
        Washer,
        /// <summary>
        /// Reader imager
        /// </summary>
        ReaderImager,
        /// <summary>
        /// Shaker
        /// </summary>
        Shaker,
        /// <summary>
        /// Stacker
        /// </summary>
        Stacker,
        /// <summary>
        /// Hotel
        /// </summary>
        Hotel,
        /// <summary>
        /// Incubator
        /// </summary>
        Incubator,
        /// <summary>
        /// Lid handler
        /// </summary>
        LidHandler,
        /// <summary>
        /// Sealer
        /// </summary>
        Sealer,
        /// <summary>
        /// Peeler
        /// </summary>
        Peeler,
        /// <summary>
        /// Piercer
        /// </summary>
        Piercer,
        /// <summary>
        /// CapperDeCapper
        /// </summary>
        CapperDeCapper,
        /// <summary>
        /// Code reader
        /// </summary>
        CodeReader,
        /// <summary>
        /// Code labler
        /// </summary>
        CodeLabler,
        /// <summary>
        /// RFID reader writer transponder
        /// </summary>
        RFIDReaderWriterTransponder,
        /// <summary>
        /// Centrifuge
        /// </summary>
        Centrifuge,
        /// <summary>
        /// Conveyor belt
        /// </summary>
        ConveyorBelt,
        /// <summary>
        /// Robot
        /// </summary>
        Robot,
        /// <summary>
        /// Heating cooling unit thermostat
        /// </summary>
        HeatingCoolingUnitThermostat,
        /// <summary>
        /// Analytical instrument
        /// </summary>
        AnalyticalInstrument,
        /// <summary>
        /// Syringe pump
        /// </summary>
        SyringePump,
        /// <summary>
        /// Valve
        /// </summary>
        Valve,
        /// <summary>
        /// Evaporator
        /// </summary>
        Evaporator,
        /// <summary>
        /// Balance
        /// </summary>
        Balance,
        /// <summary>
        /// Pump
        /// </summary>
        Pump,
        /// <summary>
        /// Vacuum pump
        /// </summary>
        VacuumPump,
        /// <summary>
        /// Acoustic dispenser
        /// </summary>
        AcousticDispenser,
        /// <summary>
        /// Tube pincher puncher
        /// </summary>
        TubePincherPuncher,
        /// <summary>
        /// PCR cycler
        /// </summary>
        PCRCycler,
        /// <summary>
        /// Camera
        /// </summary>
        Camera,
        /// <summary>
        /// Digital IO
        /// </summary>
        DigitalIO = 901,
        /// <summary>
        /// Analog IO
        /// </summary>
        AnalogIO,
        /// <summary>
        /// Motor controller
        /// </summary>
        MotorController,
        /// <summary>
        /// Custom device
        /// </summary>
        CustomDevice = 1000,
    }


    /// <summary>
    /// Contains the common usable return codes, which are the value of the member "Code" in the SiLA Return Value structure.
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// 1 Success
        /// </summary>
        Success = 1,
        /// <summary>
        /// 2 Asynchronous command accepted
        /// </summary>
        AsynchronousCommandAccepted,
        /// <summary>
        /// 3 Asynchronous command has finished
        /// </summary>
        AsynchronousCommandHasFinished,
        /// <summary>
        /// 4 Device is busy due to other command execution
        /// </summary>
        DeviceIsBusyDueToOtherCommandExecution,
        /// <summary>
        /// 5 Error on lockId
        /// </summary>
        ErrorOnLockId,
        /// <summary>
        /// 6 Error on requestId
        /// </summary>
        ErrorOnRequestId,
        /// <summary>
        /// 7 Error on deviceId
        /// </summary>
        ErrorOnDeviceId,
        /// <summary>
        /// 8 Error on certificate check
        /// </summary>
        ErrorOnCertificateCheck,
        /// <summary>
        /// 9 Command not allowed in this state
        /// </summary>
        CommandNotAllowedInThisState,
        /// <summary>
        /// 10 Error in Data sent to Event Receiver
        /// </summary>
        ErrorInDataSentToEventReceiver,
        /// <summary>
        /// 11 Invalid command parameter
        /// </summary>
        InvalidCommandParameter,
        /// <summary>
        /// 12 Finished with warning
        /// </summary>
        FinishedWithWarning,
        /// <summary>
        /// 13 Command unexecuted de-queued due to error in previous command execution
        /// </summary>
        CommandUnexecutedDueToErrorInPreviousCommandExecution,
    }
}