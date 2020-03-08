using System;
using System.Collections.Generic;
using System.Text;
using Reinterpret.Net;

namespace Glarduino
{
	public static class GlarduinoClientConstants
	{
		/// <summary>
		/// The magical number constant.
		/// Based on: 0xD9B4BEF9
		/// See: https://bitcoin.stackexchange.com/questions/43189/what-is-the-magic-number-used-in-the-block-structure
		/// </summary>
		public const int GLARDUINO_CONNECTION_START_MAGIC_NUMBER = -642466055;

		/// <summary>
		/// Represents the 4-byte representation of <see cref="GLARDUINO_CONNECTION_START_MAGIC_NUMBER"/>.
		/// </summary>
		public static byte[] MagicalByteNumber = GlarduinoClientConstants.GLARDUINO_CONNECTION_START_MAGIC_NUMBER.Reinterpret();
	}
}
