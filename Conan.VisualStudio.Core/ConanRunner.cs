using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Conan.VisualStudio.Core
{
    public class ConanRunner
    {
        private readonly string _executablePath;

        public ConanRunner(string executablePath) =>
            _executablePath = executablePath;

        public Task<Process> Install(ConanProject project)
        {
            string Escape(string arg) => arg.Contains(" ") ? $"\"{arg}\"" : arg;
            string ProcessArgument(string name, string value) => $"-s {name}={Escape(value)}";

            var path = project.Path;
            const string generatorName = "visual_studio_multi";
            var settingValues = new[]
            {
                ("compiler.version", project.CompilerVersion)
            };
            const string options = "--build missing --update";

            var settings = string.Join(" ", settingValues.Select(pair =>
            {
                var (key, value) = pair;
                return ProcessArgument(key, value);
            }));
            var arguments = $"install {Escape(path)} " +
                            $"-g {generatorName} " +
                            $"--install-folder {Escape(project.InstallPath)} " +
                            $"{settings} {options}";

            var startInfo = new ProcessStartInfo
            {
                FileName = _executablePath,
                Arguments = arguments,
                UseShellExecute = false,
                WorkingDirectory = path,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            return Task.Run(() => Process.Start(startInfo));
        }
    }
}
