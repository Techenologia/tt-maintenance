// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using FluentAssertions;
using TT.Backend.Modules.Maintenance.Entities;
using Xunit;

namespace TT.Backend.Tests.Business;

public class MaintenanceEntityTests
{
    // --- EquipmentEntity ---

    [Fact]
    public void Equipment_DefaultStatus_IsOperational()
    {
        var equipment = new EquipmentEntity
        {
            Name         = "Compresseur A1",
            SerialNumber = "SN-001",
            Brand        = "Atlas",
            Location     = "Atelier 1",
            Category     = EquipmentCategory.Machine
        };

        equipment.Status.Should().Be(EquipmentStatus.Operational);
        equipment.Id.Should().NotBeEmpty();
        equipment.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Equipment_SetUnderMaintenance_StatusChanges()
    {
        var equipment = new EquipmentEntity { Name = "Pompe B2" };

        equipment.Status = EquipmentStatus.UnderMaintenance;

        equipment.Status.Should().Be(EquipmentStatus.UnderMaintenance);
    }

    [Fact]
    public void Equipment_AllCategories_AreValid()
    {
        var categories = Enum.GetValues<EquipmentCategory>();
        categories.Should().HaveCount(6);
        categories.Should().Contain(EquipmentCategory.Machine);
        categories.Should().Contain(EquipmentCategory.Vehicle);
        categories.Should().Contain(EquipmentCategory.HVAC);
    }

    // --- TechnicianEntity ---

    [Fact]
    public void Technician_DefaultStatus_IsAvailable()
    {
        var tech = new TechnicianEntity
        {
            FirstName = "Amadou",
            LastName  = "Diallo",
            Email     = "amadou@tt.com",
            Phone     = "+22700000000",
            Specialty = TechnicianSpecialty.Electrical
        };

        tech.Status.Should().Be(TechnicianStatus.Available);
        tech.Id.Should().NotBeEmpty();
        tech.HiredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Technician_SetBusy_StatusChanges()
    {
        var tech = new TechnicianEntity { FirstName = "Moussa" };

        tech.Status = TechnicianStatus.Busy;

        tech.Status.Should().Be(TechnicianStatus.Busy);
    }

    [Fact]
    public void Technician_AllSpecialties_AreValid()
    {
        var specialties = Enum.GetValues<TechnicianSpecialty>();
        specialties.Should().HaveCount(6);
        specialties.Should().Contain(TechnicianSpecialty.Electrical);
        specialties.Should().Contain(TechnicianSpecialty.IT);
    }

    // --- TransactionEntity ---

    [Fact]
    public void Transaction_DefaultCreatedAt_IsUtcNow()
    {
        var tx = new TT.Backend.Modules.Fintech.Wallet.Entities.TransactionEntity
        {
            From   = "W001",
            To     = "W002",
            Amount = 500m,
            Fees   = 5m
        };

        tx.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        tx.Amount.Should().Be(500m);
        tx.Fees.Should().Be(5m);
    }
}

