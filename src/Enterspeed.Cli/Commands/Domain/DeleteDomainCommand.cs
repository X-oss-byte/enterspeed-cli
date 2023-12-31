﻿using Enterspeed.Cli.Api.Domain;
using Enterspeed.Cli.Services.ConsoleOutput;
using MediatR;
using System.CommandLine;
using System.CommandLine.Invocation;
using Enterspeed.Cli.Domain.Models;

namespace Enterspeed.Cli.Commands.Domain;

public class DeleteDomainCommand : ConfirmCommand
{
    public DeleteDomainCommand() : base(name: "delete", "Delete domain")
    {
        AddArgument(new Argument<Guid>("id", "Id of the domain") { Arity = ArgumentArity.ExactlyOne });
    }

    public new class Handler : BaseCommandHandler, ICommandHandler
    {
        private readonly IMediator _mediator;
        private readonly IOutputService _outputService;

        public Guid Id { get; set; }
        

        public Handler(IMediator mediator, IOutputService outputService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _outputService = outputService;
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            if (!Yes && !GetConfirmation())
            {
                return 0;
            }

            var response = await _mediator.Send(new DeleteDomainRequest(DomainId.Parse(DomainId.From(Id.ToString()))));
            _outputService.Write(response);

            return 0;
        }
    }
}