using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Firefox.Model
{
	public class FirefoxCookieEntry : ICookieEntry
	{
		public string Url { get; }
		public string Name { get; }
		public DateTime CreationTime { get; }
		public DateTime LastAccessed { get; }
		public DateTime ExpiryTime { get; }
		public string Value { get; }

		public FirefoxCookieEntry(string url, string name, string value, DateTime creationTime, DateTime lastAccessed, DateTime expiryTime)
		{
			Url = url;
			Name = name;
			Value = value;
			CreationTime = creationTime;
			LastAccessed = lastAccessed;
			ExpiryTime = expiryTime;
		}
	}
}
