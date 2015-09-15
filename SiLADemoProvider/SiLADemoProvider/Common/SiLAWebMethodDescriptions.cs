namespace SiLA.Provider
{
    /// <summary>
    /// Contains strings to set Description property of WebMethodAttribute
    /// </summary>
    public static class SiLAWebMethodDescriptions
    {
        /// <summary>
        /// Abort description
        /// </summary>
        public const string AbortDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command aborts all running and pending asynchronous commands of the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// DoContinue description
        /// </summary>
        public const string DoContinueDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command enables the continuation of the process for a paused system." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS, which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// GetDeviceIdentification description
        /// </summary>
        public const string GetDeviceIdentificationDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary> This command reports on details of the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS which has locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"deviceDescription\">" +
                                "This is the Device Identification. It is a SOAP complex type." +
                                "</Parameter> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// GetStatus description
        /// </summary>
        public const string GetStatusDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command reports the status of the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"deviceId\">" +
                                "This is the identification the device returns to identify itself at the PMS." +
                                "</Parameter> " +
                                "<Parameter name=\"state\">" +
                                "This is the status of the device" +
                                "</Parameter> " +
                                "<Parameter name=\"subStates\">" +
                                "This is the substate of the above state." +
                                "</Parameter> " +
                                "<Parameter name=\"locked\">" +
                                "This is the lock state of the device (locked=true, unlocked = false)" +
                                "</Parameter> " +
                                "<Parameter name=\"PMSId\">" +
                                "This is the identification of the PMS that locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"currentTime\">" +
                                "This is the time of reporting status information." +
                                "</Parameter> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// Initialize description
        /// </summary>
        public const string InitializeDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command initializes the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// LockDevice description
        /// </summary>
        public const string LockDeviceDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command locks the device for exclusive use." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\" >" +
                                "This parameter hands over the lock identification of the PMS to the device. The device will only accept further commands, if they use the same lockId." +
                                "</Parameter> " +
                                "<Parameter name=\"lockTimeout\" xsi:type=\"Duration\">" +
                                "If this parameter is omitted, no timeout will be set. Otherwise" +
                                "the device will unlock itself if it does not receive any commands within the timeout period." +
                                "</Parameter> " +
                                "<Parameter name=\"eventReceiverURI\" >" +
                                "This is the service URI of the Service Consumer’s event Receiver (used in case of timeout that will issue a reset with it)." +
                                "</Parameter> " +
                                "<Parameter name=\"PMSId\" >" +
                                "This is the Id of the PMS in order to identify the PMS that locked a device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// Pause description
        /// </summary>
        public const string PauseDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command pauses the process/workflow in order to enable user intervention." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS, which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description>" +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// Reset description
        /// </summary>
        public const string ResetDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to reset the Service Provider at any time from any state." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\" >" +
                                "This parameter is the identification of the PMS, which has locked" +
                                "the device." +
                                "</Parameter> " +
                                "<Parameter name=\"deviceId\" >" +
                                "This is the identification the device returns to identify itself at the PMS." +
                                "</Parameter> " +
                                "<Parameter name=\"eventReceiverURI\" >" +
                                "This is the connection information of the Service Consumers event Receiver." +
                                "</Parameter> " +
                                "<Parameter name=\"PMSId\" >" +
                                "This is the Id of the PMS in order to identify the PMS that locked a device." +
                                "</Parameter> " +
                                "<Parameter name=\"errorHandlingTimeout\" xsi:type=\"Duration\">" +
                                "This is the timeout until an errorhandling state is changed into an error state." +
                                "</Parameter> " +
                                "<Parameter name=\"simulationMode\" >" +
                                "This selects simulation mode." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\">" +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// UnlockDevice description
        /// </summary>
        public const string UnlockDeviceDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to unlock the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\">" +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// Delay description
        /// </summary>
        public const string DelayDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>The Delay command is used to suspend command execution.</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">This parameter is the identification of the SiLA Service Consumer, which has locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"duration\">This parameter defines suspend duration." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";


        /// <summary>
        /// GetParameters description
        /// </summary>
        public const string GetParametersDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to retrieve the available parameter values of the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS which has locked the device." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"1\"> " +
                                "<Description>" +
                                "Returns the parameter set of the device as parameterName, value pair." +
                                "</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// SetParameters description
        /// </summary>
        public const string SetParametersDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to send new parameter values to the device." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS, which has locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"paramsXML\">" +
                                "The xml Document of paramsXML is formatted as stated in the ParameterSet tag." +
                                "<ParameterSet>" +
                                "</ParameterSet>" +
                                "</Parameter>" +
                                "</SiLACommandDescription>";

        /// <summary>
        /// StoreAtPosition description
        /// </summary>
        public const string StoreAtPositionDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to store a labware item at a predefined position." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">" +
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" +
                                "This parameter is the identification of the PMS, which has locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"position\">" +
                                "The parameter is used to specify at which position the labware shall be stored." +
                                "</Parameter> " +
                                "<Parameter name=\"labwareType\">" +
                                "The parameter may be used to transfer information about the labware that the device might need to access it correctly." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";

        /// <summary>
        /// RetrieveByPositionId description
        /// </summary>
        public const string RetrieveByPositionIdDescription = "<SiLACommandDescription isCommonCommand=\"true\" estimatedDuration=\"PT0S\" " +
                                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                "xsi:noNamespaceSchemaLocation=\"http://sila-standard.org/schemata/SoapAnnotation_1.2.xsd\"> " +
                                "<Summary>This command is used to retrieve a labware item by specifying the position identification." +
                                "</Summary> " +
                                "<Parameter name=\"requestId\" minValue=\"1\" maxValue=\"2147483647\">"+
                                "This parameter is the unique identification of this command call." +
                                "</Parameter> " +
                                "<Parameter name=\"lockId\">" + 
                                "This parameter is the identification of the PMS, which has locked the device." +
                                "</Parameter> " +
                                "<Parameter name=\"position\">" +
                                "The parameter is used to specify which labware item shall be retrieved." +
                                "</Parameter> " +
                                "<Parameter name=\"labwareType\">" +
                                "The parameter may be used to transfer information about the labware that the device might need to access it correctly." +
                                "</Parameter> " +
                                "<Response xsi:type=\"standardResponse\" parameterSetCount=\"0\"> " +
                                "<Description>Empty response</Description> " +
                                "</Response> " +
                                "</SiLACommandDescription>";
    }
}
