using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class QuaternionGlarduinoClient : BaseUnityGlarduinoAdapterClient
	{
		[SerializeField]
		private MonoBehaviour Listener;

		void Awake()
		{
			if(!Listener is IMessageListener<RecyclableArraySegment<Quaternion>>)
				throw new InvalidOperationException($"Provided Component: {Listener} On GameObject: {Listener.gameObject.name} does not implement {nameof(IMessageListener<RecyclableArraySegment<Quaternion>>)}");
		}

		public async Task Start()
		{
			var dispatcher = gameObject.AddComponent<MainThreadUpdateDispatchLatestToListenerMessageDispatcher<RecyclableArraySegment<Quaternion>>>();
			dispatcher.Listener = (IMessageListener<RecyclableArraySegment<Quaternion>>)Listener;

			UnityQuaternionSegmentGlarduinoClient client = new UnityQuaternionSegmentGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new QuaternionSegmentMessageDeserializerStrategy(), dispatcher);

			await StartClient(client)
				.ConfigureAwait(false);
		}
	}
}
