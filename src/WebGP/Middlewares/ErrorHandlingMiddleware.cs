﻿using FluentValidation;

namespace WebGP.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}