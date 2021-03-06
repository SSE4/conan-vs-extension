using System;
using System.Threading.Tasks;
using Conan.VisualStudio.Core;
using Microsoft.VisualStudio.VCProjectEngine;
using EnvDTE;

namespace Conan.VisualStudio.Services
{
    public interface IVcProjectService
    {
        VCProject GetActiveProject();
        ConanProject ExtractConanProject(VCProject vcProject, ISettingsService settingsService);
        Task<ConanProject> ExtractConanProjectAsync(VCProject vcProject, ISettingsService settingsService);
        Task AddPropsImportAsync(string projectPath, string propFilePath);
        Guid UnloadProject(VCProject project);
        void ReloadProject(Guid projectGuid);
        bool IsConanProject(Project project);
        VCProject AsVCProject(Project project);
        string GetInstallationDirectory(ISettingsService settingsService, VCConfiguration configuration);
    }
}
