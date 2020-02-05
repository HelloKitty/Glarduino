using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Glarduino
{
	/// <summary>
	/// Simplified generic type <see cref="string"/>-based implementation of <see cref="BaseUnityGlardunioClient{TMessageType}"/>
	/// </summary>
	public sealed class UnityStringGlarduinoClient : BaseUnityGlardunioClient<string>
	{
		/// <inheritdoc />
		public UnityStringGlarduinoClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<string> messageDeserializer, 
			IMessageDispatchingStrategy<string> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}
	}
}
