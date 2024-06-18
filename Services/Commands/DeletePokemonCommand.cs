using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Repositories;

namespace ThePokemonProject.Services.Commands
{
    public class DeletePokemonCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeletePokemonCommand(int pokeId)
        {
            Id = pokeId;
        }
    }
    public class DeletePokemonCommandHandler : IRequestHandler<DeletePokemonCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;

        public DeletePokemonCommandHandler(IMapper mapper, IPokemonRepository pokemonRepository, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
        }


        public async Task<bool> Handle(DeletePokemonCommand request, CancellationToken cancellationToken)
        {
            if (!_pokemonRepository.PokemonExists(request.Id))
            {
                return false;
            }
            var pokemonToDelete = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(request.Id));

            //  var pokemonToDelete = _pokemonRepository.GetPokemon(request.Id);
            var reviewsToDelete = _reviewRepository.GetReviewsOfAPokemon(request.Id);


            foreach (var review in reviewsToDelete)
            {
                _reviewRepository.DeleteReview(review);
            }


            var deletionResult = await Task.FromResult(_pokemonRepository.DeletePokemon(pokemonToDelete));
            return deletionResult;
        }
    }
}