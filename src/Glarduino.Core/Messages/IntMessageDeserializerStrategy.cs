using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// <see cref="int"/>-based implementation of <see cref="IMessageDeserializerStrategy{TMessageType}"/>
	/// that reads a 4 byte chunk and interprets it as an <see cref="int"/>.
	/// </summary>
	public sealed class IntMessageDeserializerStrategy : IMessageDeserializerStrategy<int>
	{
		//TODO: This isn't thread safe.
		/// <summary>
		/// Rusable 4 byte byte buffer for temp storage of incoming int chunks.
		/// </summary>
		private readonly byte[] IntBuffer = new byte[sizeof(int)];

		/// <inheritdoc />
		public async Task<int> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			await serialPort.ReadAsync(IntBuffer, 0, sizeof(int), cancellationToken);
			return Unsafe.ReadUnaligned<int>(ref IntBuffer[0]);
		}
	}
}
