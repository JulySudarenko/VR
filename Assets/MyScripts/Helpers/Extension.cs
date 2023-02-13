using System;
using UnityEngine;

namespace GeekBrains.Helpers
{
	public static class Extension
	{
		public static bool TryBool(this string self)
		{
			bool res;
			return Boolean.TryParse(self, out res) && res;
		}

		public static string PathCombine(this string self, string path)
		{
			return System.IO.Path.Combine(self, path);
		}

		public static T GetOrAddComponent<T>(this Component child) where T : Component
		{
			T result = child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
			return result;
		}

		public static string Format(this string self, params object[] args)
		{
			return String.Format(self,args);
		}
	}
}