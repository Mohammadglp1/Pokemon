using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ThePokemonProject;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

public class CreatePokemonEndpoint : EndpointBaseAsync.WithRequest<CreatePokemonEndpoint.CreatePokemonRequest>
    .WithActionResult<PokemonDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public CreatePokemonEndpoint(IMapper mapper ,IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    [HttpPost("api/Pokemon/Create/{pokeId}")]
    [SwaggerOperation(
            Summary = " Create Pokemon",
            Description = "Create Pokemon",
            OperationId = "Pokemon.Create",
            Tags = new[] { "PokemonEndpoints" })
        ]
    
    public override async Task<ActionResult<PokemonDto>> HandleAsync(CreatePokemonEndpoint.CreatePokemonRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreatePokemonCommand(request.Body);
        var pokemonDto = await _mediator.Send(command);
        return Ok(pokemonDto);
    }

    public class CreatePokemonRequest
    {
        [FromBody] public CreatePokemonRequestBody Body { get; set;}
 
       } //todo []
 
     public class CreatePokemonRequestBody
        {
            public int OwnerId { get; set; }
            public int CategoryId { get; set; }
            public PokemonDto PokemonCreate { get; set; }
        }
    
}