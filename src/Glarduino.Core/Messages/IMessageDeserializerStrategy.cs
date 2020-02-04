using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that handle reading from a serial port and deserializing a higher level
	/// message of a specific type to the stream.
	/// </summary>
	/// <typeparam name="TMessageType"></typeparam>
	public interface IMessageDeserializerStrategy<TMessageType>
	{
		/// <summary>
		/// Reads and deserializers the message of type <typeparamref name="TMessageType"/> from the provided port <paramref name="serialPort"/>.
		/// </summary>
		/// <param name="serialPort">The serial port to read from.</param>
		/// <param name="cancellationToken">Optional cancel token.</param>
		/// <returns>The deserialized message.</returns>
		Task<TMessageType> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken));
	}
}
