using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Simplified type interface for <see cref="Quaternion"/> <see cref="RecyclableArraySegment{T}"/> <see cref="IMessageListener{TMessageType}"/> implementation.
	/// </summary>
	public interface IQuaternionSegmentMessageListener : IMessageListener<RecyclableArraySegment<Quaternion>>
	{

	}
}
