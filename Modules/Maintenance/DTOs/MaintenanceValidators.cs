// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using FluentValidation;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    // â”€â”€â”€ Equipment â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

    public class CreateEquipmentRequestValidator : AbstractValidator<CreateEquipmentRequest>
    {
        public CreateEquipmentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de l'Ã©quipement est obligatoire.")
                .MaximumLength(200).WithMessage("Le nom ne peut pas dÃ©passer 200 caractÃ¨res.");

            RuleFor(x => x.SerialNumber)
                .NotEmpty().WithMessage("Le numÃ©ro de sÃ©rie est obligatoire.")
                .MaximumLength(100).WithMessage("Le numÃ©ro de sÃ©rie ne peut pas dÃ©passer 100 caractÃ¨res.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("La marque est obligatoire.")
                .MaximumLength(100).WithMessage("La marque ne peut pas dÃ©passer 100 caractÃ¨res.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("L'emplacement est obligatoire.")
                .MaximumLength(200).WithMessage("L'emplacement ne peut pas dÃ©passer 200 caractÃ¨res.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("CatÃ©gorie d'Ã©quipement invalide.");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty().WithMessage("La date d'achat est obligatoire.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La date d'achat ne peut pas Ãªtre dans le futur.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Les notes ne peuvent pas dÃ©passer 1000 caractÃ¨res.")
                .When(x => x.Notes != null);
        }
    }

    // â”€â”€â”€ Technician â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

    public class CreateTechnicianRequestValidator : AbstractValidator<CreateTechnicianRequest>
    {
        public CreateTechnicianRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Le prÃ©nom est obligatoire.")
                .MaximumLength(100).WithMessage("Le prÃ©nom ne peut pas dÃ©passer 100 caractÃ¨res.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Le nom est obligatoire.")
                .MaximumLength(100).WithMessage("Le nom ne peut pas dÃ©passer 100 caractÃ¨res.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est obligatoire.")
                .EmailAddress().WithMessage("Format d'email invalide.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Le tÃ©lÃ©phone est obligatoire.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$").WithMessage("Format de tÃ©lÃ©phone invalide.");

            RuleFor(x => x.Specialty)
                .IsInEnum().WithMessage("SpÃ©cialitÃ© invalide.");
        }
    }

    // â”€â”€â”€ Corrective Task â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

    public class CreateCorrectiveTaskRequestValidator : AbstractValidator<CreateCorrectiveTaskRequest>
    {
        public CreateCorrectiveTaskRequestValidator()
        {
            RuleFor(x => x.EquipmentId)
                .NotEmpty().WithMessage("L'identifiant de l'Ã©quipement est obligatoire.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Le titre est obligatoire.")
                .MaximumLength(200).WithMessage("Le titre ne peut pas dÃ©passer 200 caractÃ¨res.");

            RuleFor(x => x.ProblemDescription)
                .NotEmpty().WithMessage("La description du problÃ¨me est obligatoire.")
                .MinimumLength(10).WithMessage("La description doit contenir au moins 10 caractÃ¨res.")
                .MaximumLength(2000).WithMessage("La description ne peut pas dÃ©passer 2000 caractÃ¨res.");

            RuleFor(x => x.Severity)
                .IsInEnum().WithMessage("Niveau de sÃ©vÃ©ritÃ© invalide.");
        }
    }

    // â”€â”€â”€ Spare Part â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

    public class CreateSparePartRequestValidator : AbstractValidator<CreateSparePartRequest>
    {
        public CreateSparePartRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de la piÃ¨ce est obligatoire.")
                .MaximumLength(200).WithMessage("Le nom ne peut pas dÃ©passer 200 caractÃ¨res.");

            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("La rÃ©fÃ©rence est obligatoire.")
                .MaximumLength(100).WithMessage("La rÃ©fÃ©rence ne peut pas dÃ©passer 100 caractÃ¨res.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("La quantitÃ© ne peut pas Ãªtre nÃ©gative.");

            RuleFor(x => x.MinimumQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("La quantitÃ© minimale ne peut pas Ãªtre nÃ©gative.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Le prix unitaire doit Ãªtre supÃ©rieur Ã  0.");

            RuleFor(x => x.Supplier)
                .MaximumLength(200).WithMessage("Le fournisseur ne peut pas dÃ©passer 200 caractÃ¨res.")
                .When(x => x.Supplier != null);
        }
    }
}

