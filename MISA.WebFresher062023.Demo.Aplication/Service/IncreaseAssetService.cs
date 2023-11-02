using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseAssetService : BaseCrudService<IncreaseAsset, Guid, IncreaseAssetDto, IncreaseAssetCreateDto, IncreaseAssetUpdateDto>, IIncreaseAssetService
    {
        private readonly IMapper _mapper;
        private readonly IIncreaseAssetRepository _increaseDetailRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly ISourceService _sourceService;
        private readonly IUnitOfWork _uow;

        public IncreaseAssetService(IIncreaseAssetRepository repository, IMapper mapper, IIncreaseAssetRepository increaseDetailRepository, ISourceRepository sourceRepository, IUnitOfWork uow, ISourceService sourceService) : base(repository)
        {
            _mapper = mapper;
            _increaseDetailRepository = increaseDetailRepository;
            _sourceRepository = sourceRepository;
            _uow = uow;
            _sourceService = sourceService;
        }

        public async Task<List<IncreaseAssetDto>> GetAllIncreaseAssetByIncreaseIdAsync(Guid increaseId)
        {
            var assetIncreaseInfo = await _increaseDetailRepository.GetListIncreaseDetailByIncreaseId(increaseId);

            var assetIncreaseDto = _mapper.Map<List<IncreaseAssetDto>>(assetIncreaseInfo);

            return assetIncreaseDto;
        }

        public async Task<List<IncreaseAssetDto>> GetIncreaseAssetByIncreaseId(Guid increaseId)
        {
            var increaseAsset = await _increaseDetailRepository.GetIncreaseAssetByIncreaesId(increaseId);

            var increaseAssetDto = increaseAsset.Select(item => MapEntityToEntityDto(item)).ToList();

            return increaseAssetDto;
        }

        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                await _sourceRepository.DeleteSourceByIncreaseAssetId(id);
                await base.DeleteAsync(id);
                await _uow.CommitAsync();
                return 1;
            } catch (Exception ex)
            {
                await _uow.RollBackAsync();
                throw ex;
            } 
            finally
            {
                await _uow.DisposeAsync();
            }

        }

        public override async Task<int> UpdateAsync(Guid id, IncreaseAssetUpdateDto entityUpdateDto)
        {
            var increaseAsset = await MapEntityUpdateDtoToEntity(id, entityUpdateDto);
            var sourcesCreate = entityUpdateDto.sourcesCreateDto?.Select(item =>
            {
                var source = _mapper.Map<Source>(item);
                source.SetId(Guid.NewGuid());
                source.IncreaseAssetId = id;

                return source;
            }).ToList();

            var sourcesUpdate = entityUpdateDto.sourcesUpdateDto?.Select(item =>
            {
                var source = _mapper.Map<Source>(item);

                return source;
            }).ToList();    

            try
            {
                _uow.BeginTransaction();

                if(entityUpdateDto.sourcesDeleteId != null)
                {
                await _sourceService.DeleteManyAsync(entityUpdateDto.sourcesDeleteId);
                }
                
                if(sourcesCreate != null)
                {
                    await _sourceService.InsertMultiAsync(sourcesCreate);
                }

                if(sourcesUpdate != null)
                {
                    await _sourceService.UpdateMultiAsync(sourcesUpdate);
                }

                await _increaseDetailRepository.UpdateAsync(increaseAsset);

                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
                throw ex;
            }
            finally
            {
                _uow.Dispose();
            }

            return 1;
        }



        public async Task<int> DeleteIncreaseAssetByIncreaseIdAsync(Guid increaseId)
        {
            var result = await _increaseDetailRepository.DeleteAssetIncreaseByIncreaseId(increaseId);
            return result;
        }

        public async Task<List<IncreaseAssetDto>> GetIncreaseAssetByListIncreaseIds(List<Guid> increaseIds)
        {
            var increaseAsset = await _increaseDetailRepository.GetIncreaseAssetByListIncreaseIds(increaseIds);

            var increaseAssetDto = increaseAsset.Select(item => MapEntityToEntityDto(item)).ToList();

            return increaseAssetDto;
        }

        public async override Task<IncreaseAsset> MapEntityCreateDtoToEntity(IncreaseAssetCreateDto createDto)
        {
            var increaseDetail = _mapper.Map<IncreaseAsset>(createDto);
            return increaseDetail;
        }

        public override IncreaseAssetDto MapEntityToEntityDto(IncreaseAsset entity)
        {
            var increaseDetailDto = _mapper.Map<IncreaseAssetDto>(entity);
            return increaseDetailDto;
        }

        public async override Task<IncreaseAsset> MapEntityUpdateDtoToEntity(Guid id, IncreaseAssetUpdateDto updateDto)
        {
            var increaseDatail = _mapper.Map<IncreaseAsset>(updateDto);
            increaseDatail.SetId(id);
            return increaseDatail;
        }

        public override string IncrementNumberString(string numberString)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaseIdAndAssetIds(Guid inceaseId, List<Guid> assetIds)
        {
            var result = await _increaseDetailRepository.GetIncreaseAssetByIncreaseIdAndAssetIds(inceaseId, assetIds);

            return result;
        }
    }
}
