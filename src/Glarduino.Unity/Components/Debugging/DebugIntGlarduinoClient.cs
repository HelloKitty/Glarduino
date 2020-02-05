using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class DebugIntGlarduinoClient : BaseUnityGlarduinoAdapterClient
	{
		private async Task Start()
		{
			UnityIntGlarduinoClient client = new UnityIntGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new IntMessageDeserializerStrategy(), new DebugLogIntMessageDispatchingStrategy());

			await StartClient(client)
				.ConfigureAwait(false);
		}
	}
}
