using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
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
		private const string DefaultNewLine = "\n";

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
		public static async Task ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (serialPort == null) throw new ArgumentNullException(nameof(serialPort));
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			if (!serialPort.IsOpen)
				throw new InvalidOperationException($"Provided {nameof(serialPort)} is not in an open state. Cannot read.");

			var bytesRead = 0;

			while (bytesRead < count)
			{
				int readBytes = await serialPort.BaseStream.ReadAsync(buffer, bytesRead + offset, count - bytesRead, cancellationToken);
				bytesRead += readBytes;
			}
		}

		/// <summary>
		/// Based on: https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.IO.Ports/src/System/IO/Ports/SerialPort.cs#L1001
		/// </summary>
		/// <param name="serialPort"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task<string> ReadLineAsync(this SerialPort serialPort)
		{
			return ReadToAsync(serialPort, DefaultNewLine);
		}

		/// <summary>
		/// Base on: https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.IO.Ports/src/System/IO/Ports/SerialPort.cs#L1006
		/// </summary>
		/// <param name="serialPort"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static async Task<string> ReadToAsync(this SerialPort serialPort, string value)
		{
			if(!serialPort.IsOpen)
				throw new InvalidOperationException($"Provided {nameof(serialPort)} is not in an open state. Cannot read.");

			if(value == null)
				throw new ArgumentNullException(nameof(value));

			if(value.Length == 0)
				throw new ArgumentException($"Provided newline: {value} is invalid.", nameof(value));

			int numCharsRead;
			int timeUsed = 0;
			int timeNow;
			StringBuilder currentLine = new StringBuilder();
			char lastValueChar = value[value.Length - 1];
			
			byte[] _singleCharBuffer = ArrayPool<byte>.Shared.Rent(serialPort.Encoding.GetMaxByteCount(1));

			try
			{
				while (true)
				{
					if (serialPort.ReadTimeout == InfiniteTimeout)
					{
						//One will be read. when completed
						await serialPort.ReadAsync(_singleCharBuffer, 0, 1);
						numCharsRead = 1;
					}
					else if (serialPort.ReadTimeout - timeUsed >= 0)
					{
						timeNow = Environment.TickCount;
						await serialPort.ReadAsync(_singleCharBuffer, 0, 1, new CancellationTokenSource(serialPort.ReadTimeout - timeUsed).Token);
						numCharsRead = 1;
						timeUsed += Environment.TickCount - timeNow;
					}
					else
						throw new TimeoutException();

					Debug.Assert((numCharsRead > 0), "possible bug in ReadBufferIntoChars, reading surrogate char?");
					AppendCharacterBuffer(currentLine, _singleCharBuffer, numCharsRead);

					if (lastValueChar == (char) _singleCharBuffer[numCharsRead - 1] && (currentLine.Length >= value.Length))
					{
						// we found the last char in the value string.  See if the rest is there.  No need to
						// recompare the last char of the value string.
						bool found = true;
						for (int i = 2; i <= value.Length; i++)
						{
							if (value[value.Length - i] != currentLine[currentLine.Length - i])
							{
								found = false;
								break;
							}
						}

						if (found)
						{
							// we found the search string.  Exclude it from the return string.
							string ret = currentLine.ToString(0, currentLine.Length - value.Length);
							return ret;
						}
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

		private static unsafe void AppendCharacterBuffer(StringBuilder currentLine, byte[] singleCharBuffer, int numCharsRead)
		{
			//It's going to be 1 char but this is fine.
			fixed(void* charPtr = &singleCharBuffer[0])
				currentLine.Append((char*)charPtr, numCharsRead);
		}
	}
}
