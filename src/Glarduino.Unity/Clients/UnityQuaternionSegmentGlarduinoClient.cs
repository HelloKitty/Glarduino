using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	public sealed class UnityQuaternionSegmentGlarduinoClient : BaseGlarduinoClient<RecyclableArraySegment<Quaternion>>
	{
		private bool unityDisposed = false;

		public UnityQuaternionSegmentGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<RecyclableArraySegment<Quaternion>> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		public UnityQuaternionSegmentGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<RecyclableArraySegment<Quaternion>> messageDispatcher, 
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
