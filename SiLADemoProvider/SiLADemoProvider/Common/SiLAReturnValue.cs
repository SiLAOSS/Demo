namespace SiLA.Provider
{
    /// <summary>
    /// SiLA return value. All responses to a command MUST have the form and content of a SiLA Return Value.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute(Name = "SiLAReturnValue", Namespace = "http://sila-standard.org")]
    [System.SerializableAttribute()]
    public class SiLAReturnValue
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLAReturnValue"/> class.
        /// </summary>
        public SiLAReturnValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLAReturnValue"/> class.
        /// </summary>
        /// <param name="retCode">The return code.</param>
        /// <param name="retText">The ret text.</param>
        /// <param name="retDuration">Duration of the ret.</param>
        /// <param name="retDeviceClass">The ret device class.</param>
        public SiLAReturnValue(int retCode, string retText, string retDuration, int retDeviceClass)
        {
            this.returnCode = retCode;
            this.message = retText;
            this.duration = retDuration;
            this.deviceClass = retDeviceClass;
        }

        #endregion //Constructors	    

        #region Properties

        /// <summary>
        /// Gets or sets the device class.
        /// </summary>
        /// <value>
        /// The ID of the Device Class.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int deviceClass { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The return message. In case an error occurs the message describes the error by a human readable text. 
        /// While device class groups will be defining some common return codes, the detailed description of the error will be different from device to device. 
        /// Therefore the message text should be helpful to the user of the device and contain enough information to enable a profound analysis of the error.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string message { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// If a synchronous command was called, the “Duration” parameters reports the consumed time for command processing.
        /// For an asynchronous command the “Duration” parameter returns the estimated duration for processing the asynchronous command in the synchronous (immediate) answer. 
        /// The PMS MUST use this value to set a timeout, which has not to be exceeded. For unknown duration, a 0 value shall be returned. 
        /// In this case the PMS cannot compute a timeout value.
        /// In the response event of an asynchronous command the “Duration” parameter represents the consumed time for processing the command.
        /// For use in the simulation mode, in all three cases the estimated time for processing the command shall be returned.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string duration { get; set; }

        /// <summary>
        /// Gets or sets the return code.
        /// </summary>
        /// <value>
        /// The return code itself.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int returnCode { get; set; }
        
        #endregion //Properties	
    
        #region ResponseData

        ResponseData _responseData;

        /// <summary>
        /// Sets the response data
        /// </summary>
        /// <param name="data">The data.</param>
        /// <seealso cref="SiLADevice.ExecuteAsync"/>
        public void SetResponseData(ResponseData data)
        {
            _responseData = data;
        }

        /// <summary>
        /// Gets the response data
        /// </summary>
        /// <returns>ResponseData</returns>
        /// <seealso cref="SiLADevice.ExecuteAsync"/>
        public ResponseData GetResponseData()
        {
            if (_responseData == null)
            {
                _responseData = new ResponseData();
            }

            return _responseData;
        }

        #endregion //ResponseData
    }
}