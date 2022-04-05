using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RofoServer.Core.Utils;
using RofoServer.Core.Utils.Emailer;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IRepositories;
using RofoServer.Persistence;
using System;
using System.Text;
using RofoServer.Core.Group.AddToGroup;
using RofoServer.Core.Group.CreateGroup;
using RofoServer.Core.Group.JoinGroup;
using RofoServer.Core.Group.ViewGroups;
using RofoServer.Core.User.AccountConfirmation;
using RofoServer.Core.User.Authentication;
using RofoServer.Core.User.RefreshTokenLogic;
using RofoServer.Core.User.Register;
using RofoServer.Core.User.RevokeToken;
using RofoServer.Core.User.ValidateAccount;

namespace RofoServer.Utils.Extensions;

public static class RofoServiceExtensions
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration config)
        => services
            .AddDbContext<RofoDbContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(config.GetConnectionString("RofoDb")));



    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config) {
        services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(config["Rofos:ApiKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<ITokenGenerator, BasicTokenGenerator>()
            .AddScoped<IJwtServices, JwtServices>()
            .AddScoped<IRepositoryManager, RepositoryManager>()
            .AddTransient<IEmailer, SendGridEmailer>()

            .AddScoped(typeof(AccountConfirmationEmailRequestModel))
            .AddScoped(typeof(AuthenticateRequestModel))
            .AddScoped(typeof(ValidateAccountRequestModel))
            .AddScoped(typeof(RefreshTokenRequestModel))
            .AddScoped(typeof(RegisterRequestModel))
            .AddScoped(typeof(RevokeRefreshTokenRequestModel))
            .AddScoped(typeof(CreateGroupRequestModel))
            .AddScoped(typeof(GetAllGroupsRequestModel))
            .AddScoped(typeof(JoinGroupRequestModel))
            .AddScoped(typeof(InviteToGroupRequestModel))

            .AddScoped(typeof(AccountConfirmationEmailCommand))
            .AddScoped(typeof(AuthenticationCommand))
            .AddScoped(typeof(ValidateAccountCommand))
            .AddScoped(typeof(RefreshTokenCommand))
            .AddScoped(typeof(RegisterCommand))
            .AddScoped(typeof(RevokeRefreshTokenCommand))
            .AddScoped(typeof(CreateGroupCommand))
            .AddScoped(typeof(GetGroupsCommand))
            .AddScoped(typeof(JoinGroupCommand))
            .AddScoped(typeof(InviteToGroupCommand))

            .AddScoped(typeof(IRequestHandler<AccountConfirmationEmailCommand, AccountConfirmationEmailResponseModel>), typeof(AccountConfirmationEmailHandler))
            .AddScoped(typeof(IRequestHandler<AuthenticationCommand, AuthenticateResponseModel>), typeof(AuthenticateHandler))
            .AddScoped(typeof(IRequestHandler<ValidateAccountCommand, ValidateAccountResponseModel>), typeof(ValidateAccountHandler))
            .AddScoped(typeof(IRequestHandler<RefreshTokenCommand, RefreshTokenResponseModel>), typeof(RefreshTokenHandler))
            .AddScoped(typeof(IRequestHandler<RegisterCommand, RegisterResponseModel>), typeof(RegisterHandler))
            .AddScoped(typeof(IRequestHandler<RevokeRefreshTokenCommand, RevokeRefreshTokenResponseModel>), typeof(RevokeRefreshTokenHandler))
            .AddScoped(typeof(IRequestHandler<CreateGroupCommand, CreateGroupResponseModel>), typeof(CreateGroupHandler))
            .AddScoped(typeof(IRequestHandler<GetGroupsCommand, GetAllGroupResponseModel>), typeof(GetAllGroupsHandler))
            .AddScoped(typeof(IRequestHandler<JoinGroupCommand, JoinGroupResponseModel>), typeof(JoinGroupHandler))
            .AddScoped(typeof(IRequestHandler<InviteToGroupCommand, InviteToGroupResponseModel>), typeof(InviteToGroupHandler))
            .AddMediatR(AppDomain.CurrentDomain.Load("RofoServer.Core"));



    public static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(c => {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Rofo API",
                    Version = "v1"
                });
        });

    public static void AddApiControllers(this IServiceCollection services)
        => services
            .AddControllers(
                options => options
                    .Filters
                    .Add<ModelOrNotFoundActionFilter>()
            );
}