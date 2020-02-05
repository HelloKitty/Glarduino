using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// <see cref="string"/>-based implementation of <see cref="IMessageDeserializerStrategy{TMessageType}"/>
	/// that reads a <see cref="string"/> based on <see cref="ICommunicationPort"/> semantics.
	/// </summary>
	public sealed class StringMessageDeserializerStrategy : IMessageDeserializerStrategy<string>
	{
		/// <inheritdoc />
		public Task<string> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serialPort.ReadLineAsync();
		}
	}
}
