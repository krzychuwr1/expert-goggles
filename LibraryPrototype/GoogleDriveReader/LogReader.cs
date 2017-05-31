using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleDrive
{
    public class LogReader
    {
        public static void GetFilesHistoryFromLogs(string logsPath)
        {
            var logs = GenereteLogs(logsPath).Take(5).ToList();
        }

        private static IEnumerable<FileLogEntry> GenereteLogs(string logsPath)
        {
            var reg = new Regex(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}.*", RegexOptions.Compiled);
            foreach (var line in File.ReadLines(logsPath))
            {
                if (reg.IsMatch(line))
                {
                    var lineParts = line.Split(' ');
                    yield return new FileLogEntry()
                    {
                        Date = DateTime.Parse($"{lineParts[0]} {lineParts[1].Replace(',', '.')}"),
                        LogLevel = lineParts[3],
                        Log = lineParts.Skip(6).Aggregate((acc, curr) => acc + " " + curr).TrimStart(' ')
                    };
                }
            }
        }
    }
}
