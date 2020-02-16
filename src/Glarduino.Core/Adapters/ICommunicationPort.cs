using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that provide access to a stream-based communication port.
	/// </summary>
	public interface ICommunicationPort : IDisposable
	{
		/// <summary>
		/// Indicates if the port is open.
		/// </summary>
		bool IsOpen { get; }

		/// <summary>
		/// Mutable read timeout in milliseconds.
		/// </summary>
		int ReadTimeout { get; set; }

		/// <summary>
		/// Mutable write timeout in milliseconds.
		/// </summary>
		int WriteTimeout { get; set; }

		/// <summary>
		/// The default string encoding.
		/// </summary>
		Encoding Encoding { get; }

		/// <summary>
		/// The communication stream.
		/// </summary>
		Stream BaseStream { get; }

		/// <summary>
		/// Synchronously opens the communication port.
		/// </summary>
		/// <param name="cancelToken"></param>
		void Open(CancellationToken cancelToken = default(CancellationToken));

		/// <summary>
		/// Synchronously closes the communication port.
		/// </summary>
		void Close();
	}
}
