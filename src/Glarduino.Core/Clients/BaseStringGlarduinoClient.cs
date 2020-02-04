using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public class BaseStringGlarduinoClient : BaseGlarduinoClient<string>
	{
		public BaseStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<string> messageDeserializer, 
			IMessageDispatchingStrategy<string> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{
		}

		public BaseStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo,
			IMessageDeserializerStrategy<string> messageDeserializer,
			IMessageDispatchingStrategy<string> messageDispatcher,
			ICommunicationPort comPort)
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{
		}
	}
}
