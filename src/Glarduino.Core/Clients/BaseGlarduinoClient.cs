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
		private ConnectionEvents _ConnectionEvents { get; }

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

		/// <summary>
		/// Container for subscribable connection events for the client.
		/// </summary>
		public IConnectionEventsSubscribable ConnectionEvents => _ConnectionEvents;

		protected BaseGlarduinoClient(ArduinoPortConnectionInfo connectionInfo)
		{
			ConnectionInfo = connectionInfo ?? throw new ArgumentNullException(nameof(connectionInfo));
			InternallyManagedPort = new SerialPort(connectionInfo.PortName, connectionInfo.BaudRate);

			_ConnectionEvents = new ConnectionEvents();
		}

		public Task<bool> ConnectAsync()
		{
			//Copy ardity
			InternallyManagedPort.ReadTimeout = ConnectionInfo.ReadTimeout;
			InternallyManagedPort.WriteTimeout = ConnectionInfo.WriteTimeout;

			try
			{
				InternallyManagedPort.Open();
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Failed to connect Glarduino to Port: {ConnectionInfo.PortName}. Reason: {e.Message}", e);
			}

			//Alert subscribers that the client has connected.
			if (InternallyManagedPort.IsOpen)
				_ConnectionEvents.InvokeClientConnected();

			return Task.FromResult(InternallyManagedPort.IsOpen);
		}
	}
}
