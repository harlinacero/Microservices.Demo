namespace Microservices.Demo.Report.API.Domain.Entities
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public IList<Cover> Covers { get; set; }
        public IList<Question> Questions { get; set; }
        public int MaxNumberOfInsured { get; set; }
    }

    public class Cover
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public decimal? SumInsured { get; set; }
    }

    public class Question
    {
        public string QuestionCode { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; }
    }

    public enum QuestionType
    {
        Choice,
        Date,
        Numeric
    }
}
