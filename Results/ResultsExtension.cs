
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Results.Extension;

namespace Results;

public static class ResultsExtension
{
    public static ActionResult ToActionResult<T>(this IResults<T> result)
    {
        var message = result.Messages.Any() ? result.ToMultiLine(";") : null;
        var response = new HttpResponseMessage();

        if (result.Status == ResultsStatus.Success && result.Value is not null)
        {
            response.StatusCode = HttpStatusCode.OK;
            response.ReasonPhrase = message;
            return new OkObjectResult(result.Value);
        }

        if (result.Status == ResultsStatus.Created)
        {
            return new CreatedResult("", result.Value);
        }

        if (result.Status == ResultsStatus.Updated)
        {
            return new AcceptedResult();
        }

        if (result.Status == ResultsStatus.Deleted)
        {
            return new NoContentResult();
        }

        if (result.Status == ResultsStatus.NotFound)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            response.ReasonPhrase = message;

            return new NotFoundObjectResult(response);
        }

        if (result.Status == ResultsStatus.Failure)
        {
            return new ObjectResult(result.Messages.Flatten()){StatusCode = 500};
        }

        if (result.Status == ResultsStatus.BadRequest)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ReasonPhrase = message;
            return new ObjectResult(result.Value){StatusCode = (int)HttpStatusCode.BadRequest};
            // return new BadRequestObjectResult(response);
        }

        return new OkResult();
    }

    public static ActionResult ToActionResult(this IResults result)
    {
        var message = result.Messages.Any() ? result.ToMultiLine(";") : null;
        var response = new HttpResponseMessage();

        if (result.Status == ResultsStatus.Success)
        {
            if (result.Keys is not null || (result.Keys?.Count ?? 0) > 0)
            {
                return new ObjectResult(result.Keys);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.ReasonPhrase = message;
            return new ObjectResult(response);
        }

        if (result.Status == ResultsStatus.Created)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.Created};
        }

        if (result.Status == ResultsStatus.Updated)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.Accepted};
        }

        if (result.Status == ResultsStatus.Deleted)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.NoContent};
        }

        if (result.Status == ResultsStatus.NotFound)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            response.ReasonPhrase = message;

            return new NotFoundObjectResult(response);
        }

        if (result.Status == ResultsStatus.Failure)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ReasonPhrase = message;
            return new ObjectResult(result.Messages.Flatten()){StatusCode = 500};
            // return new InternalServerErrorObjectResult(response);
        }

        if (result.Status == ResultsStatus.BadRequest)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ReasonPhrase = message;
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.BadRequest};
            // return new BadRequestObjectResult(response);
        }

        return new OkResult();
    }

    public static ActionResult ToActionResult<T, T1>(this IResults<T> result, T1 value)
    {
        var message = result.Messages.Any() ? result.ToMultiLine(";") : null;
        var response = new HttpResponseMessage();

        if (result.Status == ResultsStatus.Success && value is not null)
        {
            response.StatusCode = HttpStatusCode.OK;
            response.ReasonPhrase = message;
            return new OkObjectResult(value);
        }

        if (result.Status == ResultsStatus.Created)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.Created};
        }

        if (result.Status == ResultsStatus.Updated)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.Accepted};
        }

        if (result.Status == ResultsStatus.Deleted)
        {
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.NoContent};
        }

        if (result.Status == ResultsStatus.NotFound)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            response.ReasonPhrase = message;

            return new NotFoundObjectResult(response);
        }

        if (result.Status == ResultsStatus.Failure)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ReasonPhrase = message;
            return new ObjectResult(result.Messages.Flatten()){StatusCode = 500};

            // return new InternalServerErrorObjectResult(response);
        }

        if (result.Status == ResultsStatus.BadRequest)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ReasonPhrase = message;
            return new ObjectResult(null){StatusCode = (int)HttpStatusCode.BadRequest};
            // return new BadRequestObjectResult(response);
        }

        return new OkResult();
    }

    private static bool ContainsNewLineCharacter(this string value)
    {
        foreach (var character in value)
        {
            if (character == (char) 13 || character == (char) 10)
            {
                return true;
            }
        }

        return false;
    }
}
