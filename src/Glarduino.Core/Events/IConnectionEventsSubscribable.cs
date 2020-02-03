using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that can publish connection events.
	/// </summary>
	public interface IConnectionEventsSubscribable
	{
		/// <summary>
		/// Event for client connection.
		/// </summary>
		event EventHandler OnClientConnected;

		/// <summary>
		/// Event for client disconnection.
		/// </summary>
		event EventHandler OnClientDisconnected;
	}
}
