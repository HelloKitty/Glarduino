using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public class HarcodedSizeQuaternionSegmentMessageDeserializerStrategy : QuaternionSegmentMessageDeserializerStrategy
	{
		private int HarcodedSize { get; }

		public HarcodedSizeQuaternionSegmentMessageDeserializerStrategy(int harcodedSize)
		{
			if (harcodedSize < 0) throw new ArgumentOutOfRangeException(nameof(harcodedSize));

			HarcodedSize = harcodedSize;
		}

		protected override Task<byte> GetQuaternionCountAsync(ICommunicationPort serialPort, CancellationToken cancellationToken)
		{
			return Task.FromResult((byte)HarcodedSize);
		}
	}
}
