using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Services.Queries;

namespace ThePokemonProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PokemonController(IMediator mediator ,IPokemonRepository pokemonRepository,IReviewRepository reviewRepository, IOwnerRepository ownerRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
            _mediator = mediator;
            ////////this._ownerRepository = ownerRepository;
            ////////this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public  IActionResult GetPokemons()
        {
           
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(pokemons);
        }
        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public async Task <IActionResult> GetPokemon(int pokeId)
        {
            var request = new GetPokemonQuery(pokeId);
         
          
           // var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));
           
           
        }
        [HttpGet("pokeId/ratings")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            var rating = _pokemonRepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId,[FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest(ModelState);
            }
            var pokemon = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);
           
            if (!_pokemonRepository.CreatePokemon(ownerId,categoryId,pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }
        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon( int pokeId, [FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonUpdate)
        {
            if (pokemonUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (pokeId != pokemonUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);
            if (!_pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating country");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pokemonToDelete = _pokemonRepository.GetPokemon(pokeId);
            var reviewsToDelete = _reviewRepository.GetReviewsOfAPokemon(pokeId);
           if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
                {
                
                ModelState.AddModelError("", "something went wrong");
            }
            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting");
            }
            return NoContent();
        }
    }
}
