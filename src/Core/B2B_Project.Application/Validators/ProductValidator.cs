using B2B_Project.Application.Features.Product.Commands.CreateProduct;
using FluentValidation;

namespace B2B_Project.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            // Name alanı boş olamaz ve en az 3 karakter uzunluğunda olmalıdır
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır.");

            // Description alanı boş olabilir, ancak maksimum 500 karakter ile sınırlıdır
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olmalıdır.");

            // Price alanı boş olamaz ve 0'dan büyük bir değer olmalıdır
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Ürün fiyatı boş olamaz.")
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            // ProductCode alanı boş olamaz ve 5 ila 10 karakter arasında olmalıdır
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Ürün kodu boş olamaz.")
                .Length(5, 10).WithMessage("Ürün kodu 5 ile 10 karakter arasında olmalıdır.");

            // Stock alanı null olabilir, ancak pozitif bir sayı olmalıdır
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).When(x => x.Stock.HasValue).WithMessage("Stok miktarı 0 veya daha büyük olmalıdır.");

            // CategoryId boş olamaz
            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Kategori ID'si boş olamaz.");

            // Username boş olamaz
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");

            // En az bir ürün resmi yüklenmiş olmalıdır
            RuleFor(x => x.ProductImages)
                .NotNull().WithMessage("En az bir ürün resmi yüklenmiş olmalıdır.")
                .Must(images => images != null && images.Any()).WithMessage("En az bir ürün resmi yüklenmiş olmalıdır.");
        }
    }
}
