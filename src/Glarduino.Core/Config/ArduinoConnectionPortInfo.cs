using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Port connection information for the Arduino connection.
	/// </summary>
	public class ArduinoPortConnectionInfo
	{
		/// <summary>
		/// Port name with which the SerialPort object will be created.
		/// </summary>
		public string PortName { get; }

		/// <summary>
		/// Baud rate that the serial device is using to transmit data.
		/// This is bits per second.
		/// </summary>
		public int BaudRate { get; }

		public int WriteTimeout { get; }

		/// <summary>
		/// Amount of milliseconds alotted to a single read or connect. An
		/// exception is thrown when such operations take more than this time
		/// to complete.
		/// </summary>
		public int ReadTimeout { get; } = 100;

		/// <summary>
		/// Amount of milliseconds alloted to a single write. An exception is thrown
		/// when such operations take more than this time to complete.
		/// </summary>
		public int writeTimeout { get; } = 100;

		public ArduinoPortConnectionInfo(string portName, int baudRate)
		{
			if (string.IsNullOrEmpty(portName)) throw new ArgumentException("Value cannot be null or empty.", nameof(portName));
			if (baudRate <= 0) throw new ArgumentOutOfRangeException(nameof(baudRate));

			PortName = portName;
			BaudRate = baudRate;
		}

		public ArduinoPortConnectionInfo(string portName, int baudRate, int readTimeout, int writeTimeout)
			: this(portName, baudRate)
		{
			ReadTimeout = readTimeout;
			WriteTimeout = writeTimeout;
		}
	}
}
