using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.GoogleDrive.DiskExtensions
{
	public static class DiskExtension
	{
		public static IGoogleDriveReader GetGoogleDriverReader(this IDisk disk, string userName) => new GoogleDriveReader(disk, userName);
	}
}
