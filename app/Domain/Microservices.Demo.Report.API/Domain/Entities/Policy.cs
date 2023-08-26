namespace Microservices.Demo.Report.API.Domain.Entities
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string Number { get; set; }
        public string ProductCode { get; set; }
        public string AgentLogin { get; set; }
        public int PolicyStatusId { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
