using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glarduino
{
	public sealed class TestStringMessageCaptureStrategy : IMessageDispatchingStrategy<string>
	{
		public List<string> ReadStrings { get; } = new List<string>();

		public async Task DispatchMessageAsync(string message)
		{
			Console.WriteLine(message);
			ReadStrings.Add(message);
		}
	}
}
