using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Base Unity3D client adapter for editor component access to the Glarduino library.
	/// </summary>
	public abstract class BaseUnityGlarduinoAdapterClient : MonoBehaviour, IDisposable
	{
		/// <summary>
		/// The string name of the communication port.
		/// </summary>
		[SerializeField]
		[Tooltip("Port name with which the SerialPort object will be created.")]
		private string portName = "COM3";

		/// <summary>
		/// The bitrate of data transfer for the communication.
		/// </summary>
		[SerializeField]
		[Tooltip("Baud rate that the serial device is using to transmit data.")]
		private int baudRate = 9600;

		/// <summary>
		/// See: <see cref="baudRate"/>.
		/// </summary>
		protected int BaudRate => baudRate;

		/// <summary>
		/// See: <see cref="baudRate"/>
		/// </summary>
		protected string PortName => portName;

		/// <summary>
		/// The current disposable client.
		/// </summary>
		protected IDisposable CurrentClient { get; private set; }

		private CancellationTokenSource RunCancelToken { get; } = new CancellationTokenSource();

		/// <summary>
		/// Called when the component is disable in Unity3D.
		/// </summary>
		void OnDisable()
		{
			//When disabled we should just dispose.
			Dispose();
		}

		/// <summary>
		/// See: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationQuit.html
		/// </summary>
		void OnApplicationQuit()
		{
			//When disabled we should just dispose.
			Dispose();
		}

		/// <summary>
		/// Implementer should call this method to start the client.
		/// </summary>
		/// <typeparam name="TMessageType">The client message handling type.</typeparam>
		/// <param name="client">The client instance.</param>
		/// <returns>Awaitable for client start.</returns>
		protected Task StartClient<TMessageType>([NotNull] BaseGlarduinoClient<TMessageType> client)
		{
			CurrentClient = client ?? throw new ArgumentNullException(nameof(client));

			client.ConnectionEvents.OnClientConnected += (sender, args) => Debug.Log($"Port: {PortName} connected.");
			client.ConnectionEvents.OnClientDisconnected += (sender, args) => Debug.Log($"Port: {PortName} disconnected.");
			client.OnExceptionEncountered += (sender, exception) => Debug.LogError($"Exception from Glardiuno Listener: {exception}");

			//If there is no await here, it's because it was removed during a period of time
			//when there was an unmanaged crash happening. And if it's not here, then it was the cause
			//so don't readd it. If the await is back then it was back
			Task.Factory.StartNew(async () =>
			{
				await client.ConnectAsync(RunCancelToken.Token)
					.ConfigureAwait(false);

				await client.StartListeningAsync(RunCancelToken.Token)
					.ConfigureAwait(false);

			}, RunCancelToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			RunCancelToken.Cancel();
			CurrentClient?.Dispose();
			RunCancelToken?.Dispose();
		}
	}
}
