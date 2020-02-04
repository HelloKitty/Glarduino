using System.IO;
using System.Text;
using System.Threading.Tasks;
using Glarduino;
using NUnit.Framework;

namespace Glarduino.Tests
{
	public class Tests
	{
		[Test]
		public async Task Test_Can_Read_Hello()
		{
			//Arrange
			MemoryStream sharedStream = new MemoryStream();
			TestCommunicationPortAdapter port = new TestCommunicationPortAdapter(sharedStream);
			BaseStringGlarduinoClient client = new BaseStringGlarduinoClient(new ArduinoPortConnectionInfo("COM4", 4000), new StringMessageDeserializerStrategy(), new TestStringMessageCaptureStrategy(), port);
			sharedStream.Write(port.Encoding.GetBytes("Hello\n"));
			sharedStream.Position = 0;

			//act
			await client.ConnectAsync();
			await client.StartListeningAsync();
		}
	}
}