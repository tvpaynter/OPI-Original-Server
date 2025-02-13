using FluentValidation;
using Utg.Api.Common.Constants;
using Utg.Api.Models.OPIModels;

namespace Utg.Api.Validators
{
    public sealed class RequestValidator : AbstractValidator<TransactionRequest>
    {
        public RequestValidator()
        {
            RuleFor(transRequest => transRequest.TransAmount).NotNull().When(tr => tr.TransType != OPITransactionType.Void);
            RuleFor(transRequest => transRequest.TransType).NotNull();
            RuleFor(transRequest => transRequest.TransDateTime).NotNull();
            RuleFor(transRequest => transRequest.SiteId).NotNull();
            RuleFor(transRequest => transRequest.SequenceNo).NotNull();
        }
    }
}
