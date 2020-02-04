using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Glarduino
{
	public sealed class TestStringMessageCaptureStrategy : IMessageDispatchingStrategy<string>
	{
		private string ExpectedString { get; }

		public TestStringMessageCaptureStrategy(string expectedString)
		{
			ExpectedString = expectedString ?? throw new ArgumentNullException(nameof(expectedString));
		}

		public async Task DispatchMessageAsync(string message)
		{
			Assert.AreEqual(ExpectedString, message);
		}
	}
}
