using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Services.Queries;

namespace ThePokemonProject.Services.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }



        public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(request.Id));
            return await
                Task.FromResult(_categoryRepository.GetCategory(request.Id));
        }
    }
}


