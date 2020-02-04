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

			for (int i = 0; i < count;)
			{
				int readBytes = await serialPort.BaseStream.ReadAsync(buffer, i + offset, count - i, cancellationToken);

				i += readBytes;
			}
		}

		/// <summary>
		/// Based on: https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.IO.Ports/src/System/IO/Ports/SerialPort.cs#L1001
		/// </summary>
		/// <param name="serialPort"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task<string> ReadLineAsync(this ICommunicationPort serialPort)
		{
			return ReadToAsync(serialPort, DefaultNewLine);
		}

		/// <summary>
		/// Base on: https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.IO.Ports/src/System/IO/Ports/SerialPort.cs#L1006
		/// </summary>
		/// <param name="serialPort"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static async Task<string> ReadToAsync(this ICommunicationPort serialPort, char value)
		{
			if(!serialPort.IsOpen)
				throw new InvalidOperationException($"Provided {nameof(serialPort)} is not in an open state. Cannot read.");

			int timeUsed = 0;
			int timeNow;
			StringBuilder currentLine = new StringBuilder();
			char lastValueChar = value;

			int charSizeCount = serialPort.Encoding.IsSingleByte ? 1 : serialPort.Encoding.GetMaxByteCount(1);
			byte[] _singleCharBuffer = ArrayPool<byte>.Shared.Rent(charSizeCount);

			try
			{
				while (true)
				{
					if (serialPort.ReadTimeout == InfiniteTimeout)
					{
						//One will be read. when completed
						await serialPort.ReadAsync(_singleCharBuffer, 0, charSizeCount);
					}
					else if (serialPort.ReadTimeout - timeUsed >= 0)
					{
						timeNow = Environment.TickCount;
						await serialPort.ReadAsync(_singleCharBuffer, 0, charSizeCount, new CancellationTokenSource(serialPort.ReadTimeout - timeUsed).Token);
						timeUsed += Environment.TickCount - timeNow;
					}
					else
						throw new TimeoutException();

					char charVal = ComputeCharValue(serialPort.Encoding, _singleCharBuffer, charSizeCount);
					currentLine.Append(charVal);

					if (lastValueChar == (char)charVal)
					{
						// we found the search string.  Exclude it from the return string.
						string ret = currentLine.ToString(0, currentLine.Length - 1);
						return ret;
					}
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

		private static unsafe char ComputeCharValue(Encoding serialPortEncoding, byte[] singleCharBuffer, int charSizeCount)
		{
			if(charSizeCount == 1)
			{
				char outChar = (char)singleCharBuffer[0];
				return outChar;
			}
			else
				throw new NotImplementedException($"TODO: Support other encodings: {serialPortEncoding.EncodingName} not supported yet.");

			/*char outValue;
			//It's going to be 1 char but this is fine.
			fixed (byte* charPtr = singleCharBuffer)
			{
				
			}*/
		}
	}
}
