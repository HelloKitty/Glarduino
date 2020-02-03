using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	public sealed class UnityStringGlarduinoClient : BaseStringGlarduinoClient
	{
		public UnityStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<string> messageDeserializer, 
			IMessageDispatchingStrategy<string> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		//Override to only indicate being connected if the application is in playmode too.
		public override bool isConnected => base.isConnected && Application.isPlaying;
	}
}
