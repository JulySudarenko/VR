using System.Collections.Generic;
using UnityEngine;

namespace GeekBrains.Controllers
{
	public class BotController : BaseController
	{
		private int _priority = -1;
		public int CountBot = 500;
		public List<Bot> GetBotList = new List<Bot>();

		private void Start()
		{
			Init();
		}
		public void Init()
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
			{
				CreateBot(CountBot);
				var tempTarget = player.transform;
				foreach (var tempBot in GetBotList)
				{
					tempBot.Agent.avoidancePriority = ++_priority;
					tempBot.Target = tempTarget;
				}
			}
		}
		public void AddBotToList(Bot bot)
		{
			if (!GetBotList.Contains(bot))
			{
				GetBotList.Add(bot);
			}
		}
		public void RemoveBotToList(Bot bot)
		{
			if (GetBotList.Contains(bot))
			{
				GetBotList.Remove(bot);
			}
		}

		private void CreateBot(int count)
		{
			var loadBot = Resources.Load<Bot>("Bot");
			if (loadBot == null) return;
			for (var i = 0; i < count; i++)
			{
				var tempBot = Instantiate(loadBot, new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25)), Quaternion.identity);
				AddBotToList(tempBot);
			}
		}
	}

}