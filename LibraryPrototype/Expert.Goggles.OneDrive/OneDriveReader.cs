using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.OneDrive
{
	public interface IOneDriveReader { }

	public class OneDriveReader : IOneDriveReader
	{
		private readonly IDisk _disk;
		private readonly string _username;

		public OneDriveReader(IDisk disk, string username)
		{
			_disk = disk;
			_username = username;
		}
	}
}