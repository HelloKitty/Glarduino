using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Static/global configuration for Glardunio.
	/// </summary>
	public static class GlardunioStaticConfig
	{
		/// <summary>
		/// Mutable static configuration property that determines if
		/// Glardunio will spit out debug logging info.
		/// </summary>
		public static bool DebugLogging { get; set; } = false;
	}
}
