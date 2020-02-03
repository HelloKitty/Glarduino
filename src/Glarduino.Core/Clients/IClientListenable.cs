using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that are client listenable
	/// </summary>
	public interface IClientListenable
	{
		/// <summary>
		/// Starts the client listening.
		/// </summary>
		/// <returns>Awaitable that completes when the client finishes listening.</returns>
		Task StartListeningAsync();
	}
}
