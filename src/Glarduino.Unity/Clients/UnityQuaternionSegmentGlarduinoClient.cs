using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Simplified generic type <see cref="RecyclableArraySegment{T}"/>-based implementation of <see cref="BaseUnityGlarduinoClient{TMessageType}"/>
	/// </summary>
	public sealed class UnityQuaternionSegmentGlarduinoClient : BaseGlarduinoClient<RecyclableArraySegment<Quaternion>>
	{
		/// <inheritdoc />
		public UnityQuaternionSegmentGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<RecyclableArraySegment<Quaternion>> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		/// <inheritdoc />
		public UnityQuaternionSegmentGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<RecyclableArraySegment<Quaternion>> messageDeserializer, 
			IMessageDispatchingStrategy<RecyclableArraySegment<Quaternion>> messageDispatcher, 
			ICommunicationPort comPort) 
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{

		}
	}
}
