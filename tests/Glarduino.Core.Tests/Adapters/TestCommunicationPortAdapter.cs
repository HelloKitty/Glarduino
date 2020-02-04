using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Glarduino
{
	public sealed class TestCommunicationPortAdapter : ICommunicationPort
	{
		private bool _isOpen;
		private int _readTimeout = 1000;
		private int _writeTimeout = 1000;

		public bool IsOpen => _isOpen;

		public int ReadTimeout
		{
			get => _readTimeout;
			set => _readTimeout = value;
		}

		public int WriteTimeout
		{
			get => _writeTimeout;
			set => _writeTimeout = value;
		}

		public Encoding Encoding => Encoding.ASCII;

		public Stream BaseStream { get; }

		public TestCommunicationPortAdapter(Stream baseStream)
		{
			BaseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
		}

		public void Open()
		{
			_isOpen = true;
		}

		public void Close()
		{
			_isOpen = false;
		}

		public void Dispose()
		{

		}
	}
}
