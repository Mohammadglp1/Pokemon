using System.Reflection.Metadata.Ecma335;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ThePokemonProject.Dto;
using ThePokemonProject.Services.Queries;

public class UpdatePokemonEndpoint : EndpointBaseAsync.WithRequest<UpdatePokemonEndpoint.UpdatePokemonRequest>
    .WithActionResult<bool>
{
    private readonly IMediator _mediator;
    public UpdatePokemonEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpPut("api/Pokemon/Update/{pokeId}")]
    [SwaggerOperation(
            Summary = "Update Pokemon By Id",
            Description = "Update Pokemon By Id",
            OperationId = "Pokemon.Update",
            Tags = new[] { "PokemonEndpoints" })
        ]

    public async override Task<ActionResult<bool>> HandleAsync(UpdatePokemonRequest request, CancellationToken cancellationToken = default)
    {
        //command
        var op = await _mediator.Send(request, cancellationToken);
        return Ok(op);
    }
    
    public class UpdatePokemonRequest
    {
        [FromRoute(Name = "pokeId")]
        public int PokeId { get; set; }
        [FromBody] public UpdatePokemonRequestBody Body { get; set; }
    }
    public class UpdatePokemonRequestBody
    {
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
        public PokemonDto PokemonUpdate { get; set; }
    }
}