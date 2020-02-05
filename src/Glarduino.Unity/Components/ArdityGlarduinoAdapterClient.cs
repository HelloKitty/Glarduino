using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Ardity compatibility adapter for string-based message reading and dispatching.
	/// </summary>
	public sealed class ArdityGlarduinoAdapterClient : BaseUnityGlarduinoAdapterClient
	{
		/// <summary>
		/// The message listener.
		/// </summary>
		[SerializeField]
		private MonoBehaviour Listener;

		void Awake()
		{
			if(!Listener is IMessageListener<string>)
				throw new InvalidOperationException($"Provided Component: {Listener} On GameObject: {Listener.gameObject.name} does not implement {nameof(IMessageListener<string>)}");
		}

		/// <summary>
		/// Unity3D awaitable <see cref="Start"/> method that starts the client.
		/// </summary>
		/// <returns></returns>
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
