using Octokit;

namespace CVSite.SERVICE
{
    public interface IGitHubService
    {
        Task<GitHubClient> GetAuthClientAsync();
        Task<List<PortfolioRepositoriesDto>> GetPortfolioAsync();
        Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? name, string language, string? user);

        //Task<int> GetUserFollowerAsync(string userName);
        //Task<List<Repository>> SearchRepositoriesInCSharp();

    }
}