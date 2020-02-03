using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public sealed class ConnectionEvents : IConnectionEventsSubscribable
	{
		public event EventHandler OnClientConnected;

		public event EventHandler OnClientDisconnected;

		public void InvokeClientConnected()
		{
			OnClientConnected?.Invoke(this, EventArgs.Empty);
		}

		public void InvokeClientDisconnected()
		{
			OnClientDisconnected?.Invoke(this, EventArgs.Empty);
		}
	}
}
