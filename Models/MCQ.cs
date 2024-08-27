using System.Collections.Generic;

namespace testing.Models
{
    public class MCQ
    {
        public int Id { get; set; }
        public string? Example { get; set; }
        public string? Picture { get; set; }
        public required string Question { get; set; }
        public List<string>? Details { get; set; }
        public required List<MultipleQuestion> MultipleSelection { get; set; }
    }

    public class MultipleQuestion
    {
        public required string Answer { get; set; }
        public required bool IsCorrect { get; set; }
    }
}
