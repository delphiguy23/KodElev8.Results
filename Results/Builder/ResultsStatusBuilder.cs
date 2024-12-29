namespace Results.Builder;

public class ResultsStatusBuilder<T> : ValueResults<T>
{
    public ResultsStatusBuilder(ResultsStatus status) : base(status)
    {
    }

    public ResultsStatusBuilder<T> PrependMessage(string message)
    {
        Messages.Insert(0, message);
        return this;
    }

    public ResultsStatusBuilder<T> WithMessageFormat(string message, params object[] paramList)
    {
        message = string.Format(message, paramList);

        Messages.Add(message);
        return this;
    }

    public ResultsStatusBuilder<T> WithMessage(string message)
    {
        Messages.Add(message);
        return this;
    }

    public ResultsStatusBuilder<T> WithMessage(IEnumerable<string> messages)
    {
        if (!messages.Any())
        {
            return this;
        }

        Messages.AddRange(messages);
        return this;
    }

    public ResultsStatusBuilder<T> WithMessagesFrom(IResults results)
    {
        WithMessage(results.Messages);
        return this;
    }

    public ResultsStatusBuilder<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    public ResultsStatusBuilder<T> WithKeysFrom(IResults results)
    {
        foreach (var key in results.Keys.Keys)
        {
            Keys[key] = results.Keys[key];
        }

        return this;
    }

    public ResultsStatusBuilder<T> WithKey(string key, object value)
    {
        Keys[key] = value;
        return this;
    }

    public ResultsStatusBuilder<T> FromResults(IResults results)
    {
        WithMessage(results.Messages);
        WithKeysFrom(results);

        if (!results.GetType().IsGenericType) return this;
        
        var value = results.GetType()
            .GetProperty("Value")
            ?.GetValue(results, null);

        if (value is T value1)
        {
            WithValue(value1);
        }

        return this;
    }

    public ResultsStatusBuilder<T> FromException(Exception exception)
    {
        Messages.Add(exception.Message);
        Messages.Add(exception.ToString());
        return this;
    }
    
    public ResultsStatusBuilder<T> GetValueOrDefault(T value)
    {
        if (typeof(T) == typeof(string))
            Value = (T)Convert.ChangeType(string.Empty, typeof(T));

        if (Value is not null)
        {
            return this;
        }

        Value = default!;

        return this;
    }
}
