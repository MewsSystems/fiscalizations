namespace Mews.Fiscalizations.Fiskaly.Models;

public class ResponseResult<TResult>(TResult successResult = null, ErrorResult errorResult = null)
    where TResult : class
{
    public TResult SuccessResult { get; } = successResult;

    public ErrorResult ErrorResult { get; } = errorResult;

    public bool IsSuccess => ErrorResult is null;
}