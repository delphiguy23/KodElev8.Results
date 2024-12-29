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

    public static IResults<T> ToResults<T>(this T value)
    {
        return ResultsTo.Something<T>(value);
    }

    public static IResults<T> Is<T>(this IResults<T> results, Func<T, bool> predicate)
    {
        if (!results.IsSuccess())
        {
            return results;
        }

        if (results.Value is null)
        {
            return ResultsTo.Failure<T>("Value is null");
        }

        if(!predicate(results.Value))
        {
            return ResultsTo.Failure<T>("Predicate results to false");
        }

        return results;
    }

    public static IResults<T2> Is<T, T2>(this IResults<T> results, Func<T, bool> condition, Func<IResults<T>, IResults<T2>> then)
    {
        if (condition(results.Value))
        {
            return (IResults<T2>)then(results);
        }

        return (IResults<T2>)false.ToResults();
    }

    public static IResults<T2> Is<T, T2>(this IResults<T> results, Func<T, bool> condition, Func<IResults<T>, IResults<T2>> then, Func<IResults<T>, IResults<T2>> @else)
    {
        if (!results.IsSuccess())
        {
            return ResultsTo.Failure<T2>().FromResults(results);
        }

        if (condition(results.Value))
        {
            return (IResults<T2>)then(results);
        }

        return (IResults<T2>) @else(results);
    }

    public static IResults<T> Validate<T>(this IResults<T> results, Func<T, bool> predicate, string message)
    {
        if (!results.IsSuccess())
        {
            return results;
        }

        if (predicate(results.Value))
        {
            return ResultsTo.Failure<T>().WithMessage(message);
        }

        return ResultsTo.Success(results.Value);
    }

    public static IResults<T> Validate<T>(this T results, Func<T, bool> predicate, string message)
    {
        if (predicate(results))
        {
            return ResultsTo.Failure<T>().WithMessage(message);
        }

        return ResultsTo.Success(results);
    }

    public static IResults<T1> Then<T, T1>(this IResults<T> results, Func<T, T1> predicate)
    {
        if (!results.IsSuccess())
        {
            return ResultsTo.Failure<T1>().FromResults(results);
        }

        var result = predicate(results.Value);

        return ResultsTo.Something<T1>(result);
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
