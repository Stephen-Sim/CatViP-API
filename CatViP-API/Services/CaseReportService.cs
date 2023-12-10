using AutoMapper;
using CatViP_API.DTOs.CaseReportDTOs;
using CatViP_API.DTOs.PostDTOs;
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

        public ResponseResult CheckIsReportExist(long authId, long id)
        {
            var res = new ResponseResult();

            res.IsSuccessful = _caseReportRepository.CheckIsReportExist(authId, id);

            if (!res.IsSuccessful)
            {
                res.ErrorMessage = "Report is not exist";
            }

            return res;
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
                DateTime = DateTime.Now,

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

        public ICollection<OwnCaseReportDTO> GetOwnCaseReports(long autId)
        {
            var cases = _mapper.Map<ICollection<OwnCaseReportDTO>>(_caseReportRepository.GetOwnCaseReports(autId));

            foreach (var caseReport in cases)
            {
                caseReport.CaseReportImages = _mapper.Map<ICollection<CaseReportImageDTO>>(_caseReportRepository.GetCaseReportImages(caseReport.Id));
            }

            return cases;
        }

        public async Task<ResponseResult> RevokeCaseReport(long id)
        {
            var res = new ResponseResult();

            res.IsSuccessful = await _caseReportRepository.RevokeCaseReport(id);

            if (!res.IsSuccessful)
            {
                res.ErrorMessage = "Report is not exist";
            }

            return res;
        }

        public async Task<ResponseResult> SettleCaseReport(long id)
        {
            var res = new ResponseResult();

            res.IsSuccessful = await _caseReportRepository.SettleCaseReport(id);

            if (!res.IsSuccessful)
            {
                res.ErrorMessage = "Report is not exist";
            }

            return res;
        }
    }
}
