using Microsoft.Extensions.DependencyInjection;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance
{
    public static class MaintenanceModule
    {
        public static IServiceCollection AddMaintenanceModule(this IServiceCollection services)
        {
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IPreventiveService, PreventiveService>();
            services.AddScoped<ICorrectiveService, CorrectiveService>();
            services.AddScoped<ITechnicianService, TechnicianService>();
            services.AddScoped<ISparePartService, SparePartService>();
            return services;
        }
    }
}