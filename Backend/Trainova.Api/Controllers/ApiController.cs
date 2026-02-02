using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Api.Controllers;

[ApiController]
public class ApiController(
    LinkGenerator _linkGenerator)
    : ControllerBase
{

    protected IActionResult Success(DoneStatus status)
    {
        return status switch
        {
            DoneStatus.Done => Ok(),
            DoneStatus.Created => Created(string.Empty, null),
            DoneStatus.Accepted => Accepted(),
            DoneStatus.Partial => StatusCode(StatusCodes.Status206PartialContent),
            DoneStatus.NoContent => NoContent(),
            _ => Ok()
        };
    }

    protected IActionResult CreatedWithGetById<T>(
        string actionName,
        object routeValues,
        T value)
    {
        var url = _linkGenerator.GetPathByAction(
            HttpContext,
            actionName,
            values: routeValues
        );

        return Created(url!, value);
    }

    protected IActionResult Success<T>(
        T value,
        DoneStatus status,
        object? routeValues = null,
        string getMethod = "GetById")
    {
        return status switch
        {
            DoneStatus.Created when routeValues is not null
                => CreatedWithGetById(getMethod, routeValues, value),
            DoneStatus.Created => StatusCode(201, value),
            DoneStatus.Done => Ok(value),
            DoneStatus.Accepted => Accepted(value),
            DoneStatus.Partial => StatusCode(206, value),
            DoneStatus.NoContent => NoContent(),

            _ => Ok(value)
        };
    }


    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: error.Description);
    }

    protected IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

    protected IActionResult MapResult<T>(ResultOf<T> result)
    {
        return result.Match(
            (value, status) => Success(value, status),
            errors => Problem(errors)
        );
    }




}
