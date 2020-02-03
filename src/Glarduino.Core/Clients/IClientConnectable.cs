using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	/// <summary>
	/// Contract for types that are client connectable.
	/// </summary>
	public interface IClientConnectable
	{
		/// <summary>
		/// Opens the underlying Arduino port.
		/// </summary>
		/// <returns></returns>
		Task<bool> ConnectAsync();
	}
}
