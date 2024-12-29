using Results.Formats;

namespace Results;

[Serializable]
public class ValueResults<TValue> : IResults<TValue>
{
    internal ValueResults(ResultsStatus status)
    {
        Status = status;
        Messages = new List<string>();
        Value = default!;
        Keys = new Dictionary<string, object>();
    }

    internal ValueResults(IResults<TValue> results)
    {
        // ProblemDetails = problemDetails;
        Status = results.Status;
        Messages = results.Messages;
        Value = results.Value;
        Keys = results.Keys;
    }

    internal ValueResults(IResults results)
    {
        Status = results.Status;
        Messages = results.Messages;
        Value = default!;
        Keys = results.Keys;
    }

    public bool Success => Status == ResultsStatus.Success;
    public bool NotFound => Status == ResultsStatus.NotFound;
    public bool BadRequest => Status == ResultsStatus.BadRequest;
    public bool Failure => Status == ResultsStatus.Failure;
    public bool Created => Status == ResultsStatus.Created;
    public bool Updated => Status == ResultsStatus.Updated;
    public bool Deleted => Status == ResultsStatus.Deleted;

    public List<string> Messages { get; protected set; }
    public ResultsStatus Status { get; set; }
    public TValue Value { get; set; }

    public Dictionary<string, object> Keys { get; }

    public override string ToString()
    {
        return ToMultiLine(string.Empty);
    }

    public string ToMultiLine(string delimiter = "")
    {
        return MultiLineFormatter.ToMultiLine(delimiter, Messages);
    }
}
