using AutoMapper;
using CatViP_API.DTOs.CaseReportDTOs;
using CatViP_API.Models;
using CatViP_API.Repositories;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

namespace CatViP_API.Services
{
    public class CaseReportService : ICaseReportService
    {
        private readonly ICaseReportRepository _caseReportRepository;
        private readonly IMapper _mapper;

        public CaseReportService(ICaseReportRepository caseReportRepository, IMapper mapper)
        {
            _caseReportRepository = caseReportRepository;
            _mapper = mapper;
        }

        public async Task<ResponseResult> CreateCaseReport(long authId, CaseReportRequestDTO caseReportRequestDTO)
        {
            var storeResult = new ResponseResult();

            var catCaseReport = new CatCaseReport()
            {
                Address = caseReportRequestDTO.Address,
                CatId = caseReportRequestDTO.CatId,
                CatCaseReportStatusId = 1,
                Description = caseReportRequestDTO.Description,
                Latitude = caseReportRequestDTO.Latitude,
                Longitude = caseReportRequestDTO.Longitude,
                UserId = authId,

                CatCaseReportImages = caseReportRequestDTO.CaseReportImages.Select(pi => new CatCaseReportImage
                {
                    Image = pi.Image,
                    IsBloodyContent = pi.IsBloodyContent
                }).ToList(),
            };

            storeResult.IsSuccessful = await _caseReportRepository.StoreCaseReport(catCaseReport);

            if (!storeResult.IsSuccessful)
            {
                storeResult.ErrorMessage = "fail to store";
                return storeResult;
            }

            // push notification

            return storeResult;
        }
    }
}
