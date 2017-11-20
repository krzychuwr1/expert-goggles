using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Expert.Goggles.GoogleDrive
{
    public class LogReader
    {
        public static IEnumerable<FileActionEntry> GetFilesHistoryFromLogs(Stream fileStream)
            => GetActionEntries(GetFileExtensionEntries(GenereteLogs(fileStream)));

        private static IEnumerable<FileLogEntry> GenereteLogs(Stream fileStream)
        {
            var reg = new Regex(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}.*", RegexOptions.Compiled);
	        var streamReader = new StreamReader(fileStream);
	        while (!streamReader.EndOfStream)
	        {
		        var line = streamReader.ReadLine();
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

        public static IEnumerable<FileLogEntry> GetFileExtensionEntries(IEnumerable<FileLogEntry> logs)
        {
            foreach(var log in logs)
            {
                if (log.Log.Contains("name=u'") && log.Log.Contains("FSChange("))
                {
                    log.Log = log.Log.Substring(log.Log.IndexOf("FSChange("));
                    yield return log;
                }
            }
        }

        public static IEnumerable<FileActionEntry> GetActionEntries(IEnumerable<FileLogEntry> logs)
        {
            foreach (var log in logs)
            {
                var fschangeParameters = new string(log.Log.SkipWhile(c => c != '(').ToArray()).Trim('(', ')').Split(',');

                var fileName = filterFSChangeParameter(fschangeParameters, "name", '\'');

                var direction = Enum.Parse(typeof(Direction), filterFSChangeParameter(fschangeParameters, "Direction", '.')) as Direction?;

                var action = Enum.Parse(typeof(Action), filterFSChangeParameter(fschangeParameters, "Action", '.')) as Action?;

                var path = filterFSChangeParameter(fschangeParameters, "path", '\'').TrimStart('\\', '?').Replace(@"\\", @"\");
                
                long.TryParse(filterFSChangeParameter(fschangeParameters, "size", '='), out var fileSize);

                yield return new FileActionEntry()
                {
                    FileName = fileName,
                    Direction = direction,
                    Action = action,
                    Date = log.Date,
                    Path = path,
                    FileSize = fileSize
                };
            }
        }

        private static string filterFSChangeParameter(string[] fschangeParameters, string parameterName, char trimChar)
            => new string(fschangeParameters.FirstOrDefault(f => f.Trim().StartsWith(parameterName))?.SkipWhile(c => c != trimChar)?.ToArray())?.Trim(trimChar);
    }

}
