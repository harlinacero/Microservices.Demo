using Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos;

namespace Microservices.Demo.Report.API.Aplication
{
    public interface IReportApplicationService
    {
        Task<IEnumerable<PolicyDto>> GetPolicies(string AgentLogin);
    }
}
