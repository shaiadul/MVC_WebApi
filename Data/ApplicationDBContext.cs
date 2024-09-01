using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testing.Models;



namespace testing.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ExamsPack> ExamsPacks { get; set; }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<MCQ> MCQs { get; set; }
        public DbSet<MultipleQuestion> MultipleQuestions { get; set; }


    }
}