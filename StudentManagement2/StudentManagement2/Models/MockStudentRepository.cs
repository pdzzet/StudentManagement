using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement2.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;

        public MockStudentRepository()
        {
            _studentList = new List<Student>
            {
                new Student(){ Id = 1,Name = "张三",ClassName = ClassNameEnum.FirstGrade,Email = "Tony-zhang@qq.com"},
                new Student(){ Id = 2,Name = "李四",ClassName = ClassNameEnum.SecondGrade,Email = "lisi@qq.com"},
                new Student(){ Id = 3,Name = "王二麻子",ClassName = ClassNameEnum.GradeThree,Email = "wang@qq.com"},
            };
        }

        /// <summary>
        /// 添加一名新的学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student Add(Student student)
        {
            student.Id = _studentList.Max(s => s.Id) + 1;
            _studentList.Add(student);
            return student;
        }

        /// <summary>
        /// 删除一名学生的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student Delete(int id)
        {
            Student student = _studentList.FirstOrDefault(s=>s.Id==id);
            if (student!=null) 
            {
                _studentList.Remove(student);
            }
            return student;
        }

        /// <summary>
        /// 通过id来获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(a=>a.Id==id);
        }

        /// <summary>
        /// 获取所有的学生信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }

        /// <summary>
        /// 更新一名学生的信息
        /// </summary>
        /// <param name="updateStudent"></param>
        /// <returns></returns>
        public Student Update(Student updateStudent)
        {
            Student student = _studentList.FirstOrDefault(s => s.Id == updateStudent.Id);

            if (student != null) 
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.ClassName = updateStudent.ClassName;
            }
            return student;
        }
    }
}
