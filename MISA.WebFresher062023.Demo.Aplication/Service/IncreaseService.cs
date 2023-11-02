using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;
using MISA.WebFresher062023.Demo.Domain.Resource;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseService : BaseCrudService<Increase, Guid, IncreaseDto, IncreaseCreateDto, IncreasePutDto>, IIncreaseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IIncreaseRepository _increaseRepository;
        private readonly IIncreaseAssetService _increaseAssetService;
        private readonly ISourceService _sourceService;
        private readonly IAutoCodeService _autoCodeService;
        private readonly IIncreaseManger _increaseManger;

        #region contructor
        public IncreaseService(IMapper mapper, IUnitOfWork uow, IIncreaseRepository increaseRepository, IIncreaseAssetService increaseAssetService, ISourceService sourceService, IAutoCodeService autoCodeService, IIncreaseManger increaseManger) : base(increaseRepository)
        {
            _mapper = mapper;
            _uow = uow;
            _increaseRepository = increaseRepository;
            _increaseAssetService = increaseAssetService;
            _sourceService = sourceService;
            _autoCodeService = autoCodeService;
            _increaseManger = increaseManger;
        }
        #endregion

        /// <summary>
        /// hàm lọc theo điều kiện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        /// createdby: ttnham(06/09/2023)
        public async Task<IncreaseFilterDto> FilterIncreaseAsync(int pageSize, int pageNumber, string? searchString)
        {
            var filterIncrease = new FilterIncrease()
            {
                PageNumber = pageNumber == 0 ? 1 : pageNumber,
                PageSize = pageSize == 0 ? 10 : pageSize,
                SearchString = searchString == null ? "%%" : searchString
            };

            var filterIncreaseResult = await _increaseRepository.FilterIncreaseAysnc(filterIncrease);
            var filterIncreaseDto = _mapper.Map<IncreaseFilterDto>(filterIncreaseResult);

            for (int i = 0; i < filterIncreaseDto.Data.Count; i++)
            {
                var assetIncrease = await _increaseAssetService.GetAllIncreaseAssetByIncreaseIdAsync(filterIncreaseDto.Data[i].IncreaseId);
                filterIncreaseDto.Data[i].IncreaseAsset = assetIncrease;
            }

            filterIncreaseDto.TotalPage = (int)Math.Ceiling((double)filterIncreaseDto.TotalRecord / filterIncreaseDto.PageSize);

            return filterIncreaseDto;
        }

        /// <summary>
        /// hàm thêm mới 1 chứng từ
        /// </summary>
        /// <param name="increaseCreateDto"></param>
        /// <param name="assetsUpdateDto"></param>
        /// <returns></returns>
        /// 
        public override async Task<int> InsertAsync(IncreaseCreateDto increaseCreateDto)
        {
            var increaseAssets = new List<IncreaseAsset>();
            var sources = new List<Source>();
            var increase = await MapEntityCreateDtoToEntity(increaseCreateDto);
            var increaseId = Guid.NewGuid();

            increase.ModifyDate = DateTimeOffset.Now;
            increase.IncreaseId = increaseId;

            var assets = increaseCreateDto.AssetUpdateDtos.Select(assetUpdateDto =>
            {
                var assetDetailId = Guid.NewGuid();
                increaseAssets.Add(new IncreaseAsset()
                {
                    IncreaseAssetId = assetDetailId,
                    AssetId = assetUpdateDto.AssetId ?? Guid.NewGuid(),
                    IncreaseId = increaseId,
                    IncreaseCost = assetUpdateDto.IncreaseCost,
                });
                if (assetUpdateDto.Sources != null)
                {
                    assetUpdateDto?.Sources?.ForEach(sourceCreateDto =>
                    {
                        var source = _mapper.Map<Source>(sourceCreateDto);
                        source.SetId(Guid.NewGuid());
                        source.IncreaseAssetId = assetDetailId;
                        
                        sources.Add(source);
                    });

                }

                return _mapper.Map<Asset>(assetUpdateDto);
            }).ToList();

            try
            {
                await _uow.BeginTransactionAsync();


                var result = await _increaseRepository.InsertAsync(increase);

                await _autoCodeService.UpdateCodeAsync(AutoCodeType.Increase, increase.IncreaseCode);

                await _increaseAssetService.InsertMultiAsync(increaseAssets);

                await _sourceService.InsertMultiAsync(sources);

                await _uow.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollBackAsync();
                throw ex;
            }
            finally
            {
                await _uow.DisposeAsync();
            }
        }

        /// <summary>
        /// hàm cập nhật chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <param name="increaseUpdateDto"></param>
        /// <param name="assetsUpdateDto"></param>
        /// <param name="increaseAssetDeleteIds"></param>
        /// <param name="sourceDeleteId"></param>
        /// <returns></returns>
        public override async Task<int> UpdateAsync(Guid increaseId, IncreasePutDto entityUpdateDto)
        {
            var increaseAssetsCreate = new List<IncreaseAsset>();
            var increaseAssetUpdate = new List<IncreaseAsset>();


            var sourcesCreate = new List<Source>();
            var sourcesUpdate = new List<Source>();

            entityUpdateDto.AssetsUpdateDto.ForEach(assetUpdateDto =>
            {
                var asset = _mapper.Map<Asset>(assetUpdateDto);

                increaseAssetUpdate.Add(new IncreaseAsset()
                {
                    IncreaseAssetId = assetUpdateDto.IncreaseAssetId ?? throw new NotFoundException(),
                    AssetId = assetUpdateDto.AssetId ?? throw new NotFoundException(),
                    IncreaseCost = assetUpdateDto.IncreaseCost,
                    IncreaseId = increaseId
                });

                assetUpdateDto.Sources?.ForEach(sourceCreate =>
                {
                    var source = _mapper.Map<Source>(sourceCreate);
                    source.IncreaseAssetId = assetUpdateDto.IncreaseAssetId ?? throw new NotFoundException();

                    if(source.SourceId == null || source.SourceId == Guid.Empty)
                    {
                        source.SetId(Guid.NewGuid());
                        sourcesCreate.Add(source);
                    }
                });

                assetUpdateDto.SourcesUpdates?.ForEach(item =>
                {
                    var sourceUpdate = _mapper.Map<Source>(item);
                    sourcesUpdate.Add(sourceUpdate);
                });
            });

            entityUpdateDto.AssetsCreateDto.ForEach(assetCreateDto =>
            {
                var asset = _mapper.Map<Asset>(assetCreateDto);

                var increaseAssetId = Guid.NewGuid();

                increaseAssetsCreate.Add(new IncreaseAsset()
                {
                    IncreaseAssetId = increaseAssetId,
                    AssetId = assetCreateDto.AssetId ?? Guid.NewGuid(),
                    IncreaseId = increaseId,
                    IncreaseCost = assetCreateDto.IncreaseCost,
                });

                assetCreateDto.Sources?.ForEach(sourceCreate =>
                {
                    var source = _mapper.Map<Source>(sourceCreate);

                    source.SetId(Guid.NewGuid());
                    source.IncreaseAssetId = increaseAssetId;

                    sourcesCreate.Add(source);
                });
            });

            var increaseAssetDeleteIds = entityUpdateDto.IncreaseAssetDeleteIds;
            increaseAssetDeleteIds.Select(async id =>
            {
                var sourceDelete = await _sourceService.GetSourceByIncreaseAssetIdAsync(id);
                sourceDelete?.ForEach(source =>
                {
                    entityUpdateDto.SourceDeteleIds.Add(source.SourceId);
                });
            });
            var increase = await MapEntityUpdateDtoToEntity(increaseId, entityUpdateDto);
            increase.IncreaseDate = increase.IncreaseDate.AddDays(1);
            increase.IncreaseRecordDate = increase.IncreaseRecordDate.AddDays(1);

            try
            {
                await _uow.BeginTransactionAsync();

                await _sourceService.DeleteManyAsync(entityUpdateDto.SourceDeteleIds);

                await _sourceService.DeleteSourceByIncreaseAssetIds(entityUpdateDto.IncreaseAssetDeleteIds);

                await _increaseRepository.UpdateAsync(increase);

                await _increaseAssetService.UpdateMultiAsync(increaseAssetUpdate);

                await _increaseAssetService.InsertMultiAsync(increaseAssetsCreate);

                await _increaseAssetService.DeleteManyAsync(increaseAssetDeleteIds);

                await _sourceService.InsertMultiAsync(sourcesCreate);

                await _sourceService.UpdateMultiAsync(sourcesUpdate);

                await _uow.CommitAsync();

            }
            catch (Exception ex)
            {

                await _uow.RollBackAsync();
                throw ex;

            }
            finally
            {
                await _uow.DisposeAsync();
            }

            return 1;
        }


        /// <summary>
        /// Hàm xóa chưng từ theo id
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        /// Createdby: ttnham(07/10/2023)
        public override async Task<int> DeleteAsync(Guid increaseId)
        {

            var increaseAssets = await _increaseAssetService.GetIncreaseAssetByIncreaseId(increaseId);

            var increaseAssetIs = increaseAssets.Select(item => item.IncreaseAssetId).ToList();

            var increase = await CrudRepository.GetAsync(increaseId);

            try
            {
                await _uow.BeginTransactionAsync();
                await _sourceService.DeleteSourceByIncreaseIdAsync(increaseId);
                await _increaseAssetService.DeleteManyAsync(increaseAssetIs);
                var result = await _increaseRepository.DeleteAsync(increase);
                await _uow.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollBackAsync();
                throw ex;
            }
            finally
            {
                await _uow.DisposeAsync();
            }

        }

        /// <summary>
        /// Hàm xóa nhiều chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        /// Createdby: TTNham(07/10/2023)
        public override async Task<int> DeleteManyAsync(List<Guid> increaseId)
        {
            var increaseAssets =  await _increaseAssetService.GetIncreaseAssetByListIncreaseIds(increaseId);

            var increaseAssetIds = increaseAssets.Select(item => item.IncreaseAssetId).ToList();

            var increases = (await _increaseRepository.GetManyAsync(increaseId)).Item1;
            var increaseIds = increases.Select(item => item.IncreaseId).ToList();

            try
            {
                await _uow.BeginTransactionAsync();

                await _sourceService.DeleteSourceByIncreaseAssetIds(increaseAssetIds);

                await _increaseAssetService.DeleteManyAsync(increaseAssetIds);

                await _increaseRepository.DeleteManyAsync(increaseIds);

                await _uow.CommitAsync();
            }
            catch (Exception ex) 
            {
                await _uow.RollBackAsync();
                throw;
            }
            finally
            {
                await _uow.DisposeAsync();
            }

            return 1;
        }

        public async override Task<Increase> MapEntityCreateDtoToEntity(IncreaseCreateDto createDto)
        {
            await _increaseManger.CheckDuplicateCodeAsync(createDto.IncreaseCode);
            var result = _mapper.Map<Increase>(createDto);
            return result;
        }

        public override IncreaseDto MapEntityToEntityDto(Increase entity)
        {
            var result = _mapper.Map<IncreaseDto>(entity);
            return result;
        }

        public async override Task<Increase> MapEntityUpdateDtoToEntity(Guid id, IncreasePutDto updateDto)
        {
            await _increaseManger.CheckDuplicateCodeAsync( updateDto.IncreaseUpdateDto.IncreaseCode,id);
            var result = _mapper.Map<Increase>(updateDto.IncreaseUpdateDto);
            result.SetId(id);
            return result;
        }

        public async Task<IncreaseDto> MapIncreaseJoinToIncreaseDto(IncreaseJoin increaseJoin)
        {
            var result = _mapper.Map<IncreaseDto>(increaseJoin);
            return result;
        }

        public override string IncrementNumberString(string numberString)
        {
            // Chuyển chuỗi số thành mảng các chữ số
            char[] digits = numberString.ToCharArray();

            int carry = 1; // Số dư
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int digit = digits[i] - '0' + carry; // Chuyển từ ký tự sang số, cộng thêm số dư

                carry = digit / 10; // Lấy phần thập phân làm số dư cho vòng lặp tiếp theo
                digits[i] = (char)((digit % 10) + '0'); // Lấy chữ số cuối cùng và chuyển lại thành ký tự
            }

            // Nếu vẫn còn số dư sau khi kết thúc vòng lặp, thêm số dư vào phía trước chuỗi
            if (carry > 0)
            {
                return $"{carry}{new string(digits)}";
            }
            else
            {
                return new string(digits);
            }
        }

        public async Task<ExcelPackage> ExportExcelAsync(FilterIncrease baseFilter)
        {
            var increase = await _increaseRepository.FilterIncreaseAysnc(baseFilter);
            if (increase.Data.Count <= 0) throw new NotFoundException();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Danh_sach_chung_tu");

            var colIndex = 1;
            var rowIndex = 3;
            List<int> columnsNumber = new();

            // Tạo header
            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.Index;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.IncreaseCode;
            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.IncreaseDate;
            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.IncreaseRecordDate;
            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.TotalCost;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.Content;

            //Định dạng header
            workSheet.Row(rowIndex).Height = 30;
            using (var range = workSheet.Cells[rowIndex, 1, rowIndex, colIndex - 1])
            {

                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            // Định dạng cho tất cả các cột
            var rowEnd = rowIndex + increase.Data.Count;
            var colEnd = colIndex - 1;
            using (var range = workSheet.Cells[rowIndex + 1, 1, rowEnd, colEnd])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            }

            foreach (var column in columnsNumber)
            {
                workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            using (var range = workSheet.Cells[1, 1, 1, colEnd])
            {
                range.Merge = true;
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 20;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Value = "DANH SÁCH CHỨNG TỪ";
            }

            foreach(var increaseItem in increase.Data)
            {
                colIndex = 1;
                rowIndex++;
                workSheet.Cells[rowIndex, colIndex++].Value = rowIndex - 3;
                workSheet.Cells[rowIndex, colIndex++].Value = increaseItem.IncreaseCode;
                workSheet.Cells[rowIndex, colIndex++].Value = increaseItem.IncreaseDate;
                workSheet.Cells[rowIndex, colIndex++].Value = increaseItem.IncreaseRecordDate;
                workSheet.Cells[rowIndex, colIndex++].Value = increaseItem.Content;
                workSheet.Cells[rowIndex, colIndex++].Value = increaseItem.Content;
            }

            workSheet.Cells.AutoFitColumns();

            return package;
        }
    }
}
