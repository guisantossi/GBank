using GBank.Banking.Application.Interfaces;
using GBank.Banking.Application.Services;
using GBank.Banking.Data.Context;
using GBank.Banking.Data.Repository;
using GBank.Banking.Domain.CommandHandlers;
using GBank.Banking.Domain.Commands;
using GBank.Banking.Domain.Interfaces;
using GBank.Domain.Core.Bus;
using GBank.Infra.Bus;
using GBank.Transfer.Application.Interfaces;
using GBank.Transfer.Application.Services;
using GBank.Transfer.Data.Context;
using GBank.Transfer.Data.Repository;
using GBank.Transfer.Domain.EventHandlers;
using GBank.Transfer.Domain.Events;
using GBank.Transfer.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Infra.IoC
{
    public class DependencyContainer
    {
        public static void Register(IServiceCollection services)
        {
            //domain bus
            services.AddTransient<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            services.AddTransient<TransferEventHandler>();

            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();
        }
    }
}
