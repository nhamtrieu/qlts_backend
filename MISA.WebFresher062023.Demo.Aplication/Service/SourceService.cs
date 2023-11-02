using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class SourceService : BaseCrudService<Source, Guid, SourceDto, SourceCreateDto, SourceUpdateDto>, ISourceService
    {
        private readonly IMapper _mapper;
        private readonly ISourceService _sourceService;
        private ISourceRepository _sourceRepository;
        
        public SourceService(ISourceRepository sourceRepository, IMapper mapper) : base(sourceRepository)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
        }

        public async Task<int> DeleteSourceByIncreaseAssetIds(List<Guid> increaseAssetIds)
        {
            var reuslt = await _sourceRepository.DeleteSourceByIncreaseAssetIds(increaseAssetIds);
            return reuslt;
        }

        public async Task<int> DeleteSourceByIncreaseIdAsync(Guid increaseId)
        {
           var result = await _sourceRepository.DeleteSourceByIncreaseIdAsync(increaseId); 
            return result;
        }

        public async Task<List<SourceDto>> GetSourceByAssetIdAsync(Guid assetId)
        {
            var sources = await _sourceRepository.GetSourcetByAssetIdAsync(assetId);
            var sourcesDto = sources.Select(source => MapEntityToEntityDto(source)).ToList();

            return sourcesDto;
        }

        public async Task<List<SourceDto>> GetSourceByIncreaseIdAsync(Guid increaseId)
        {
            var sources = await _sourceRepository.GetSourceByIncreaseIdAsync(increaseId);
            var sourcesDto = sources.Select(source => MapEntityToEntityDto(source)).ToList();

            return sourcesDto;
        }

        public async Task<List<SourceDto>> GetSourceByIncreaseAssetIdAsync(Guid increaseAssetId)
        {
            var sources = await _sourceRepository.GetSourcesByIncreaseAssetIdAsync(increaseAssetId);

            var sourcesDto = sources.Select(source => MapEntityToEntityDto(source)).ToList();

            return sourcesDto;
        }

        public override string IncrementNumberString(string numberString)
        {
            throw new NotImplementedException();
        }

        public override async Task<Source> MapEntityCreateDtoToEntity(SourceCreateDto createDto)
        {
            var result = _mapper.Map<Source>(createDto);
            return result;
        }

        public override SourceDto MapEntityToEntityDto(Source entity)
        {
            var result = _mapper.Map<SourceDto>(entity);
            return result;
        }

        public override async Task<Source> MapEntityUpdateDtoToEntity(Guid id, SourceUpdateDto updateDto)
        {
            var result = _mapper.Map<Source>(updateDto);
            result.SetId(id);
            return result;
        }
    }
}
