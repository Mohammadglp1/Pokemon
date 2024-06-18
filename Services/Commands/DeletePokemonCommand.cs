using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

namespace ThePokemonProject.Services.Commands
{
    public class DeletePokemonCommand : IRequest<bool>
    {
       public int Id { get; set; }
        public DeletePokemonCommand(int pokeId)
        {
            Id=pokeId;
        }
    }

}

