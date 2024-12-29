namespace Results.Problem;

public interface IProblemDetails
{
    object Details { get; }
}

public interface IProblemDetails<TProblem>
{
    TProblem Details { get; }
}
