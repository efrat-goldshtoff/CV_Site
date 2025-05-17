using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Octokit;

namespace CVSite.SERVICE
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubOptions _options;

        public GitHubService(IOptions<GitHubOptions> options)
        {
            _options = options.Value;
        }
        public Task<GitHubClient> GetAuthClientAsync()
        {
            var client = new GitHubClient(new ProductHeaderValue("GithubPortfolioApp"));

            if (!string.IsNullOrWhiteSpace(_options.Token))
            {
                client.Credentials = new Credentials(_options.Token);
            }
            return Task.FromResult(client);
        }
        public async Task<List<PortfolioRepositoriesDto>> GetPortfolioAsync()
        {
            var client = await GetAuthClientAsync();
            var repos = await client.Repository.GetAllForCurrent();

            var result = new List<PortfolioRepositoriesDto>();

            foreach (var repo in repos)
            {
                var languages = await client.Repository.GetAllLanguages(repo.Id);
                string mainLanguage = languages.Count > 0 ? languages.First().Name : "N/A";

                //var commits = await client.Repository.Commit.GetAll(repo.Owner.Login, repo.Name);
                //Console.WriteLine("=============" + commits.Count + "==============");
                //foreach (var commit in commits)
                //{
                //    Console.WriteLine("=============" + commit.Commit?.Message + "==============");
                //    Console.WriteLine("=============" + commit.Commit?.Author?.Date + "================");
                //}
                //var lastCommit = commits.FirstOrDefault()?.Commit.Author?.Date ?? DateTime.MinValue;
                var pulls = await client.PullRequest.GetAllForRepository(repo.Owner.Login, repo.Name);
                int pullCount = pulls.Count;

                result.Add(new PortfolioRepositoriesDto
                {
                    Name = repo.Name,
                    Url = repo.HtmlUrl,
                    Description = repo.Description,
                    Language = mainLanguage,
                    Stars = repo.StargazersCount,
                    PullRequestsCount = pullCount,
                    //LastCommitDate = lastCommit.DateTime
                });
            }
            return result;
        }

        public async Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? name, string language, string? user)
        {
            var client = new GitHubClient(new ProductHeaderValue("GithubPortfolioApp"));
            var req = new SearchRepositoriesRequest(name ?? "")
            {
                SortField = RepoSearchSort.Updated,
                Order = SortDirection.Descending
            };
            //if (!string.IsNullOrWhiteSpace(language))
            req.Language = language;
            //req.Language = Language.Parse(language);
            if (!string.IsNullOrWhiteSpace(user))
                req.User = user;
            var result = await client.Search.SearchRepo(req);
            return result.Items;
        }
        //private readonly GitHubClient _client;
        //public GitHubService()
        //{
        //    _client = new GitHubClient(new ProductHeaderValue("my-github-app"));
        //}

        //public async Task<int> GetUserFollowerAsync(string userName)
        //{
        //    var user = await _client.User.Get(userName);
        //    return user.Followers;
        //}

        //public async Task<List<Repository>> SearchRepositoriesInCSharp()
        //{
        //    var request = new SearchRepositoriesRequest("repo-name") { Language = Language.CSharp };
        //    var result = await _client.Search.SearchRepo(request);
        //    return result.Items.ToList();
        //}
    }
}
