using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expert.Goggles.Core.Model
{
	public interface IFileActionEntry
	{
		DateTime? Date { get; }
		string FileName { get; }
		long? FileSize { get; }
	}
}
