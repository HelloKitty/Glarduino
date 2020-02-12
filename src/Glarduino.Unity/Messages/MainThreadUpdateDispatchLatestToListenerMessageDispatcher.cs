using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Unity3D main-threaded/<see cref="Update"/>-based generic <see cref="IMessageDispatchingStrategy{TMessageType}"/> implementation.
	/// </summary>
	/// <typeparam name="TMessageType"></typeparam>
	public class MainThreadUpdateDispatchLatestToListenerMessageDispatcher<TMessageType> : MonoBehaviour, IMessageDispatchingStrategy<TMessageType>
	{
		/// <summary>
		/// Syncronization object.
		/// </summary>
		private readonly object SyncObj = new object();

		/// <summary>
		/// The last message handled.
		/// </summary>
		private TMessageType LatestMessage { get; set; }

		/// <summary>
		/// The listener for the incoming messages.
		/// </summary>
		public IMessageListener<TMessageType> Listener { get; set; }

		/// <summary>
		/// Mutable <see cref="bool"/> indicating if a new message has been sent between the last frame.
		/// </summary>
		private bool isNewMessageAvailable { get; set; }

		/// <summary>
		/// Indicates if the message type is disposable.
		/// Implements IDisposable.
		/// </summary>
		public static bool isDisposableType { get; } = typeof(IDisposable).IsAssignableFrom(typeof(TMessageType));

		/// <summary>
		/// Unity3D called update tick.
		/// </summary>
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

		/// <inheritdoc />
		public Task DispatchMessageAsync(TMessageType message)
		{
			lock(SyncObj)
			{
				//Assume it's been disposed of because it's been dispatched outside of our control.
				if(isDisposableType && isNewMessageAvailable && !Equals(LatestMessage, default(TMessageType))) //ant not null
					((IDisposable)LatestMessage).Dispose();

				LatestMessage = message;
				isNewMessageAvailable = true;
			}

			return Task.CompletedTask;
		}
	}
}
