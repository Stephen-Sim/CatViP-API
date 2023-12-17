using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

namespace CatViP_API.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;

        public AnalysisService(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public ResponseResult<Dictionary<string, int>> GetMissingCatsCount(string query)
        {
            var res = new ResponseResult<Dictionary<string, int>>();

            if (query == "lastOneWeek")
            {
                var dicts = new Dictionary<string, int>();

                for (var i = DateTime.Today.AddDays(-6).Date; i <= DateTime.Today.Date; i = i.AddDays(1))
                {
                    dicts.Add(i.DayOfWeek.ToString(), _analysisRepository.GetMissingCatCount(i));
                }

                res.Result = dicts;
            }
            else if (query == "last28Days")
            {
                var dicts = new Dictionary<string, int>();
                var startOfPeriod = DateTime.Today.AddDays(-27);
                var endOfPeriod = DateTime.Today;

                for (var i = startOfPeriod; i <= endOfPeriod; i = i.AddDays(7))
                {
                    var endOfWeek = i.AddDays(6) > endOfPeriod ? endOfPeriod : i.AddDays(6);
                    dicts.Add($"Week of {i:MMMM dd}", _analysisRepository.GetMissingCatCount(i, endOfWeek));
                }

                res.Result = dicts;
            }
            else if (query == "last12Months")
            {
                var dicts = new Dictionary<string, int>();
                var startOfYear = DateTime.Today.AddMonths(-11);
                var endOfYear = DateTime.Today;

                for (var i = startOfYear; i <= endOfYear; i = i.AddMonths(1))
                {
                    var endOfMonth = i.AddMonths(1).AddDays(-1) > endOfYear ? endOfYear : i.AddMonths(1).AddDays(-1);
                    dicts.Add(i.ToString("MMMM yyyy"), _analysisRepository.GetMissingCatCount(i, endOfMonth));
                }

                res.Result = dicts;
            }
            else
            {
                res.IsSuccessful = false;
                res.ErrorMessage = "Invalid query indentifier";
            }

            return res;
        }

        public Dictionary<string, int> GetUsersCount()
        {
            return new Dictionary<string, int>
            {
                { "Cat Owner", _analysisRepository.GetCatOwnerCount() },
                { "Cat Expert", _analysisRepository.GetCatExpertCount() },
            };
        }

        public ResponseResult<int> GetExpertTipsCount(string query)
        {
            Dictionary<string, int> queryValues = new Dictionary<string, int>()
            {
                { "today", _analysisRepository.GetTodayTipsCount() },
                { "lastWeek", _analysisRepository.GetOneWeekTipsCount() },
                { "lastMonth", _analysisRepository.GetOneMonthTipsCount() },
                { "last3Months", _analysisRepository.GetThreeMonthsTipsCount() },
            };

            var res = new ResponseResult<int>();

            if (queryValues.ContainsKey(query))
            {
                res.Result = queryValues[query];
            }
            else
            {
                res.IsSuccessful = false;
                res.ErrorMessage = "Invalid query indentifier";
            }

            return res;
        }
    }
}
