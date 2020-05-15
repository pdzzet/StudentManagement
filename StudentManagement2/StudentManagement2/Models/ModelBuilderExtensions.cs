using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement2.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Student>().HasData(
                    new Student
                    {
                        Id = 1,
                        Name = "eike",
                        ClassName = ClassNameEnum.FirstGrade,
                        Email = "eike@qq.com"
                    },
                    new Student
                    {
                        Id = 2,
                        Name = "kasa",
                        ClassName = ClassNameEnum.FirstGrade,
                        Email = "kasa@qq.com"
                    }
                );
        }
    }
}
