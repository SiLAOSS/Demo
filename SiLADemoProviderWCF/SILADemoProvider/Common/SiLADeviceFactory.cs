using System;
using System.Collections.Generic;
using System.Linq;

namespace SiLA.Provider
{
    /// <summary>
    /// Factory for SiLA devices
    /// </summary>
    public static class SiLADeviceFactory
    {
        static List<SiLADevice> SiLADevices;
        private static object _lock = new object();

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deviceName">Name of the device.</param>
        /// <returns></returns>
        public static T GetDevice<T>(string deviceName) where T : SiLADevice
        {
            lock (_lock)
            {
                if (SiLADevices == null)
                {
                    SiLADevices = new List<SiLADevice>();
                    return null;
                }

                T device = SiLADevices.First(d => d.DeviceIdentification.DeviceName == deviceName) as T;
                return device;
            }
        }

        /// <summary>
        /// Connects to the device.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deviceName">Name of the device.</param>
        /// <returns></returns>
        public static T ConnectTo<T>(string deviceName) where T : SiLADevice
        {
            lock (_lock)
            {
                T device = GetDevice<T>(deviceName);
                if (device == null)
                {
                    device = Activator.CreateInstance<T>();
                    SiLADevices.Add(device);
                    device.DeviceIdentification.DeviceName = deviceName;
                }

                return device;
            }
        }
    }
}