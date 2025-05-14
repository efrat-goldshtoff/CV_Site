using Octokit;

namespace CVSite.SERVICE
{
    public interface IGitHubService
    {
        Task<int> GetUserFollowerAsync(string userName);
        Task<List<Repository>> SearchRepositoriesInCSharp();

    }
}