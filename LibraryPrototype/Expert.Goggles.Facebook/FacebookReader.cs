using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Facebook
{
	public interface IFacebokReader { }

	public class FacebookReader : IFacebokReader
	{
		private readonly string _username;

		public FacebookReader(IDisk disk, string username)
		{
			_username = username;
		}
	}
}