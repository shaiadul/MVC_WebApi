using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testing.Models
{

    public class ExamsPack
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public List<Exams>? Exams { get; set; } = new List<Exams>();
    }
    public class Exams
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public List<MCQ>? MCQs { get; set; } = new List<MCQ>();
    }
    public class MCQ
    {
        public int Id { get; set; }
        public string? Example { get; set; }
        public string? Picture { get; set; }
        public required string Question { get; set; }
        public List<string>? Details { get; set; }
        public List<MultipleQuestion>? MultipleSelection { get; set; }
    }

    public class MultipleQuestion
    {
        public int Id { get; set; }
        public required string Answer { get; set; }
        public required bool IsCorrect { get; set; }
    }
}
