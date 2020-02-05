using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// <see cref="Quaternion"/> <see cref="RecyclableArraySegment{T}"/>-based implementation of <see cref="BaseUnityGlarduinoAdapterClient"/>.
	/// </summary>
	public sealed class QuaternionGlarduinoClient : BaseUnityGlarduinoAdapterClient
	{
		/// <summary>
		/// The message listener.
		/// Must implement <see cref="RecyclableArraySegment{T}"/> for <see cref="Quaternion"/> <see cref="IMessageListener{TMessageType}"/>.
		/// </summary>
		[SerializeField]
#pragma warning disable 649
		private MonoBehaviour Listener;
#pragma warning restore 649

		/// <summary>
		/// Hack/Debug hardcoded <see cref="Quaternion"/> amount that's coming in through the serial ports.
		/// </summary>
		[SerializeField]
		private int ExpectedQuaternionCount = 0;

		/// <summary>
		/// Called by Unity3D at component awake.
		/// </summary>
		void Awake()
		{
			if(!(Listener is IMessageListener<RecyclableArraySegment<Quaternion>>))
				throw new InvalidOperationException($"Provided Component: {Listener} On GameObject: {Listener.gameObject.name} does not implement {nameof(IMessageListener<RecyclableArraySegment<Quaternion>>)}");
		}

		/// <summary>
		/// Called by Unity3D at component. start.
		/// </summary>
		/// <returns></returns>
		public async Task Start()
		{
			var dispatcher = gameObject.AddComponent<MainThreadUpdateDispatchLatestToListenerMessageDispatcher<RecyclableArraySegment<Quaternion>>>();
			dispatcher.Listener = (IMessageListener<RecyclableArraySegment<Quaternion>>)Listener;

			UnityQuaternionSegmentGlarduinoClient client = new UnityQuaternionSegmentGlarduinoClient(new ArduinoPortConnectionInfo(PortName, BaudRate), new HarcodedSizeQuaternionSegmentMessageDeserializerStrategy(ExpectedQuaternionCount), dispatcher);

			await StartClient(client)
				.ConfigureAwait(false);
		}
	}
}
