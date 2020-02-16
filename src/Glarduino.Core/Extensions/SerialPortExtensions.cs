using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	public static class SerialPortExtensions
	{
		/// <summary>
		/// From https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.IO.Ports/src/System/IO/Ports/SerialPort.cs#L36
		/// </summary>
		private const char DefaultNewLine = '\n';

		public const int InfiniteTimeout = -1;

		/// <summary>
		/// Reads a chunk into the provided <paramref name="buffer"/> from the provided <see cref="SerialPort"/>.
		/// </summary>
		/// <param name="serialPort">The port to read from.</param>
		/// <param name="buffer">The buffer to read into.</param>
		/// <param name="offset">The offset into the buffer.</param>
		/// <param name="count">The count of bytes to read.</param>
		/// <param name="cancellationToken">Optional cancellation token for the read operation.</param>
		/// <returns>Awaitable for when the operation is completed.</returns>
		public static async Task ReadAsync(this ICommunicationPort serialPort, byte[] buffer, int offset, int count, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (serialPort == null) throw new ArgumentNullException(nameof(serialPort));
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			if (!serialPort.IsOpen)
				throw new InvalidOperationException($"Provided {nameof(serialPort)} is not in an open state. Cannot read.");

			for (int i = 0; i < count && !cancellationToken.IsCancellationRequested;)
			{
				int readBytes = await serialPort.BaseStream.ReadAsync(buffer, i + offset, count - i, cancellationToken)
					.ConfigureAwait(false);

				i += readBytes;
			}
		}

		/// <summary>
		/// Read a line from the SerialPort asynchronously
		/// </summary>
		/// <param name="serialPort">The port to read data from</param>
		/// <returns>A line read from the input</returns>
		public static async Task<string> ReadLineAsync(this ICommunicationPort serialPort)
		{
			if(!serialPort.Encoding.IsSingleByte)
				throw new InvalidOperationException($"TODO: Cannot support {serialPort.Encoding.EncodingName}. Only supports 1 byte ASCII right now.");

			int charSizeCount = serialPort.Encoding.IsSingleByte ? 1 : serialPort.Encoding.GetMaxByteCount(1);
			byte[] _singleCharBuffer = ArrayPool<byte>.Shared.Rent(charSizeCount);
			StringBuilder builder = new StringBuilder();

			try
			{
				// Read the input one byte at a time, convert the
				// byte into a char, add that char to the overall
				// response string, once the response string ends
				// with the line ending then stop reading
				while (true)
				{
					await serialPort.ReadAsync(_singleCharBuffer, 0, 1)
						.ConfigureAwait(false);

					builder.Append((char) _singleCharBuffer[0]);

					if (builder[builder.Length - 1] == DefaultNewLine)
						// Truncate the line ending
						return builder.ToString(0, builder.Length - 1);
				}
			}
			catch (Exception e)
			{
				throw;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(_singleCharBuffer, true);
			}
		}
	}
}
