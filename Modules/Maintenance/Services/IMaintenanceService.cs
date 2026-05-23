// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceTaskEntity>> GetAllTasks();
        Task<MaintenanceTaskEntity?> GetTaskById(Guid id);
        Task CreateTask(MaintenanceTaskEntity task);
    }
}
