using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;
using Trainova.Api.Models;
using Trainova.Application.Common.Interfaces.MarkUps;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Api.Controllers;

[ApiController]
public class ApiController(
    CurrentUser? currentUser)
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




    protected IActionResult Success<T>(
    T value,
    DoneStatus status)
    {
        return status switch
        {

            DoneStatus.Created
                => StatusCode(201, CreateResponse(value, "Created successfully", 201)),

            DoneStatus.Done
                => Ok(CreateResponse(value, "Success", 200)),

            DoneStatus.Accepted
                => Accepted(CreateResponse(value, "Accepted", 202)),

            DoneStatus.Partial
            when value is IEnumerable<ITotalCountIncluded> countIncludeds
                => StatusCode(
                    206,
                    CreateResponse(
                        value,
                        "Partial content",
                        206,
                        countIncludeds.Count(),
                        countIncludeds.FirstOrDefault().TotalCount
                    )
                ),
            DoneStatus.Partial when value is IEnumerable enumerable
                => StatusCode(
                    206,
                    CreateResponse(
                        value,
                        "Partial content",
                        206,
                        enumerable.Cast<object>().Count()
                    )
                ),

            DoneStatus.NoContent
                => StatusCode(204),

            _ => Ok(CreateResponse(value, "Success", 200))
        };
    }

    private ApiResponse<T> CreateResponse<T>(
    T? data,
    string? message,
    int statusCode,
    int? count = null,
    int? totalCount = null)
    {
        return new ApiResponse<T>(
            Data: data,
            Message: message,
            StatusCode: statusCode,
            ResponseTime: DateTime.UtcNow,
            Count: count,
            TotalCount: totalCount,
            UserId: currentUser.Id
        );
    }


    protected IActionResult ErrorsPassed(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationError(errors);
        }

        return ErrorPassed(errors[0]);
    }

    protected IActionResult ErrorPassed(Error error)
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

    protected IActionResult ValidationError(List<Error> errors)
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
            errors => ErrorsPassed(errors)
        );
    }





}
