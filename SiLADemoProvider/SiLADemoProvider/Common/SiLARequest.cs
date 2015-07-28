namespace SiLA.Provider
{
    /// <summary>
    /// SiLA request
    /// </summary>
    public class SiLARequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiLARequest"/> class.
        /// </summary>
        public SiLARequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLARequest"/> class.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="lockId">The lock id.</param>
        /// <param name="commandName">Name of the command.</param>
        public SiLARequest(int requestId, string lockId, string commandName)
        {
            this.RequestId = requestId;
            this.LockId = lockId;
            this.CommandName = commandName;
        }

        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        /// <value>
        /// The request id.
        /// </value>
        public int RequestId { get; set; }

        /// <summary>
        /// Gets or sets the lock id.
        /// </summary>
        /// <value>
        /// The lock id.
        /// </value>
        public string LockId { get; set; }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string CommandName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", 
                this.CommandName, this.RequestId, this.LockId);
        }

    }
}