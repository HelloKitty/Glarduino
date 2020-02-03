using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public interface IConnectionEventsSubscribable
	{
		event EventHandler OnClientConnected;

		event EventHandler OnClientDisconnected;
	}
}
