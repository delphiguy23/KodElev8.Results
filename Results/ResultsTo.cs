using Results.Builder;

namespace Results;

public static class ResultsTo
{
    //Success
    public static ResultsStatusBuilder<object> Success()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Success);
    }

    public static ResultsStatusBuilder<TValue> Success<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Success);
    }

    public static ResultsStatusBuilder<object> Success(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Success).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Success<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Success).WithValue(value);
    }

    public static ResultsStatusBuilder<object> Created()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Created);
    }

    public static ResultsStatusBuilder<TValue> Created<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Created);
    }

    public static ResultsStatusBuilder<object> Created(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Created).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Created<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Created).WithValue(value);
    }

    public static ResultsStatusBuilder<object> Deleted()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Deleted);
    }

    public static ResultsStatusBuilder<TValue> Deleted<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Deleted);
    }

    public static ResultsStatusBuilder<object> Deleted(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Deleted).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Deleted<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Deleted).WithValue(value);
    }

    public static ResultsStatusBuilder<object> Updated()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Deleted);
    }

    public static ResultsStatusBuilder<TValue> Updated<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Deleted);
    }

    public static ResultsStatusBuilder<object> Updated(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Updated).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Updated<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Updated).WithValue(value);
    }

    //Not Found
    public static IResults NotFound()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.NotFound);
    }

    public static ResultsStatusBuilder<TValue> NotFound<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.NotFound);
    }

    public static ResultsStatusBuilder<TValue> NotFound<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.NotFound).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> NotFound<TValue>(string value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.NotFound).WithMessage(value);
    }

    //Not UnAuthorized
    public static ResultsStatusBuilder<object> UnAuthorized()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.UnAuthorized);
    }

    public static ResultsStatusBuilder<TValue> UnAuthorized<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.UnAuthorized);
    }

    public static ResultsStatusBuilder<TValue> UnAuthorized<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.UnAuthorized).WithValue(value);
    }

    public static ResultsStatusBuilder<object> UnAuthorized(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.UnAuthorized).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> UnAuthorized<TValue>(string value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.UnAuthorized).WithMessage(value);
    }

    //Failure
    public static ResultsStatusBuilder<object> Failure()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Failure);
    }

    public static ResultsStatusBuilder<TValue> Failure<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Failure);
    }

    public static ResultsStatusBuilder<object> Failure(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.Failure).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Failure<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Failure).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Failure<TValue>(string value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.Failure).WithMessage(value);
    }

    //BadRequest
    public static ResultsStatusBuilder<object> BadRequest()
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.BadRequest);
    }

    public static ResultsStatusBuilder<TValue> BadRequest<TValue>()
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.BadRequest);
    }

    public static ResultsStatusBuilder<object> BadRequest(object value)
    {
        return new ResultsStatusBuilder<object>(ResultsStatus.BadRequest).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> BadRequest<TValue>(TValue value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.BadRequest).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> BadRequest<TValue>(string value)
    {
        return new ResultsStatusBuilder<TValue>(ResultsStatus.BadRequest).WithMessage(value);
    }

    ////

    public static ResultsStatusBuilder<TValue> Something<TValue>(TValue? value)
    {
        return value == null ? new ResultsStatusBuilder<TValue>(ResultsStatus.NotFound) 
                            : new ResultsStatusBuilder<TValue>(ResultsStatus.Success).WithValue(value);
    }

    public static ResultsStatusBuilder<TValue> Something<TValue>(IResults<TValue> value)
    {
        return value.Status switch
        {
            ResultsStatus.Success => new ResultsStatusBuilder<TValue>(ResultsStatus.Success).FromResults(value),
            ResultsStatus.Created => new ResultsStatusBuilder<TValue>(ResultsStatus.Created).FromResults(value),
            ResultsStatus.Updated => new ResultsStatusBuilder<TValue>(ResultsStatus.Updated).FromResults(value),
            ResultsStatus.Deleted => new ResultsStatusBuilder<TValue>(ResultsStatus.Deleted).FromResults(value),
            ResultsStatus.NotFound => new ResultsStatusBuilder<TValue>(ResultsStatus.NotFound).FromResults(value),
            ResultsStatus.BadRequest => new ResultsStatusBuilder<TValue>(ResultsStatus.BadRequest).FromResults(value),
            ResultsStatus.Failure => new ResultsStatusBuilder<TValue>(ResultsStatus.Failure).FromResults(value),
            _ => new ResultsStatusBuilder<TValue>(ResultsStatus.Success).FromResults(value)
        };
    }
}
