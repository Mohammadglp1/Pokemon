using System.Reflection.Metadata.Ecma335;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ThePokemonProject.Dto;
using ThePokemonProject.Models;
using ThePokemonProject.Services.Commands;
using ThePokemonProject.Services.Queries;

public class DeletePokemonEndpoint : EndpointBaseAsync.WithRequest<DeletePokemonEndpoint.DeletePokemonRequest>
    .WithResult<PokemonDto>
{
    private readonly IMediator _mediator;
    public DeletePokemonEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpDelete("api/Pokemon/Delete/{pokeId}")]
    [SwaggerOperation(
            Summary = " Delete Pokemon By Id",
            Description = "Delete Pokemon By Id",
            OperationId = "Pokemon.Delete",
            Tags = new[] { "PokemonEndpoints" })
        ]
    public override async Task<PokemonDto> HandleAsync(DeletePokemonRequest request, CancellationToken cancellationToken = default)
    {
        var command = new DeletePokemonCommand(request.Id);
        var deletionResult = await _mediator.Send(command);
        if (!deletionResult)
        {
            throw new Exception("Failed to delete the Pokemon.");
        }
        return new PokemonDto();
    }
    public class DeletePokemonRequest
    {
        [FromRoute(Name = "pokeId")] public int Id { get; set; }
    }
}