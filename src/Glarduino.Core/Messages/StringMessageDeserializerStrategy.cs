using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	public sealed class StringMessageDeserializerStrategy : IMessageDeserializerStrategy<string>
	{
		public Task<string> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serialPort.ReadLineAsync();
		}
	}
}
