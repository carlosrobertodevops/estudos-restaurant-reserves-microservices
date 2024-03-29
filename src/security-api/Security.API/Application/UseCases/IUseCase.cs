﻿namespace Security.API.UseCases
{
    public interface IUseCase : IRequest
    {
        Guid CorrelationId { get; }
    }

    public interface IUseCase<T> : IRequest<T>
    {
        Guid CorrelationId { get; }
    }
}
