namespace Results.Extension;

public static class ResultsExtension
{
    public static bool IsSuccess(this IResults result) =>
        result is { Status: ResultsStatus.Success };

    public static bool IsCreated(this IResults result) =>
        result is { Status: ResultsStatus.Created };

    public static bool IsUpdated(this IResults result) =>
        result is { Status: ResultsStatus.Updated };

    public static bool IsDeleted(this IResults result) =>
        result is { Status: ResultsStatus.Deleted };

    public static bool IsNotFound(this IResults result) =>
        result is { Status: ResultsStatus.NotFound };

    public static bool IsFailure(this IResults result) =>
        result is { Status: ResultsStatus.Failure };

    public static bool IsBadRequest(this IResults result) =>
        result is { Status: ResultsStatus.BadRequest };

    public static bool IsNotFoundOrBadRequest(this IResults result) =>
        result is { Status: ResultsStatus.BadRequest or ResultsStatus.NotFound or ResultsStatus.Failure };

    public static IResults OnNotFound(this IResults result, Func<IResults> func) =>
        result is { Status: ResultsStatus.NotFound } ? func() : result;

    public static IResults<T> ToResults<T>(this T value) =>
        ResultsTo.Something<T>(value);

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

        return !predicate(results.Value) ? ResultsTo.Failure<T>("Predicate results to false") : results;
    }

    public static IResults<T2> Is<T1, T2>(this IResults<T1> results, Func<T1, bool> condition, Func<IResults<T1>, IResults<T2>> then)
    {
        if (condition(results.Value))
        {
            return (IResults<T2>)then(results);
        }

        return (IResults<T2>)false.ToResults();
    }

    public static IResults<T2> Is<T1, T2>(this IResults<T1> results, Func<T1, bool> condition, Func<IResults<T1>, IResults<T2>> then, Func<IResults<T1>, IResults<T2>> @else)
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

        return predicate(results.Value) ?
            ResultsTo.Failure<T>().WithMessage(message) :
            ResultsTo.Success(results.Value);
    }

    public static IResults<T> Validate<T>(this T results, Func<T, bool> predicate, string message)
    {
        return predicate(results) ?
            ResultsTo.Failure<T>().WithMessage(message) :
            ResultsTo.Success(results);
    }

    public static IResults<T2> Then<T1, T2>(this IResults<T1> results, Func<T1, T2> predicate)
    {
        if (!results.IsSuccess())
        {
            return ResultsTo.Failure<T2>().FromResults(results);
        }

        var result = predicate(results.Value);

        return ResultsTo.Something<T2>(result);
    }
}
