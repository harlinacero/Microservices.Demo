namespace Microservices.Demo.Policy.API.Domain
{
    using Microservices.Demo.Policy.API.Domain.Entities;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class PolicyDomainService
    {
        public Address CreateAddress(string country, string zipCode, string city, string street)
        {
            return new Address(country, zipCode, city, street);
        }
        public Policy Buy(Offer offer, PolicyHolder customer)
        {
            return CreatePolicyFromOffer(customer, offer);
        }

        public Policy CreatePolicyFromOffer(PolicyHolder policyHolder, Offer offer)
        {
            return CreatePolicy(policyHolder, offer);
        }
        public Policy CreatePolicy(PolicyHolder policyHolder, Offer offer)
        {
            Policy policy = new Policy
            {
                Number = Guid.NewGuid().ToString(),
                ProductCode = offer.ProductCode,
                PolicyStatusId = (int)Enum.PolicyStatus.Active,
                CreationDate = SysTime.CurrentTime,
                AgentLogin = offer.AgentLogin
            };

            policy.PolicyVersions.Add(CreatePolicyVersionFromOffer(policy, 1, policyHolder, offer));

            return policy;

        }

        public int NextVersionNumber(IList<PolicyVersion>  policyVersions) {
            return policyVersions.Count == 0 ? 1 : LastVersion(policyVersions).VersionNumber + 1;
        }
        public PolicyVersion FirstVersion(IEnumerable<PolicyVersion> versions)
        {
            return versions.First(v => v.VersionNumber == 1);
        }
        public static PolicyVersion LastVersion(IEnumerable<PolicyVersion> versions)
        {
            return versions.OrderByDescending(v => v.VersionNumber).First();
        }

        public PolicyVersion CreatePolicyVersionFromOffer(
           Policy policy,
           int version,
           PolicyHolder policyHolder,
           Offer offer)
        {
            return CreatePolicyVersion(policy, version, policyHolder, offer);
        }

        private PolicyVersion CreatePolicyVersion(
           Policy policy,
           int version,
           PolicyHolder policyHolder,
           Offer offer)
        {
            PolicyVersion policyVersion = new PolicyVersion
            {
                Policy = policy,
                VersionNumber = version,
                PolicyHolder = policyHolder,
                CoverPeriodPolicyValidityPeriod = offer.PolicyValidityPeriod.Clone(),
                VersionValidityPeriodPolicyValidityPeriod = offer.PolicyValidityPeriod.Clone(),
                PolicyCovers = offer.OfferCovers.Select(c => CreatePolicyCover(c, offer.PolicyValidityPeriod.Clone())).ToList()
            };

            policyVersion.TotalPremiumAmount = policyVersion.PolicyCovers.Sum(c => c.Premium);

            return policyVersion;
        }

        public PolicyCover CreatePolicyCover(OfferCover offerCover, PolicyValidityPeriod coverPeriod)
        {
            return new PolicyCover
            {
                Code = offerCover.Code,
                Premium = offerCover.Price,
                PolicyValidityPeriod = coverPeriod
            };
        }

        public int GetDays(DateTime PolicyFrom,DateTime PolicyTo)
        {
            return PolicyTo.Subtract(PolicyFrom).Days;
        }

        public PolicyCover PolicyCoverEndOn(PolicyCover policyCover, DateTime endDate)
        {
            var originalDaysCovered = GetDays(policyCover.PolicyValidityPeriod.PolicyFrom, policyCover.PolicyValidityPeriod.PolicyTo);
            PolicyValidityPeriod policyValidityPeriod = PolicyValidityPeriodEndOn(policyCover.PolicyValidityPeriod, endDate);
            var daysNotUsed = originalDaysCovered - GetDays(policyValidityPeriod.PolicyFrom, policyValidityPeriod.PolicyTo);
            var premium = decimal.Round
            (
                policyCover.Premium - (policyCover.Premium * decimal.Divide(daysNotUsed, originalDaysCovered))
                , 2
            );

            return new PolicyCover
            {
                Code = policyCover.Code,
                Premium = premium,
                PolicyValidityPeriod = PolicyValidityPeriodEndOn(policyCover.PolicyValidityPeriod,endDate)
            };
        }
        public PolicyValidityPeriod PolicyValidityPeriodEndOn(PolicyValidityPeriod policyValidityPeriod, DateTime endDate)
        {
            return new PolicyValidityPeriod(policyValidityPeriod.PolicyFrom, endDate);
        }

        public PolicyTerminationResult Terminate(Policy policy,DateTime terminationDate)
        {
            //ensure is not already terminated
            if (policy.PolicyStatusId != (int)Enum.PolicyStatus.Active)
                throw new ApplicationException($"Policy {policy.Number} is already terminated");

            //get version valid at term date
            PolicyVersion versionAtTerminationDate = EffectiveOn(policy.PolicyVersions,terminationDate);

            if (versionAtTerminationDate == null)
                throw new ApplicationException($"No valid policy {policy.Number} version exists at {terminationDate}. Policy cannot be terminated.");

            if (!versionAtTerminationDate.CoverPeriodPolicyValidityPeriod.Contains(terminationDate))
                throw new ApplicationException($"Policy {policy.Number} does not cover {terminationDate}. Policy cannot be terminated at this date.");

            //create terminal version
            policy.PolicyVersions.Add(PolicyVersionEndOn(versionAtTerminationDate, terminationDate));

            //change status
            policy.PolicyStatusId = (int)Enum.PolicyStatus.Terminated;

            //return term version
            var terminalVersion = LastVersion(policy.PolicyVersions);
            return new PolicyTerminationResult(terminalVersion, versionAtTerminationDate.TotalPremiumAmount - terminalVersion.TotalPremiumAmount);
        }

        public PolicyVersion EffectiveOn(IEnumerable<PolicyVersion> versions, DateTime effectiveDate)
        {
            return versions
                .Where(v => IsEffectiveOn(v,effectiveDate))
                .OrderByDescending(v => v.VersionNumber)
                .FirstOrDefault();
        }
        public bool IsEffectiveOn(PolicyVersion policyVersion, DateTime theDate)
        {
            return policyVersion.CoverPeriodPolicyValidityPeriod.Contains(theDate);
        }

        public PolicyVersion PolicyVersionEndOn(PolicyVersion policyVersion,DateTime endDate)
        {
            var endedCovers = policyVersion.PolicyCovers.Select(c => PolicyCoverEndOn(c,endDate)).ToList();

            var termVersion = new PolicyVersion
            {
                Policy = policyVersion.Policy,
                VersionNumber = NextVersionNumber(policyVersion.Policy.PolicyVersions.ToList()),
                PolicyHolder = new PolicyHolder(policyVersion.PolicyHolder.FirstName, policyVersion.PolicyHolder.LastName, policyVersion.PolicyHolder.Pesel, policyVersion.PolicyHolder.Address),
                CoverPeriodPolicyValidityPeriod = PolicyValidityPeriodEndOn(policyVersion.CoverPeriodPolicyValidityPeriod, endDate),
                VersionValidityPeriodPolicyValidityPeriod = PolicyValidityPeriodBetween(endDate.AddDays(1), policyVersion.VersionValidityPeriodPolicyValidityPeriod.PolicyTo),
                PolicyCovers = endedCovers,
                TotalPremiumAmount = endedCovers.Sum(c => c.Premium)
            };
            return termVersion;
        }

        public PolicyValidityPeriod PolicyValidityPeriodBetween(DateTime validFrom, DateTime validTo)
        {
            return new PolicyValidityPeriod(validFrom, validTo);
        }
    }
}
