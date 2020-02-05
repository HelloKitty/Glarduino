using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class MainThreadUpdateDispatchLatestToListenerMessageDispatcher<TMessageType> : MonoBehaviour, IMessageDispatchingStrategy<TMessageType>
	{
		private readonly object SyncObj = new object();

		private TMessageType LatestMessage { get; set; }

		public IMessageListener<TMessageType> Listener { get; set; }

		private bool isNewMessageAvailable { get; set; }

		void Update()
		{
			if (Listener == null)
				return;

			if (LatestMessage == null)
				return;

			if (!isNewMessageAvailable)
				return;

			TMessageType message;

			lock (SyncObj)
			{
				message = LatestMessage;
				isNewMessageAvailable = false;
			}

			Listener.OnMessage(message);
		}

		public Task DispatchMessageAsync(TMessageType message)
		{
			lock(SyncObj)
			{
				LatestMessage = message;
				isNewMessageAvailable = true;
			}

			return Task.CompletedTask;
		}
	}
}
