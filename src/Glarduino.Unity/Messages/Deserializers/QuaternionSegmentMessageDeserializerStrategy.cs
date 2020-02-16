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
	/// <summary>
	/// <see cref="Quaternion"/> <see cref="RecyclableArraySegment{T}"/> implementation of <see cref="IMessageDeserializerStrategy{TMessageType}"/>.
	/// Reading incoming <see cref="Quaternion"/> chunks as 4 4-byte float data chunks with a leading 1 byte <see cref="Quaternion"/> array length.
	/// </summary>
	public class QuaternionSegmentMessageDeserializerStrategy : IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>>
	{
		/// <summary>
		/// Reusable single byte buffer.
		/// </summary>
		private byte[] SingleByteBuffer { get; } = new byte[1];

		/// <summary>
		/// Reusuable <see cref="Quaternion"/> byte buffer.
		/// </summary>
		private byte[] SingleQuatBuffer { get; } = new byte[sizeof(float) * 4];

		/// <inheritdoc />
		public async Task<RecyclableArraySegment<Quaternion>> ReadMessageAsync(ICommunicationPort serialPort, CancellationToken cancellationToken = default(CancellationToken))
		{
			byte quaternionCount = await GetQuaternionCountAsync(serialPort, cancellationToken);

			if (quaternionCount == 0)
				return RecyclableArraySegment<Quaternion>.Empty;

			//Now we should read the quats based on the provided count
			var quatArray = ArrayPool<Quaternion>.Shared.Rent(quaternionCount);

			try
			{
				for (int i = 0; i < quaternionCount && !cancellationToken.IsCancellationRequested; i++)
				{
					await serialPort.ReadAsync(SingleQuatBuffer, 0, SingleQuatBuffer.Length, cancellationToken);

					//Just return whatever we have if cancelled.
					if(cancellationToken.IsCancellationRequested)
						return new RecyclableArraySegment<Quaternion>(quatArray, 0, quaternionCount);

					//quatArray[i] = new Quaternion(GetFloatFromQuatBuffer(1), GetFloatFromQuatBuffer(2), GetFloatFromQuatBuffer(3), GetFloatFromQuatBuffer(0));
					quatArray[i] = new Quaternion(GetFloatFromQuatBuffer(0), GetFloatFromQuatBuffer(1), GetFloatFromQuatBuffer(2), GetFloatFromQuatBuffer(3));
					Debug.Log($"Quat log: {quatArray[i].x} {quatArray[i].y} {quatArray[i].z} {quatArray[i].w}");
				}

				return new RecyclableArraySegment<Quaternion>(quatArray, 0, quaternionCount);
			}
			catch (Exception e)
			{
				throw;
			}
			finally
			{
				//If exceptions happen let's dipose of the array
				ArrayPool<Quaternion>.Shared.Return(quatArray);
			}
		}

		/// <summary>
		/// Gets the size of the incoming <see cref="Quaternion"/> array.
		/// </summary>
		/// <param name="serialPort">The communication port.</param>
		/// <param name="cancellationToken">Optional cancel token.</param>
		/// <returns>Awaitable byte future.</returns>
		protected virtual async Task<byte> GetQuaternionCountAsync(ICommunicationPort serialPort, CancellationToken cancellationToken)
		{
			await serialPort.ReadAsync(SingleByteBuffer, 0, 1, cancellationToken);
			byte quaternionCount = SingleByteBuffer[0];
			return quaternionCount;
		}

		/// <summary>
		/// Reads a <see cref="float"/> value from the <see cref="SingleQuatBuffer"/>.
		/// </summary>
		/// <param name="index">The index to read.</param>
		/// <returns>A float value at the specified <paramref name="index"/>.</returns>
		private float GetFloatFromQuatBuffer(int index)
		{
			return Unsafe.ReadUnaligned<float>(ref SingleQuatBuffer[sizeof(float) * index]);
		}
	}
}
