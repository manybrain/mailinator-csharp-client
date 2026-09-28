using System;
using System.IO;

namespace mailinator_csharp_client_tests
{
    internal static class TestEnvironment
    {
        internal static void LoadDotEnv()
        {
            var dotEnvPath = FindDotEnv(Environment.CurrentDirectory) ?? FindDotEnv(AppDomain.CurrentDomain.BaseDirectory);
            if (dotEnvPath == null)
                return;

            foreach (var line in File.ReadAllLines(dotEnvPath))
            {
                var trimmedLine = line.Trim();
                if (trimmedLine.Length == 0 || trimmedLine.StartsWith("#"))
                    continue;

                if (trimmedLine.StartsWith("export "))
                    trimmedLine = trimmedLine.Substring("export ".Length).TrimStart();

                var separatorIndex = trimmedLine.IndexOf('=');
                if (separatorIndex <= 0)
                    continue;

                var name = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();
                if (value.Length >= 2 && ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") && value.EndsWith("'"))))
                    value = value.Substring(1, value.Length - 2);

                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                    Environment.SetEnvironmentVariable(name, value);
            }
        }

        private static string FindDotEnv(string startDirectory)
        {
            if (string.IsNullOrWhiteSpace(startDirectory))
                return null;

            var directory = new DirectoryInfo(startDirectory);
            while (directory != null)
            {
                var path = Path.Combine(directory.FullName, ".env");
                if (File.Exists(path))
                    return path;

                directory = directory.Parent;
            }

            return null;
        }
    }
}
