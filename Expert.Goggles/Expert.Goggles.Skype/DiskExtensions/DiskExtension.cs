using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Skype.DiskExtensions
{
	public static class DiskExtension
	{
		public static ISkypeReader GetSkypeReader(this IDisk disk, string userName) => new SkypeReader(disk, userName);
	}
}
