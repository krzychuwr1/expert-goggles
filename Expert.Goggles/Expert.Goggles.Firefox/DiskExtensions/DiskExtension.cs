using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Firefox.DiskExtensions
{
	public static class DiskExtension
	{
		public static IFirefoxReader GetFirefoxReader(this IDisk disk, string userName) => new FirefoxReader(disk, userName);
	}
}
