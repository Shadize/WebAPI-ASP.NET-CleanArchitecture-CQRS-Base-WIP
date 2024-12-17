using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace WebAPI.Application.Behaviors
{

    /*
     Pas sur de la propreté de l'utilisation de : 

                    var responseType = typeof(TResponse);
                    var value = responseType.GetProperty("Value");


     */
    public class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();

            // Request Logging
            // Serialize the request
            var requestJson = JsonSerializer.Serialize(request);
            // Log the serialized request
            logger.LogInformation("Handling request {CorrelationID}: {Request}", correlationId, requestJson);

            // Response logging
            var response = await next();

            if(response != null && response is Result)
            {
                var responseType = typeof(TResponse);
                var value = responseType.GetProperty("Value");
                // Serialize the request
                var responseJson = JsonSerializer.Serialize(value);
                // Log the serialized request
                logger.LogInformation("Response for {Correlation}: {Response}", correlationId, responseJson);
            }


            // Return response
            return response;
        }
    }
}
