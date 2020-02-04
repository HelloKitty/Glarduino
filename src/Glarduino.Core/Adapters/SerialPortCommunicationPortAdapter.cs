using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace Glarduino
{
	public sealed class SerialPortCommunicationPortAdapter : ICommunicationPort
	{
		private SerialPort InternalSerialPort { get; }

		public bool IsOpen => InternalSerialPort.IsOpen;

		public int ReadTimeout
		{
			get => InternalSerialPort.ReadTimeout;
			set => InternalSerialPort.ReadTimeout = value;
		}

		public int WriteTimeout
		{
			get => InternalSerialPort.WriteTimeout;
			set => InternalSerialPort.WriteTimeout = value;
		}

		public Encoding Encoding => InternalSerialPort.Encoding;

		public Stream BaseStream => InternalSerialPort.BaseStream;

		public SerialPortCommunicationPortAdapter(SerialPort internalSerialPort)
		{
			InternalSerialPort = internalSerialPort ?? throw new ArgumentNullException(nameof(internalSerialPort));
		}

		public void Dispose()
		{
			InternalSerialPort.Dispose();
		}

		public void Open()
		{
			InternalSerialPort.Open();
		}

		public void Close()
		{
			InternalSerialPort.Close();
		}
	}
}
