using FluentValidation;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    // ─── Equipment ───────────────────────────────────────────────────────────────

    public class CreateEquipmentRequestValidator : AbstractValidator<CreateEquipmentRequest>
    {
        public CreateEquipmentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de l'équipement est obligatoire.")
                .MaximumLength(200).WithMessage("Le nom ne peut pas dépasser 200 caractères.");

            RuleFor(x => x.SerialNumber)
                .NotEmpty().WithMessage("Le numéro de série est obligatoire.")
                .MaximumLength(100).WithMessage("Le numéro de série ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("La marque est obligatoire.")
                .MaximumLength(100).WithMessage("La marque ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("L'emplacement est obligatoire.")
                .MaximumLength(200).WithMessage("L'emplacement ne peut pas dépasser 200 caractères.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Catégorie d'équipement invalide.");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty().WithMessage("La date d'achat est obligatoire.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La date d'achat ne peut pas être dans le futur.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Les notes ne peuvent pas dépasser 1000 caractères.")
                .When(x => x.Notes != null);
        }
    }

    // ─── Technician ──────────────────────────────────────────────────────────────

    public class CreateTechnicianRequestValidator : AbstractValidator<CreateTechnicianRequest>
    {
        public CreateTechnicianRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Le prénom est obligatoire.")
                .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Le nom est obligatoire.")
                .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est obligatoire.")
                .EmailAddress().WithMessage("Format d'email invalide.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Le téléphone est obligatoire.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$").WithMessage("Format de téléphone invalide.");

            RuleFor(x => x.Specialty)
                .IsInEnum().WithMessage("Spécialité invalide.");
        }
    }

    // ─── Corrective Task ─────────────────────────────────────────────────────────

    public class CreateCorrectiveTaskRequestValidator : AbstractValidator<CreateCorrectiveTaskRequest>
    {
        public CreateCorrectiveTaskRequestValidator()
        {
            RuleFor(x => x.EquipmentId)
                .NotEmpty().WithMessage("L'identifiant de l'équipement est obligatoire.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Le titre est obligatoire.")
                .MaximumLength(200).WithMessage("Le titre ne peut pas dépasser 200 caractères.");

            RuleFor(x => x.ProblemDescription)
                .NotEmpty().WithMessage("La description du problème est obligatoire.")
                .MinimumLength(10).WithMessage("La description doit contenir au moins 10 caractères.")
                .MaximumLength(2000).WithMessage("La description ne peut pas dépasser 2000 caractères.");

            RuleFor(x => x.Severity)
                .IsInEnum().WithMessage("Niveau de sévérité invalide.");
        }
    }

    // ─── Spare Part ──────────────────────────────────────────────────────────────

    public class CreateSparePartRequestValidator : AbstractValidator<CreateSparePartRequest>
    {
        public CreateSparePartRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de la pièce est obligatoire.")
                .MaximumLength(200).WithMessage("Le nom ne peut pas dépasser 200 caractères.");

            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("La référence est obligatoire.")
                .MaximumLength(100).WithMessage("La référence ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("La quantité ne peut pas être négative.");

            RuleFor(x => x.MinimumQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("La quantité minimale ne peut pas être négative.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Le prix unitaire doit être supérieur à 0.");

            RuleFor(x => x.Supplier)
                .MaximumLength(200).WithMessage("Le fournisseur ne peut pas dépasser 200 caractères.")
                .When(x => x.Supplier != null);
        }
    }
}
