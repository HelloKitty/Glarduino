using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that implement message dispatching.
	/// </summary>
	/// <typeparam name="TMessageType">The message type.</typeparam>
	public interface IMessageDispatchingStrategy<in TMessageType>
	{
		/// <summary>
		/// Dispatches the provided <see cref="message"/> with the implemented
		/// strategy.
		/// </summary>
		/// <param name="message">The message object.</param>
		/// <returns>Awaitable for when the dispatching process has completed.</returns>
		Task DispatchMessageAsync(TMessageType message);
	}
}
