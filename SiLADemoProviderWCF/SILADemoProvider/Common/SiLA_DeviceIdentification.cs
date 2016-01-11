using System.Xml.Serialization;

namespace SiLA.Provider
{
    /// <summary>
    /// SiLA device identification
    /// </summary>
    public class SiLA_DeviceIdentification
    {
        /// <summary>
        /// Gets or sets the WSDL.
        /// </summary>
        /// <value>
        /// The WSDL URI of the service provider
        /// </value>
        public string Wsdl { get; set; }

        /// <summary>
        /// Gets or sets the SiLA interface version.
        /// </summary>
        /// <value>
        /// The version of the SiLA General Interface Specification.
        /// </value>
        public string SiLAInterfaceVersion { get; set; }

        /// <summary>
        /// Gets or sets the SiLA device class.
        /// </summary>
        /// <value>
        /// The SiLA device class
        /// </value>
        public int SiLADeviceClass { get; set; }

        /// <summary>
        /// Gets or sets the SiLA device class version.
        /// </summary>
        /// <value>
        /// The version of the Common Command Set.
        /// </value>
        public string SiLADeviceClassVersion { get; set; }

        /// <summary>
        /// Gets or sets the SiLA sub device class.
        /// </summary>
        /// <value>
        /// If the SiLADeviceClass is 1000 or more, this property will list the Device Classes out of which this device is composed. 
        /// If the SiLADeviceClass is lower than 1000, this property MUST be omitted.
        /// </value>
        public int[] SiLASubDeviceClass { get; set; }

        /// <summary>
        /// Gets or sets the device manufacturer.
        /// </summary>
        /// <value>
        /// Company name of the device manufacturer. It is recommended to use the URI of the manufacturer’s homepage for this entry.
        /// </value>
        public string DeviceManufacturer { get; set; }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>
        /// The model name of the device (vendor specific)
        /// </value>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the device serial number.
        /// </summary>
        /// <value>
        /// The serial number of the device (vendor specific)
        /// </value>
        public string DeviceSerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the device firmware version.
        /// </summary>
        /// <value>
        /// The version of the firmware (vendor specific)
        /// </value>
        public string DeviceFirmwareVersion { get; set; }       
    }
}