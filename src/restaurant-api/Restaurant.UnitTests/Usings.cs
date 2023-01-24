//libs
global using Xunit;
global using Bogus;
global using MediatR;
global using NSubstitute;
global using FluentAssertions;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using NSubstitute.ExceptionExtensions;
global using enzotlucas.DevKit.Core.Exceptions;

//tests
global using Restaurant.UnitTests.Fixtures.Application.ViewModels;
global using Restaurant.UnitTests.Fixtures.API.Controllers;
global using Restaurant.UnitTests.Fixtures.API;

//api
global using Restaurant.API.Features.V1.Controllers;

//application
global using Restaurant.Application.ViewModels;
global using Restaurant.Application.Queries.GetRestaurantById;
global using Restaurant.Application.Commands.CreateRestaurant;

//core

//infrastructure
