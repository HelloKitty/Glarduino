using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class ArdityGlarduinoAdapterClient : BaseUnityGlarduinoAdapterClient
	{
		[SerializeField]
		private MonoBehaviour Listener;

		void Awake()
		{
			if(!Listener is IMessageListener<string>)
				throw new InvalidOperationException($"Provided Component: {Listener} On GameObject: {Listener.gameObject.name} does not implement {nameof(IMessageListener<string>)}");
		}

		private async Task Start()
		{
			var dispatcher = gameObject.AddComponent<MainThreadUpdateDispatchLatestToListenerMessageDispatcher<string>>();
			dispatcher.Listener = (IMessageListener<string>)Listener;

			UnityStringGlarduinoClient client = new UnityStringGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new StringMessageDeserializerStrategy(), dispatcher);

			await StartClient(client)
				.ConfigureAwait(false);
		}
	}
}
