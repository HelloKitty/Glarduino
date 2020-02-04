using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	public sealed class IntMessageDeserializerStrategy : IMessageDeserializerStrategy<int>
	{
		//TODO: This isn't thread safe.
		private readonly byte[] IntBuffer = new byte[sizeof(int)];

		public async Task<int> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			await serialPort.ReadAsync(IntBuffer, 0, sizeof(int), cancellationToken);
			return Unsafe.ReadUnaligned<int>(ref IntBuffer[0]);
		}
	}
}
