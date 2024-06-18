using MediatR;
using ThePokemonProject.Dto;
using static CreatePokemonEndpoint;

namespace ThePokemonProject;


public class CreatePokemonCommand : IRequest<int>
    {
   
   public CreatePokemonRequestBody CreatePokemonRequestBody { get; set; }
        public CreatePokemonCommand(CreatePokemonRequestBody createPokemonRequestBody)
        {
           
            CreatePokemonRequestBody = createPokemonRequestBody;
            
        }
    }

