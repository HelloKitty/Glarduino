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
	public sealed class QuaternionSegementMessageDeserializerStrategy : IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>>
	{
		private byte[] SingleByteBuffer { get; } = new byte[1];

		private byte[] SingleQuatBuffer { get; } = new byte[sizeof(float) * 4];

		public async Task<RecyclableArraySegment<Quaternion>> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			await serialPort.ReadAsync(SingleByteBuffer, 0, 1, cancellationToken);
			byte quaternionCount = SingleByteBuffer[0];

			if (quaternionCount == 0)
				return RecyclableArraySegment<Quaternion>.Empty;

			//Now we should read the quats based on the provided count
			var quatArray = ArrayPool<Quaternion>.Shared.Rent(quaternionCount);

			for(int i = 0; i < quaternionCount; i++)
			{
				await serialPort.ReadAsync(SingleQuatBuffer, 0, SingleQuatBuffer.Length, cancellationToken);
				quatArray[i] = new Quaternion(GetFloatFromQuatBuffer(3), GetFloatFromQuatBuffer(0), GetFloatFromQuatBuffer(1), GetFloatFromQuatBuffer(2));
			}

			return new RecyclableArraySegment<Quaternion>(quatArray, 0, quaternionCount);
		}

		private float GetFloatFromQuatBuffer(int index)
		{
			return Unsafe.ReadUnaligned<float>(ref SingleQuatBuffer[sizeof(float) * index]);
		}
	}
}
