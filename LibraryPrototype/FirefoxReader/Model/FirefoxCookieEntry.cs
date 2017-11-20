using System;

namespace ExpertGoggles.Firefox.Model
{
	public class FirefoxCookieEntry
	{
		public string Url;
		public string Name;
		public DateTime CreationTime;
		public DateTime LastAccessed;
		public DateTime ExpiryTime;
		public string Value;

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
