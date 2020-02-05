using System;
using System.Collections.Generic;
using System.Text;

namespace Glarduino
{
	/// <summary>
	/// Base type for Unity3D-based <see cref="BaseGlarduinoClient{TMessageType}"/> implementations.
	/// </summary>
	/// <typeparam name="TMessageType"></typeparam>
	public abstract class BaseUnityGlardunioClient<TMessageType> : BaseGlarduinoClient<TMessageType>
	{
		/// <summary>
		/// Indicates if the client has been disposed.
		/// </summary>
		private bool unityDisposed = false;

		/// <inheritdoc />
		protected BaseUnityGlardunioClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<TMessageType> messageDeserializer, 
			IMessageDispatchingStrategy<TMessageType> messageDispatcher) 
			: base(connectionInfo, messageDeserializer, messageDispatcher)
		{

		}

		/// <inheritdoc />
		protected BaseUnityGlardunioClient(ArduinoPortConnectionInfo connectionInfo, 
			IMessageDeserializerStrategy<TMessageType> messageDeserializer, 
			IMessageDispatchingStrategy<TMessageType> messageDispatcher, 
			ICommunicationPort comPort) 
			: base(connectionInfo, messageDeserializer, messageDispatcher, comPort)
		{

		}

		//Override to only indicate being connected if the application is in playmode too.
		/// <inheritdoc />
		public override bool isConnected => base.isConnected && !unityDisposed;

		/// <inheritdoc />
		public override void Dispose()
		{
			unityDisposed = true;
			base.Dispose();
		}
	}
}
