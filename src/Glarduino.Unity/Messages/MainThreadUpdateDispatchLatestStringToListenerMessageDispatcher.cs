using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class MainThreadUpdateDispatchLatestStringToListenerMessageDispatcher : MonoBehaviour, IMessageDispatchingStrategy<string>
	{
		private readonly object SyncObj = new object();

		private string LatestMessage { get; set; }

		public IStringMessageListener Listener { get; set; }

		private bool isNewMessageAvailable { get; set; }

		private int debugMessageRecieved = 0;

		public async Task DispatchMessageAsync(string message)
		{
			lock (SyncObj)
			{
				LatestMessage = message;
				isNewMessageAvailable = true;
				debugMessageRecieved++;
			}
		}

		void Update()
		{
			if (Listener == null)
				return;

			if (LatestMessage == null)
				return;

			if (!isNewMessageAvailable)
				return;

			string message;

			Debug.Log($"Skipped Message: {debugMessageRecieved - 1}");

			lock (SyncObj)
			{
				message = LatestMessage;
				isNewMessageAvailable = false;
				debugMessageRecieved = 0;
			}

			Listener.OnMessageArrived(message);
		}
	}
}
