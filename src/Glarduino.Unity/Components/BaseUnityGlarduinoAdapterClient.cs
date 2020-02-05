using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Base Unity3D client adapter for editor component access to the Glarduino library.
	/// </summary>
	public abstract class BaseUnityGlarduinoAdapterClient : MonoBehaviour
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

		/// <summary>
		/// Called when the component is disable in Unity3D.
		/// </summary>
		void OnDisable()
		{
			//When disabled we should just dispose.
			CurrentClient?.Dispose();
		}

		/// <summary>
		/// See: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationQuit.html
		/// </summary>
		void OnApplicationQuit()
		{
			//When disabled we should just dispose.
			CurrentClient?.Dispose();
		}

		/// <summary>
		/// Implementer should call this method to start the client.
		/// </summary>
		/// <typeparam name="TMessageType">The client message handling type.</typeparam>
		/// <param name="client">The client instance.</param>
		/// <returns>Awaitable for client start.</returns>
		protected async Task StartClient<TMessageType>([NotNull] BaseGlarduinoClient<TMessageType> client)
		{
			CurrentClient = client ?? throw new ArgumentNullException(nameof(client));

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
