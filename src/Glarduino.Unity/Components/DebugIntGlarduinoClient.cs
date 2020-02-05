using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class DebugIntGlarduinoClient : BaseUnityGlarduinoAdapterClient
	{
		private IDisposable _currentClient { get; set; }

		protected override IDisposable CurrentClient => _currentClient;

		private async Task Start()
		{
			UnityIntGlarduinoClient client = new UnityIntGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new IntMessageDeserializerStrategy(), new DebugLogIntMessageDispatchingStrategy());

			_currentClient = client;
			client.ConnectionEvents.OnClientConnected += (sender, args) => Debug.Log($"Port: {PortName} connected.");
			client.ConnectionEvents.OnClientDisconnected += (sender, args) => Debug.Log($"Port: {PortName} disconnected.");
			client.OnExceptionEncountered += (sender, exception) => Debug.LogError($"Exception from Glardiuno Listener: {exception}");

			await Task.Factory.StartNew(async () =>
			{
				await client.ConnectAsync()
					.ConfigureAwait(false);

				await client.StartListeningAsync()
					.ConfigureAwait(false);

			}, TaskCreationOptions.LongRunning);
		}
	}
}
