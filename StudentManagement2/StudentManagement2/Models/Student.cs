using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement2.Models
{
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        
        [Display(Name ="姓名")]
        [Required(ErrorMessage ="请输入名字"),MaxLength(50,ErrorMessage ="名字的长度不能超过50个字符")]
        public string Name { get; set; }

        [Display(Name = "班级信息")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }

        [Required(ErrorMessage ="请输入邮箱地址")]
        [Display(Name = "邮箱地址")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$",ErrorMessage ="邮箱格式不正确")]
        public string Email { get; set; }

        public string PhotoPath { get; set; }
    }
}
