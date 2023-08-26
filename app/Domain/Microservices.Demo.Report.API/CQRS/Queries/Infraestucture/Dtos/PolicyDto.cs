namespace Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }
        public string Number { get; set; }
        public string ProductCode { get; set; }
        public string AgentLogin { get; set; }
        public int PolicyStatusId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }

        public PolicyDto(int policyId, string number, string productCode, string agentLogin, int policyStatusId, DateTime? creationDate, string productName, string productImage, string productDescription)
        {
            PolicyId = policyId;
            Number = number;
            ProductCode = productCode;
            AgentLogin = agentLogin;
            PolicyStatusId = policyStatusId;
            CreationDate = creationDate;
            ProductName = productName;
            ProductImage = productImage;
            ProductDescription = productDescription;
        }
    }
}
