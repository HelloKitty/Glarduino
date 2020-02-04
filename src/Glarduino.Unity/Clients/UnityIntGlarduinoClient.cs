using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	public sealed class UnityIntGlarduinoClient : BaseIntGlarduinoClient
	{
		private bool unityDisposed = false;

		public UnityIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<int> messageDeserializer, 
			IMessageDispatchingStrategy<int> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
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
