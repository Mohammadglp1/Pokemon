using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ThePokemonProject;
using ThePokemonProject.Services.Queries;
using PokemonDto = ThePokemonProject.Dto.PokemonDto;

public class GetPokemonEndpoint 
: EndpointBaseAsync
    .WithRequest<GetPokemonEndpoint.GetPokemonRequest>
    .WithActionResult<ResultWrapper<PokemonDto>>
{
    
    private readonly IMediator _mediator;
    public GetPokemonEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
   [Authorize]
    [HttpGet("api/Pokemon/Get/{pokeId}")]
    [SwaggerOperation(
            Summary = " Get Pokemon By Id",
            Description = "Get Pokemon By Id",
            OperationId = "Pokemon.Get",
            Tags = new[] { "PokemonEndpoints" })
        ]
    public override async Task<ActionResult<ResultWrapper<PokemonDto>>> HandleAsync([FromRoute]GetPokemonRequest request, CancellationToken cancellationToken = default)
    {
        var query = new GetPokemonQuery(request.Id);
        var pokemon = await _mediator.Send(query);
        return Ok(pokemon);
    }
    public class GetPokemonRequest
    {
        [FromRoute(Name = "pokeId")] public int Id { get; set; }
    }
}