using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Simplified generic type <see cref="int"/>-based implementation of <see cref="BaseUnityGlarduinoClient{TMessageType}"/>
	/// </summary>
	public sealed class UnityIntGlarduinoClient : BaseUnityGlarduinoClient<int>
	{
		/// <inheritdoc />
		public UnityIntGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<int> messageDeserializer, 
			IMessageDispatchingStrategy<int> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}
	}
}
