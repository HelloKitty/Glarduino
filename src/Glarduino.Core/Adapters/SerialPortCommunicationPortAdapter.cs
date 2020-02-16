using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Glarduino
{
	/// <summary>
	/// The <see cref="SerialPort"/> based implementation of the <see cref="ICommunicationPort"/>
	/// interface.
	/// </summary>
	public sealed class SerialPortCommunicationPortAdapter : ICommunicationPort
	{
		/// <summary>
		/// The <see cref="SerialPort"/> used to implement the <see cref="ICommunicationPort"/> interface.
		/// </summary>
		private SerialPort InternalSerialPort { get; }

		/// <inheritdoc />
		public bool IsOpen => InternalSerialPort.IsOpen;

		/// <inheritdoc />
		public int ReadTimeout
		{
			get => InternalSerialPort.ReadTimeout;
			set => InternalSerialPort.ReadTimeout = value;
		}

		/// <inheritdoc />
		public int WriteTimeout
		{
			get => InternalSerialPort.WriteTimeout;
			set => InternalSerialPort.WriteTimeout = value;
		}

		/// <inheritdoc />
		public Encoding Encoding => InternalSerialPort.Encoding;

		/// <inheritdoc />
		public Stream BaseStream => InternalSerialPort.BaseStream;

		/// <summary>
		/// Creates a new <see cref="SerialPortCommunicationPortAdapter"/> with the provided <see cref="SerialPort"/> <paramref name="internalSerialPort"/>
		/// as the port to be used.
		/// </summary>
		/// <param name="internalSerialPort">Specified port.</param>
		public SerialPortCommunicationPortAdapter(SerialPort internalSerialPort)
		{
			InternalSerialPort = internalSerialPort ?? throw new ArgumentNullException(nameof(internalSerialPort));
		}

		/// <summary>
		/// Disposes the adapter which disposes of <see cref="InternalSerialPort"/>.
		/// </summary>
		public void Dispose()
		{
			InternalSerialPort.Dispose();
		}

		/// <param name="cancelToken"></param>
		/// <inheritdoc />
		public void Open(CancellationToken cancelToken = default(CancellationToken))
		{
			InternalSerialPort.Open();
		}

		/// <inheritdoc />
		public void Close()
		{
			if (InternalSerialPort.IsOpen)
			{
				InternalSerialPort.DiscardInBuffer();
				InternalSerialPort.DiscardOutBuffer();
				InternalSerialPort.Close();
			}
		}
	}
}
