using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expert.Goggles.Core.Model
{
	public interface ITextMessageEntry
	{
		string Content { get; set; }
		string Author { get; set; }
	}
}
