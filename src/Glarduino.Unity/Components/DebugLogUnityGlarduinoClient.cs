using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class DebugLogUnityGlarduinoClient : BaseUnityGlarduinoAdapterClient
	{
		private async Task Start()
		{
			UnityStringGlarduinoClient client = new UnityStringGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new StringMessageDeserializerStrategy(), new DebugLogStringMessageDispatchingStrategy());

			await StartClient(client)
				.ConfigureAwait(false);
		}
	}
}
