using System.ComponentModel.Design;
using Conan.VisualStudio.Services;
using Task = System.Threading.Tasks.Task;

namespace Conan.VisualStudio.Menu
{
    /// <summary>Command handler.</summary>
    internal sealed class AddConanDependsProject : MenuCommandBase
    {
        protected override int CommandId => PackageIds.AddConanDependsProjectId;

        private readonly IErrorListService _errorListService;
        private readonly IVcProjectService _vcProjectService;
        private readonly IConanService _conanService;

        public AddConanDependsProject(
            IMenuCommandService commandService,
            IErrorListService errorListService,
            IVcProjectService vcProjectService,
            IConanService conanService) : base(commandService, errorListService)
        {
            _errorListService = errorListService;
            _vcProjectService = vcProjectService;
            _conanService = conanService;
        }

        protected internal override async Task MenuItemCallbackAsync()
        {
            _errorListService.Clear();
            var vcProject = _vcProjectService.GetActiveProject();
            if (vcProject == null)
            {
                _errorListService.WriteError("A C++ project with a conan file must be selected.");
                return;
            }

            await _conanService.InstallAsync(vcProject);
            await _conanService.IntegrateAsync(vcProject);
        }
    }
}
