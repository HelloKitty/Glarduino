using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Glarduino
{
	public abstract class BaseUnityGlarduinoAdapterClient : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Port name with which the SerialPort object will be created.")]
		private string portName = "COM3";

		[SerializeField]
		[Tooltip("Baud rate that the serial device is using to transmit data.")]
		private int baudRate = 9600;

		protected int BaudRate => baudRate;

		protected string PortName => portName;

		protected IDisposable CurrentClient { get; private set; }

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
