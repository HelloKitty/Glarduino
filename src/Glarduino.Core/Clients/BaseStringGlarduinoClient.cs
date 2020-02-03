using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public abstract class BaseStringGlarduinoClient : BaseGlarduinoClient<string>
	{
		protected BaseStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<string> messageDeserializer, 
			IMessageDispatchingStrategy<string> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}
	}
}
