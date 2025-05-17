using CVSite.SERVICE;
using Microsoft.AspNetCore.Mvc;
using Octokit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CVSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {

        private readonly IGitHubService _gitHubService;
        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("Portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var data = await _gitHubService.GetPortfolioAsync();
            return Ok(data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRepositories([FromQuery] string? name, [FromQuery] string language, [FromQuery] string? user)
        {
            var repos = await _gitHubService.SearchRepositoriesAsync(name, language, user);
            return Ok(repos.Select(r => new
            {
                r.Name,
                r.FullName,
                r.Language,
                r.HtmlUrl,
                r.StargazersCount,
                r.UpdatedAt
            }));
        }
    }
}
