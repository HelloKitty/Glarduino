using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Conctract for types that can consume or listen for
	/// messages produced by the Glardiuno client.
	/// </summary>
	/// <typeparam name="TMessageType">The type of the message.</typeparam>
	public interface IMessageListener<in TMessageType>
	{
		/// <summary>
		/// Called when a message is recieved or processed.
		/// </summary>
		/// <param name="message">The message.</param>
		void OnMessage(TMessageType message);
	}
}
