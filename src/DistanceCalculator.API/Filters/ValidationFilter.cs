﻿using System.Net;
using System.Reflection;
using FluentValidation;

namespace DistanceCalculator.API.Filters;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{
}

public static class ValidationFilter
{
    public static EndpointFilterDelegate ValidationFilterFactory(
        EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        var validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices).ToList();

        return validationDescriptors.Count != 0
            ? invocationContext => Validate(validationDescriptors, invocationContext, next)
            : next;
    }

    private static async ValueTask<object?> Validate(
        IEnumerable<ValidationDescriptor> validationDescriptors,
        EndpointFilterInvocationContext invocationContext,
        EndpointFilterDelegate next)
    {
        foreach (var descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];

            if (argument is null) continue;

            var validationResult = await descriptor.Validator.ValidateAsync(
                new ValidationContext<object>(argument)
            );

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
            }
        }

        return await next.Invoke(invocationContext);
    }

    private static IEnumerable<ValidationDescriptor> GetValidators(
        MethodInfo methodInfo,
        IServiceProvider serviceProvider)
    {
        var parameters = methodInfo.GetParameters();

        for (var i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];

            if (parameter.GetCustomAttribute<ValidateAttribute>() is null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);

            if (serviceProvider.GetService(validatorType) is IValidator validator)
            {
                yield return new ValidationDescriptor
                {
                    ArgumentIndex = i,
                    ArgumentType = parameter.ParameterType,
                    Validator = validator
                };
            }
        }
    }

    private class ValidationDescriptor
    {
        public required int ArgumentIndex { get; init; }
        public required Type ArgumentType { get; init; }
        public required IValidator Validator { get; init; }
    }
}