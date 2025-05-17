using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace CVSite.SERVICE
{
    public class PortfolioRepositoriesDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string language { get; set; }
        //public List<string> Language { get; set; }
        public int Stars { get; set; }
        public int PullRequestsCount { get; set; }
        //public DateTime? LastCommitDate { get; set; }
    }
}
