using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Glarduino
{
	public sealed class DebugLogIntMessageDispatchingStrategy : IMessageDispatchingStrategy<int>
	{
		public Task DispatchMessageAsync(int message)
		{
			Debug.Log(message.ToString());
			return Task.CompletedTask;
		}
	}
}
