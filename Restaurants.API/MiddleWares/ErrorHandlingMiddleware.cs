﻿using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.API.MiddleWares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogWarning(notFound.Message);
            }
            catch (BadRequestException badException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badException.Message);

                logger.LogWarning(badException.Message);
            }
            catch (NotFoundNameException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogWarning(notFound.Message);
            }
            catch (NotFoundEmailException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogWarning(notFound.Message);
            }
            catch (NotFoundPhoneNumberException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogWarning(notFound.Message);
            }
            catch (DuplicateNameException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);

                logger.LogWarning(ex.Message);
            }
            catch (ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);

            //    context.Response.StatusCode = 500;
            //    await context.Response.WriteAsync("Something went wrong");
            //}
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;

                // اطبع الرسالة الحقيقية للخطأ مؤقتًا
                await context.Response.WriteAsync($"Error: {ex.Message}\n\n{ex.StackTrace}");
            }

        }
    }
}
