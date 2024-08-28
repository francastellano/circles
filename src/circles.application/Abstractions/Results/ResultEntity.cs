using circles.domain.Abstractions;

namespace circles.application.Abstractions.Results;


public static class ResultEntity
{

    public static Result NotFound() => Result.Failure(new Error("100", "Not Found"));
    public static Result NonControledError() => Result.Failure(new Error("500", "Non Controled Error"));
    public static Result AlreadyCanceled() => Result.Failure(new Error("504", "Already Canceled"));
    public static Result CodeDuplicated() => Result.Failure(new Error("505", "Code Duplicated"));
}


public static class ResultEntity<TEntity>
{
    public static Result<TEntity> NotFound() => Result<TEntity>.Failure<TEntity>(new Error("100", "The record has not been found"));
    public static Result<TEntity> NonControledError() => Result.Failure<TEntity>(new Error("500", "Non Controlled Error"));
}