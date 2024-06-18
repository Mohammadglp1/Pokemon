using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Repositories;
using ThePokemonProject.Services.Queries;

namespace ThePokemonProject.Services.Handlers
{
    public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery, Owner>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetOwnerQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

      

        public async Task<Owner> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(request.Id));
            return await
                Task.FromResult(_ownerRepository.GetOwner(request.Id));
        }
    }
}
