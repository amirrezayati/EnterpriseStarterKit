using Ardalis.Result;
using IdentityService.Application.Auth.DTOs;
using MediatR;

namespace IdentityService.Application.Auth.Commands;

public record RegisterUserCommand(RegisterRequest Request) : IRequest<Result<Guid>>;