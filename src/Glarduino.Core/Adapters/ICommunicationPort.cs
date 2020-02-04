using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	public interface ICommunicationPort : IDisposable
	{
		bool IsOpen { get; }

		int ReadTimeout { get; set; }

		int WriteTimeout { get; set; }

		Encoding Encoding { get; }

		Stream BaseStream { get; }

		void Open();

		void Close();
	}
}
