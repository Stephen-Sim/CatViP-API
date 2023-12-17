namespace CatViP_API.Services.Interfaces
{
    public interface IAnalysisService
    {
        ResponseResult<int> GetExpertTipsCount(string query);
        ResponseResult<Dictionary<string, int>> GetMissingCatsCount(string query);
        Dictionary<string, int> GetUsersCount();
    }
}
