namespace Results;

public interface IResults<T> : IResults
{
    T Value { get; }
}

public interface IResults
{
    ResultsStatus Status { get; }
    List<string> Messages { get; }
    Dictionary<string, object> Keys { get; }
    string ToMultiLine(string delimiter = "");
    string ToString();
}
