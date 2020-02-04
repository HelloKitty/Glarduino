using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public interface ICommunicationPort : IDisposable
	{
		bool IsOpen { get; set; }

		int ReadTimeout { get; set; }

		int WriteTimeout { get; set; }

		void Open();

		void Close();
	}
}
