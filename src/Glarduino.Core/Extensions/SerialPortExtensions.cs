using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	public static class SerialPortExtensions
	{
		/// <summary>
		/// Reads a chunk into the provided <paramref name="buffer"/> from the provided <see cref="SerialPort"/>.
		/// </summary>
		/// <param name="serialPort">The port to read from.</param>
		/// <param name="buffer">The buffer to read into.</param>
		/// <param name="offset">The offset into the buffer.</param>
		/// <param name="count">The count of bytes to read.</param>
		/// <returns>Awaitable for when the operation is completed.</returns>
		public static async Task ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count)
		{
			if (!serialPort.IsOpen)
				throw new InvalidOperationException($"Provided {nameof(serialPort)} is not in an open state. Cannot read.");

			var bytesRead = 0;

			while (bytesRead < count)
			{
				int readBytes = await serialPort.BaseStream.ReadAsync(buffer, bytesRead + offset, count - bytesRead);
				bytesRead += readBytes;
			}
		}
	}
}
