using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class DebugLogStringMessageDispatchingStrategy : IMessageDispatchingStrategy<string>
	{
		public Task DispatchMessageAsync(string message)
		{
			Debug.Log(message);
			return Task.CompletedTask;
		}
	}
}
