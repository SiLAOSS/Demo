using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SiLA.Provider
{
   
    [ServiceContract(Namespace = SiLADefinitions.ContractNamespace)]
    public interface ISiLAWebService
    {
        #region Mandatory

        ///<SiLACommandDescription xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" isCommonCommand="true" estimatedDuration="PT0S" xsi:noNamespaceSchemaLocation="http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd">
        ///   <summary>This command aborts all running and pending asynchronous commands of the device.</summary>
        ///   <param name="requestId" minValue="1" maxValue="2147483647">This parameter is the unique identification of this command call.</param>
        ///   <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        ///   <response xsi:type="standardResponse" parameterSetCount="0">
        ///      <description>Empty response</description>
        ///   </response>
        ///</SiLACommandDescription>
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "Abort")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.AbortDescription)]
        SiLAReturnValue Abort(int requestId, string lockId);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "DoContinue")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.DoContinueDescription)]
        SiLAReturnValue DoContinue(int requestId, string lockId);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "GetDeviceIdentification")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.GetDeviceIdentificationDescription)]
        SiLAReturnValue GetDeviceIdentification(int requestId, string lockId, out SiLA_DeviceIdentification deviceDescription);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "GetStatus")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.GetStatusDescription)]
        SiLAReturnValue GetStatus(int requestId, out string deviceID, out Status state, out CommandDescription[] substates, out Boolean locked, out string PMSId, out DateTime currentTime);


        ///<SiLACommandDescription xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" isCommonCommand="true" estimatedDuration="PT0S" xsi:noNamespaceSchemaLocation="http://sila-standard.org /schemata/SoapAnnotation_1.2.xsd">
        ///   <summary>This command initializes the device.</summary>
        ///   <param name="requestId" minValue="1" maxValue="2147483647">This parameter ist the unique identification of this command call.</param>
        ///   <param name="lockId">This parameter is the identification of the PMS which has locked the device.</param>
        ///   <response xsi:type="standardResponse" parameterSetCount="0">
        ///      <description>Empty response</description>
        ///   </response>
        ///</SiLACommandDescription>
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "Initialize")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.InitializeDescription)]
        SiLAReturnValue Initialize(int requestId, string lockId);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "LockDevice")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.LockDeviceDescription)]
        SiLAReturnValue LockDevice(int requestId, string lockId, string lockTimeout, string eventReceiverURI, string PMSId);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "Pause")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.PauseDescription)]
        SiLAReturnValue Pause(int requestId, string lockId);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "Reset")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.ResetDescription)]
        SiLAReturnValue Reset(int requestId, string lockId, string deviceId, string eventReceiverURI, string PMSId, string errorHandlingTimeout, Boolean simulationMode);


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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "UnlockDevice")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.UnlockDeviceDescription)]
        SiLAReturnValue UnlockDevice(int requestId, string lockId);

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
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "Delay")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.DelayDescription)]
        SiLAReturnValue Delay(int requestId, string lockId, string time);


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
        //[WebMethod(Description = SiLAWebMethodDescriptions.GetParametersDescription)]
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "GetParameters")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.GetParametersDescription)]
        SiLAReturnValue GetParameters(int requestId, string lockId);


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
        //[WebMethod(Description = SiLAWebMethodDescriptions.SetParametersDescription)]
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "SetParameters")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.SetParametersDescription)]
        SiLAReturnValue SetParameters(int requestId, string lockId, string paramsXML);


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
        //[WebMethod(Description = SiLAWebMethodDescriptions.StoreAtPositionDescription)]
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "StoreAtPosition")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.StoreAtPositionDescription)]
        SiLAReturnValue StoreAtPosition(int requestId, string lockId, int position, string labwareType);


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
        //[WebMethod(Description = SiLAWebMethodDescriptions.RetrieveByPositionIdDescription)]
        [OperationContract(Action = SiLADefinitions.SiLAOperationNamespace + "RetrieveByPositionId")]
        [WsdlDocumentation(SiLAWebMethodDescriptions.RetrieveByPositionIdDescription)]
        SiLAReturnValue RetrieveByPositionId(int requestId, string lockId, int position, string labwareType);
       

        #endregion //Required
    }

}
