using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;
using MISA.WebFresher062023.Demo.Domain.Resource;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetService : BaseCrudService<Asset, Guid, AssetDto, AssetCreateDto, AssetUpdateDto>, IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IAssetManager _assetManager;
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public AssetService(IAssetRepository repository,IAssetCategoryRepository assetCategoryRepository, IDepartmentRepository departmentRepository, IMapper mapper, IAssetManager assetManager) : base(repository)
        {
            _assetRepository = repository;
            _mapper = mapper;
            _assetManager = assetManager;
            _assetCategoryRepository = assetCategoryRepository; 
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> IsDuplicateCodeAsync(string code)
        {
            var asset = await _assetRepository.FindByCodeAsync(code);
            if (asset == null) return false;
            return true;
        }
        
        public async Task<FilterAssetDto> FilterAssetAsync(FilterAssetCreateDto filterCreate)
        {
            var filter = _mapper.Map<FilterAsset>(filterCreate);
            var filterAsset = await _assetRepository.FilterAssetAsync(filter, filterCreate.assetid);
            var filterAssetDto = _mapper.Map<FilterAssetDto>(filterAsset);
            filterAssetDto.TotalPage = (int)Math.Ceiling((double)filterAssetDto.TotalRecord / filterCreate.PageSize);

            return filterAssetDto;

        }

        public async Task<ExcelPackage> ExportExcelAsync(FilterAsset filter)
        {
            var assets = await _assetRepository.FilterBaseAsync(filter);
            if(assets.Count <= 0) 
            {
                throw new NotFoundException();
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Danh_sach_tai_san");

            var colIndex = 1;
            var rowIndex = 3;
            List<int> columnsNumber = new();
            // Tạo header
            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.Index;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.AssetCode;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.AssetName;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.AssetCategoryCode;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.AssetCategoryName;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.DepartmentCode;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.DepartmentName;

            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.Quantity;

            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.Cost;

            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.AccumulatedDepreciation;

            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.ResidualValue;

            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.PurchaseDate;

            workSheet.Column(colIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.UsedDate;

            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.LifeTime;
            columnsNumber.Add(colIndex);
            workSheet.Cells[rowIndex, colIndex++].Value = Resource.DepreciationRate;

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
            var rowEnd = rowIndex + assets.Count;
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
                range.Value = "DANH SÁCH TÀI SẢN";
            }

            foreach (var asset in assets)
            {
                colIndex = 1;
                rowIndex++;
                float residualValue = 0;
                float accumulatedDepreciation;
                var currentYear = DateTimeOffset.Now.Year;
                var userDate = asset.UsedDate.Year ;
                var timeUse = currentYear - userDate;
                if (timeUse >= asset.LifeTime) residualValue = 0;
                else residualValue = (asset.DepreciationRate * (float) asset.Cost * (asset.LifeTime - timeUse)) / 100;
                accumulatedDepreciation = (float) asset.Cost - residualValue;

                workSheet.Cells[rowIndex, colIndex++].Value = rowIndex - 3;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.AssetCode;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.AssetName;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.AssetCategoryCode;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.AssetCategoryName;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.DepartmentCode;
                workSheet.Cells[rowIndex, colIndex++].Value = asset.DepartmentName;
                workSheet.Cells[rowIndex, colIndex++].Value =  asset.Quantity.ToString("N0", new CultureInfo("vi-VN"));
                workSheet.Cells[rowIndex, colIndex++].Value = asset.Cost.ToString("N0", new CultureInfo("vi-VN"));
                workSheet.Cells[rowIndex, colIndex++].Value = accumulatedDepreciation.ToString("N0", new CultureInfo("vi-VN"));
                workSheet.Cells[rowIndex, colIndex++].Value = residualValue.ToString("N0",  new CultureInfo("vi-VN"));
                workSheet.Cells[rowIndex, colIndex++].Value = asset.PurchaseDate.ToString("dd/MM/yyyy");
                workSheet.Cells[rowIndex, colIndex++].Value = asset.UsedDate.ToString("dd/MM/yyyy");
                workSheet.Cells[rowIndex, colIndex++].Value = asset.LifeTime;
                workSheet.Cells[rowIndex, colIndex++].Value = Math.Round(asset.DepreciationRate, 2);
            }

            workSheet.Cells.AutoFitColumns();

            return package;
        }

        public override async Task<Asset> MapEntityCreateDtoToEntity(AssetCreateDto createDto)
        {
            await _assetManager.CheckDuplicateCodeAsync(createDto.AssetCode);
            var assetCreate = _mapper.Map<Asset>(createDto);
            await ValidateAsync(assetCreate, new Guid("00000000-0000-0000-0000-000000000000"));
           
            assetCreate.AssetId = Guid.NewGuid();
            assetCreate.CreatedDate = DateTimeOffset.Now;
            assetCreate.ModifyDate = DateTimeOffset.Now;
            return assetCreate;
        }

        public override async Task<Asset> MapEntityUpdateDtoToEntity(Guid id,AssetUpdateDto updateDto)
        {
            var asset = await _assetRepository.GetAsync(id);
            if(asset.AssetCode != updateDto.AssetCode)
            {
                await _assetManager.CheckDuplicateCodeAsync(updateDto.AssetCode);
            }
            var entity = _mapper.Map<Asset>(updateDto);
            entity.CreatedDate = asset.CreatedDate;
            entity.AssetId = id;
            entity.ModifyDate = DateTimeOffset.Now;
            return entity;
        }

        public override AssetDto MapEntityToEntityDto(Asset entity)
        {
            var entityDto = _mapper.Map<AssetDto>(entity);
            return entityDto;
        }

        public override string IncrementNumberString(string numberString)
        {
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
        }
        /// <summary>
        /// Hàm lưu thông tin phòng ban và loại tài sản vào tài sản
        /// </summary>
        /// <param name="asset">Tài sản</param>
        public async Task SetDepartmentAndAssetCategoryAsync(Asset asset)
        {
            var assetCategory = await _assetCategoryRepository.GetAsync(asset.AssetCategoryId);
            var department = await _departmentRepository.GetAsync(asset.DepartmentId);
            asset.DepartmentCode = department.DepartmentCode;
            asset.DepartmentName = department.DepartmentName;
            asset.AssetCategoryCode = assetCategory.AssetCategoryCode;
            asset.AssetCategoryName = assetCategory.AssetCategoryName;
        }

        private async Task ValidateAsync(Asset asset, Guid assetId)
        {
            _assetManager.CheckValidAssetCode(asset.AssetCode); 
            //_assetManager.CheckLifeTimeAnDepreciationRate(asset.LifeTime, asset.DepreciationRate);
            _assetManager.CheckPurchaseDateAndUseDate(asset.PurchaseDate, asset.UsedDate);

        }

        public async Task<List<Increase>> GetIncreaseByAssetIdAsync(Guid assetId)
        {
            var result = await _assetRepository.GetIncreaseByAssetIdAsync(assetId);
            return result;
        }

        public async Task<List<Guid>> GetIncreaseByListAssetIdsAsync(List<Guid> assetIds)
        {
            var result = await _assetRepository.GetIncreaseByListAssetIdsAsync(assetIds);

            return result;
        }
    }
}
