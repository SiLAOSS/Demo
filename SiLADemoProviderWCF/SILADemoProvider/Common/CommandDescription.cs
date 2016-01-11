namespace SiLA.Provider
{
    /// <summary>
    /// Command description
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute(Name = "CommandDescription", Namespace = "http://sila-standard.org")]
    [System.SerializableAttribute()]
    public class CommandDescription
    {
        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        /// <value>
        /// The request ID of the referring command.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int requestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the referring command.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string commandName { get; set; }

        /// <summary>
        /// Gets or sets the queue position.
        /// </summary>
        /// <value>
        /// The queue position.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int queuePosition { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time. If the Command is waiting in the queue to start, null will be returned.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string startedAt { get; set; }

        /// <summary>
        /// Gets or sets the actual substate.
        /// </summary>
        /// <value>
        /// The actual substate. If the Command is waiting in the queue to start, null will be returned.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Substate currentState { get; set; }

        /// <summary>
        /// Gets or sets the data waiting sub states.
        /// </summary>
        /// <value>
        /// A count of data waiting sub states. If the Command is waiting in the queue to start, null will be returned.
        /// </value>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int dataWaiting { get; set; }
    }
}