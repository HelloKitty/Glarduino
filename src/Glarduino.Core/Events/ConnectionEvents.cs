using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Default implementation of <see cref="IConnectionEventsSubscribable"/>.
	/// </summary>
	public sealed class ConnectionEvents : IConnectionEventsSubscribable
	{
		/// <inheritdoc />
		public event EventHandler OnClientConnected;

		/// <inheritdoc />
		public event EventHandler OnClientDisconnected;

		/// <summary>
		/// Invokes the <see cref="OnClientConnected"/> event.
		/// </summary>
		public void InvokeClientConnected()
		{
			OnClientConnected?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Invokes the <see cref="OnClientDisconnected"/> event.
		/// </summary>
		public void InvokeClientDisconnected()
		{
			OnClientDisconnected?.Invoke(this, EventArgs.Empty);
		}
	}
}
