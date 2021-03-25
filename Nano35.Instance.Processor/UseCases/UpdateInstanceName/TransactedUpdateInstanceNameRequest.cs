﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceName
{
    public class TransactedUpdateInstanceNameRequest :
        PipeNodeBase<
            IUpdateInstanceNameRequestContract,
            IUpdateInstanceNameResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateInstanceNameRequest(
            ApplicationContext context,
            IPipeNode<IUpdateInstanceNameRequestContract,
                IUpdateInstanceNameResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input,
            CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw new Exception("Транзакция отменена", ex);
            }
        }
    }
}