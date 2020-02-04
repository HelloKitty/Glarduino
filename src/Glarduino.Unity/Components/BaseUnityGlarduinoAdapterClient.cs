using System;
using System.Collections.Generic;
using System.Text;
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

		protected abstract IDisposable CurrentClient { get; }

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
	}
}
