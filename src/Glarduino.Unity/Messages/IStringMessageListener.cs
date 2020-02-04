using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	public interface IStringMessageListener
	{
		void OnMessageArrived(string message);
	}
}
