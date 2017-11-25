using System.Collections.Generic;
using System.Linq;
using Expert.Goggles.Core.Interfaces.Disk;

namespace Expert.Goggles.Detector
{
	public interface IDetector
	{
		IEnumerable<string> GetWindowsUsers();
		IEnumerable<string> GetAppsForWindowsUser(string userName);
	}

    public class Detector : IDetector
    {
	    private readonly IDisk _disk;

	    public Detector(IDisk disk)
	    {
		    _disk = disk;
	    }

	    public IEnumerable<string> GetWindowsUsers() => _disk.GetDirectorySubdirectories("Users/").Where(d => !NonUserFolders.Contains(d) && !d.Contains(".NET"));

	    public IEnumerable<string> GetAppsForWindowsUser(string userName) 
			=> from app in GetAppsLocations(userName) where _disk.CheckIfDirectoryExists(app.Path) select app.Name;

	    private IEnumerable<(string Path, string Name)> GetAppsLocations(string userName) => new List<(string Path, string Name)>
	    {
		    ($@"Users/{userName}/AppData/Local/Google/Chrome", AppNames.Chrome),
			($@"Users\{userName}\AppData\Roaming\Mozilla\Firefox", AppNames.Firefox),
			($@"Users/{userName}/AppData/Local/Google/Drive", AppNames.GoogleDrive),
			($@"Users\{userName}\AppData\Local\Packages\Microsoft.SkypeApp_kzf8qxf38zg5c\LocalState", AppNames.Skype)
	    };

	    private static readonly List<string> NonUserFolders = new List<string>
	    {
		    "DefaultAppPool", "Default", "Public"
	    };
    }
}
