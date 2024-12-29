namespace Results.Extension;

public static class ResultsExtension
{
    public static bool IsSuccess(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.Success;
    }

    public static bool IsCreated(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.Created;
    }

    public static bool IsUpdated(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.Updated;
    }

    public static bool IsDeleted(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.Deleted;
    }

    public static bool IsNotFound(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.NotFound;
    }

    public static bool IsFailure(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.Failure;
    }

    public static bool IsBadRequest(this IResults result)
    {
        return result is { } fluentResults && fluentResults.Status == ResultsStatus.BadRequest;
    }

    public static bool IsNotFoundOrBadRequest(this IResults result)
    {
        return result is { } fluentResults && (fluentResults.Status == ResultsStatus.BadRequest || fluentResults.Status == ResultsStatus.NotFound || fluentResults.Status == ResultsStatus.Failure);
    }

    public static IResults OnNotFound(this IResults result, Func<IResults> func)
    {
        if (result is { } fluentResults && fluentResults.Status == ResultsStatus.NotFound)
        {
            return func();
        }

        return result;
    }

    // public static IFluentResults OnFailure(this IFluentResults result, Func<IFluentResults> func)
    // {
    //     if (result is { } fluentResults && fluentResults.Status == FluentResultsStatus.Failure)
    //     {
    //         return func();
    //     }
    //
    //     return result;
    // }
    //
    // public static IFluentResults OnFailure(this IFluentResults result, Action func)
    // {
    //     if (result is { } fluentResults && fluentResults.Status == FluentResultsStatus.Failure)
    //     {
    //         func();
    //     }
    //
    //     return result;
    // }
    //
    // public static IFluentResults OnSuccess(this IFluentResults result, Func<IFluentResults> func)
    // {
    //     if (result is { } fluentResults && fluentResults.Status == FluentResultsStatus.Success)
    //     {
    //         return func();
    //     }
    //
    //     return result;
    // }
}
