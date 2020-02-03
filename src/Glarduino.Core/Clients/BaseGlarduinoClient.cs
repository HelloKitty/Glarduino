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
	public abstract class BaseGlarduinoClient<TMessageType> : IClientConnectable, IClientListenable
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
		public virtual bool isConnected => InternallyManagedPort.IsOpen;

		/// <summary>
		/// Container for subscribable connection events for the client.
		/// </summary>
		public IConnectionEventsSubscribable ConnectionEvents => _ConnectionEvents;

		/// <summary>
		/// Strategy for deserializing messages from the serial port.
		/// </summary>
		private IMessageDeserializerStrategy<TMessageType> MessageDeserializer { get; }

		/// <summary>
		/// Strategy for dispatching messages.
		/// </summary>
		private IMessageDispatchingStrategy<TMessageType> MessageDispatcher { get; }

		protected BaseGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<TMessageType> messageDeserializer, 
			IMessageDispatchingStrategy<TMessageType> messageDispatcher)
		{
			ConnectionInfo = connectionInfo ?? throw new ArgumentNullException(nameof(connectionInfo));
			MessageDeserializer = messageDeserializer ?? throw new ArgumentNullException(nameof(messageDeserializer));
			MessageDispatcher = messageDispatcher ?? throw new ArgumentNullException(nameof(messageDispatcher));
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

		public async Task StartListeningAsync()
		{
			if(!isConnected)
				throw new InvalidOperationException($"Cannot start listening with {nameof(StartListeningAsync)} when internal {nameof(SerialPort)} {InternallyManagedPort} is not connected/open.");

			while (isConnected)
			{
				//TODO: Handle cancellation tokens better.
				TMessageType message = await MessageDeserializer.ReadMessageAsync(InternallyManagedPort);

				//A message was recieved and deserialized as the strategy implemented.
				//Therefore we should assume we have a valid message and then pass it to the message dispatching strategy.
				await MessageDispatcher.DispatchMessageAsync(message);
			}

			//TODO: Should we assume disconnection just because listening stopped?
			_ConnectionEvents.InvokeClientDisconnected();
		}
	}
}
