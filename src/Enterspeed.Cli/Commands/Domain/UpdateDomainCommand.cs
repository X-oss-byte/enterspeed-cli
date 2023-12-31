﻿using Enterspeed.Cli.Api.Domain;
using Enterspeed.Cli.Services.ConsoleOutput;
using MediatR;
using System.CommandLine;
using System.CommandLine.Invocation;
using Enterspeed.Cli.Domain.Models;

namespace Enterspeed.Cli.Commands.Domain;

internal class UpdateDomainCommand : Command
{
    public UpdateDomainCommand() : base(name: "update", "Update domain")
    {
        AddArgument(new Argument<Guid>("id", "Id of the domain") { Arity = ArgumentArity.ExactlyOne });
        AddOption(new Option<string>(new[] { "--name", "-n" }, "Name of domain"));
        AddOption(new Option<string>(new[] { "--hostnames", "-h" }, "List of hostnames, separated by semicolon."));
    }

    public new class Handler : BaseCommandHandler, ICommandHandler
    {
        private readonly IMediator _mediator;
        private readonly IOutputService _outputService;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Hostnames { get; set; }


        public Handler(IMediator mediator, IOutputService outputService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _outputService = outputService;
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var request = new UpdateDomainRequest(DomainId.Parse(DomainId.From(Id.ToString())));
            if (!string.IsNullOrEmpty(Name))
            {
                request.Name = Name;
            }

            if (!string.IsNullOrEmpty(Hostnames))
            {
                var hostnames = Hostnames.Split(';');
                request.Hostnames = hostnames.Select(host => host.Trim()).ToArray();
            }

            var response = await _mediator.Send(request);

            _outputService.Write(response);
            return 0;
        }
    }
}