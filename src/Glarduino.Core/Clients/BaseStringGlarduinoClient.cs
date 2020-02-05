using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// base <see cref="string"/> implementation of <see cref="BaseGlarduinoClient{TMessageType}"/>.
	/// </summary>
	public class BaseStringGlarduinoClient : BaseGlarduinoClient<string>
	{
		/// <summary>
		/// Crates a new <see cref="BaseStringGlarduinoClient"/>.
		/// </summary>
		/// <param name="connectionInfo">The connection information for the port.</param>
		/// <param name="messageDeserializer">The message deserialization strategy.</param>
		/// <param name="messageDispatcher">The message dispatching strategy.</param>
		public BaseStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<string> messageDeserializer, 
			IMessageDispatchingStrategy<string> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		/// <summary>
		/// Crates a new <see cref="BaseStringGlarduinoClient"/>.
		/// </summary>
		/// <param name="connectionInfo">The connection information for the port.</param>
		/// <param name="messageDeserializer">The message deserialization strategy.</param>
		/// <param name="messageDispatcher">The message dispatching strategy.</param>
		/// <param name="comPort">The specified communication port.</param>
		public BaseStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo,
			IMessageDeserializerStrategy<string> messageDeserializer,
			IMessageDispatchingStrategy<string> messageDispatcher,
			ICommunicationPort comPort)
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{

		}
	}
}
