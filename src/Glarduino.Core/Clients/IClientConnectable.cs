using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	public interface IClientConnectable
	{
		/// <summary>
		/// Opens the underlying Arduino port.
		/// </summary>
		/// <returns></returns>
		Task<bool> ConnectAsync();
	}
}
