using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SiLA.Provider
{
    /// <summary>
    /// Contains some auxiliary methods
    /// </summary>
    public static class Tools
    {
        #region Serializiation

        /// <summary>
        /// Serializes the data into XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string CreateXML<T>(T data)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, data);
            writer.Close();

            string s = sb.ToString();
            return s;
        }

        /// <summary>
        /// Deserializes the XML into data.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>An object of the specified type</returns>
        public static T FromXML<T>(string xml)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            T data = (T)ser.Deserialize(reader);
            reader.Close();

            return data;
        }
        
        #endregion //Serializiation	
    
        #region Duration conversion from/to string

        /// <summary>
        /// Converts duration string to TimeSpan.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan ToTimeSpan(string duration)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(0);

            try
            {
                timeSpan = XmlConvert.ToTimeSpan(duration);
            }
            catch
            {
            }

            return timeSpan;
        }

        /// <summary>
        /// Converts TimeSpan to duration string.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>A formatted string according to ISO 8601</returns>
        public static string ToDuration(TimeSpan timeSpan)
        {
            string duration = XmlConvert.ToString(timeSpan);
            return duration;
        }

        /// <summary>
        /// Converts DateTime to duration string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>A formatted string according to ISO 8601</returns>
        public static string ToDuration(DateTime dateTime)
        {
            string duration = XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.Local);
            return duration;
        }

        /// <summary>
        /// Returns duration string of a zero TimeSpan.
        /// </summary>
        /// <value>
        /// The duration of the zero TimeSpan.
        /// </value>
        public static string ZeroDuration
        {
            get
            {
                return ToDuration(new TimeSpan());
            }
        }

        #endregion //Duration conversion from/to string	    

        #region Log file

        /// <summary>
        /// Writes the log line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>The formatted text</returns>
        public static string WriteLogLine(string format, params object[] args)
        {
            string line = string.Format(format, args);
            return WriteLogLine(line);
        }

        /// <summary>
        /// Writes the log line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>The formatted text</returns>
        public static string WriteLogLine(string line)
        {
            try
            {
                line = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} {1}", DateTime.Now, line);

                System.Diagnostics.Debug.WriteLine(line);


                string filePath = Properties.Settings.Default.LogFileName;
                string directory = Path.GetDirectoryName(filePath).ToUpper().Replace("%TEMP%", Path.GetTempPath());
                filePath = Path.Combine(directory, Path.GetFileName(filePath));

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(line);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Log Error: " + ex);
            }

            return line;
        }

        #endregion //Log file	    
    }
}