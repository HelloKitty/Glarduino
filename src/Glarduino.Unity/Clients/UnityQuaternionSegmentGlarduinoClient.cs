using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	public sealed class UnityQuaternionGlarduinoClient : BaseGlarduinoClient<ArraySegment<Quaternion>>
	{
		private bool unityDisposed = false;

		public UnityQuaternionGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<ArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<ArraySegment<Quaternion>> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		public UnityQuaternionGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<ArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<ArraySegment<Quaternion>> messageDispatcher, 
			ICommunicationPort comPort) 
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{

		}

		//Override to only indicate being connected if the application is in playmode too.
		public override bool isConnected => base.isConnected && !unityDisposed;

		public override void Dispose()
		{
			unityDisposed = true;
			base.Dispose();
		}
	}
}
