using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Base type for any Glarduino Arduino connected client.
	/// </summary>
	public abstract class BaseGlarduinoClient : IClientConnectable
	{
		/// <summary>
		/// The internally managed <see cref="SerialPort"/> that represents
		/// the potentially connected port to the Aurdino device.
		/// </summary>
		protected SerialPort InternallyManagedPort { get; } = null;

		/// <summary>
		/// The connection info used for the <see cref="InternallyManagedPort"/>.
		/// </summary>
		protected ArduinoPortConnectionInfo ConnectionInfo { get; }

		/// <summary>
		/// Indicates if the client is connected.
		/// </summary>
		public bool isConnected => InternallyManagedPort.IsOpen;

		protected BaseGlarduinoClient(ArduinoPortConnectionInfo connectionInfo)
		{
			ConnectionInfo = connectionInfo ?? throw new ArgumentNullException(nameof(connectionInfo));
			InternallyManagedPort = new SerialPort(connectionInfo.PortName, connectionInfo.BaudRate);
		}

		public Task<bool> ConnectAsync()
		{
			//Copy ardity
			InternallyManagedPort.ReadTimeout = ConnectionInfo.ReadTimeout;
			InternallyManagedPort.WriteTimeout = ConnectionInfo.WriteTimeout;

			InternallyManagedPort.Open();

			return Task.FromResult(InternallyManagedPort.IsOpen);
		}
	}
}
