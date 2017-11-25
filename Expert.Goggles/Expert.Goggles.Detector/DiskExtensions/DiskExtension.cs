using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Detector.DiskExtensions
{
	public static class DiskExtension
	{
		public static IDetector GetDetector(this IDisk disk) => new Detector(disk);
	}
}
