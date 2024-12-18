global using Carter;
global using MediatR;
global using FluentValidation;
global using FluentValidation.Results;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;
global using System.Reflection;

global using TodoApp.Application.Domain.Todo;
global using TodoApp.Application.Common.Behaviors;
global using TodoApp.Application.Common.Exceptions;
global using TodoApp.Application.Infrastructure.Persistence;
