using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public class BaseIntGlarduinoClient : BaseGlarduinoClient<int>
	{
		public BaseIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<int> messageDeserializer, 
			IMessageDispatchingStrategy<int> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{
		}

		public BaseIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo,
			IMessageDeserializerStrategy<int> messageDeserializer,
			IMessageDispatchingStrategy<int> messageDispatcher,
			ICommunicationPort comPort)
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{
		}
	}
}
