﻿namespace CatViP_API.Services.Interfaces
{
    public interface IAnalysisService
    {
        ResponseResult<Dictionary<string, int>> GetPostsAndExpertTipsCount(string query);
        ResponseResult<Dictionary<string, int>> GetMissingCatsCount(string query);
        Dictionary<string, int> GetUsersAndProductsCount();
    }
}
