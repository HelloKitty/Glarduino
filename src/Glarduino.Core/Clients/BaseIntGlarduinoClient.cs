using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// base <see cref="int"/> implementation of <see cref="BaseGlarduinoClient{TMessageType}"/>.
	/// </summary>
	public class BaseIntGlarduinoClient : BaseGlarduinoClient<int>
	{
		/// <summary>
		/// Crates a new <see cref="BaseIntGlarduinoClient"/>.
		/// </summary>
		/// <param name="connectionInfo">The connection information for the port.</param>
		/// <param name="messageDeserializer">The message deserialization strategy.</param>
		/// <param name="messageDispatcher">The message dispatching strategy.</param>
		public BaseIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<int> messageDeserializer, 
			IMessageDispatchingStrategy<int> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		/// <summary>
		/// Crates a new <see cref="BaseIntGlarduinoClient"/>.
		/// Specifically defining the communication port.
		/// </summary>
		/// <param name="connectionInfo">The connection information for the port.</param>
		/// <param name="messageDeserializer">The message deserialization strategy.</param>
		/// <param name="messageDispatcher">The message dispatching strategy.</param>
		/// <param name="comPort">The specified communication port.</param>
		public BaseIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo,
			IMessageDeserializerStrategy<int> messageDeserializer,
			IMessageDispatchingStrategy<int> messageDispatcher,
			ICommunicationPort comPort)
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{

		}
	}
}
