using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class ArdityGlarduinoAdapterClient : BaseUnityGlarduinoAdapterClient
	{
		private IDisposable _currentClient { get; set; }

		protected override IDisposable CurrentClient => _currentClient;

		[SerializeField]
		private MonoBehaviour Listener;

		void Awake()
		{
			if(!Listener is IStringMessageListener)
				throw new InvalidOperationException($"Provided Component: {Listener} On GameObject: {Listener.gameObject.name} does not implement {nameof(IStringMessageListener)}");
		}

		private async Task Start()
		{
			var dispatcher = gameObject.AddComponent<MainThreadUpdateDispatchLatestStringToListenerMessageDispatcher>();
			dispatcher.Listener = (IStringMessageListener)Listener;

			UnityStringGlarduinoClient client = new UnityStringGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new StringMessageDeserializerStrategy(), dispatcher);

			_currentClient = client;
			client.ConnectionEvents.OnClientConnected += (sender, args) => Debug.Log($"Port: {PortName} connected.");
			client.ConnectionEvents.OnClientDisconnected += (sender, args) => Debug.Log($"Port: {PortName} disconnected.");

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
