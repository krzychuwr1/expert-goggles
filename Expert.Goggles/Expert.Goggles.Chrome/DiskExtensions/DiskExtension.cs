using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Chrome.DiskExtensions
{
	public static class DiskExtension
	{
		public static IGoogleChromeReader GetGoogleChromeReader(this IDisk disk, string userName) => new GoogleChromeReader(disk, userName);
	}
}
