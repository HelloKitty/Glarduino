using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	public static class SerialPortExtensions
	{
		public static async Task ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count)
		{
			var bytesRead = 0;

			while (bytesRead < count)
			{
				int readBytes = await serialPort.BaseStream.ReadAsync(buffer, bytesRead + offset, count - bytesRead);
				bytesRead += readBytes;
			}
		}

		public static async Task<byte[]> ReadAsync(this SerialPort serialPort, int count)
		{
			var buffer = new byte[count];
			await serialPort.ReadAsync(buffer, 0, count);
			return buffer;
		}
	}
}
