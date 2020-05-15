using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagement2.Models;
using StudentManagement2.ViewModels;

namespace StudentManagement2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IStudentRepository studentRepository,HostingEnvironment hostingEnvironment,ILogger<HomeController> logger)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        public IActionResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }

        //[Route("Home/Details/{id?}")] 
        public IActionResult Details(int id) 
        {
            logger.LogInformation("");
            
            //throw new Exception("此异常发生在Details视图中");

            Student student = _studentRepository.GetStudent(id);

            if (student == null) 
            {
                Response.StatusCode = 404;
                return View("StudentNotFound",id);
            }

            //Student student = _studentRepository.GetStudent(1);
            //实例化HomeDetailsViewModel并存储Student详细信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = student,
                PageTitle = "学生详细信息"
            };

            //ViewData["PageTitle"] = "Studet Details";
            //ViewData["Student"] = student;
            //ViewBag.PageTitle = "学生详情";
            //ViewBag.Student = student;
            //return View("~/MyViews/Test.cshtml");//绝对路径

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model) 
        {
            if (ModelState.IsValid) {

                string uniqueFileName = null;

                if (model.Photo!=null)             //&&model.Photos.Count>0  
                {
                    ProcessUploadedFile(model);                                      
                }


                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Name,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName

                };

                _studentRepository.Add(newStudent);
                return RedirectToAction("Details",new { id = newStudent.Id});

                //Student newStudent = _studentRepository.Add(student);
                //return View("Details", new { id = newStudent.Id });
            }
            return View();
        }

        
        [HttpGet]
        public ViewResult Edit(int id) 
        {
            Student student = _studentRepository.GetStudent(id);
            
            StudentEditViewModel studentEditView = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath
            };
            return View(studentEditView);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model) 
        {
            //检查提供的数据是否有效，如果没有通过验证，需要重新编辑学生信息
            //这样用户就可以更正并重新提交编辑表单
            if (ModelState.IsValid) 
            {
                Student student = _studentRepository.GetStudent(model.Id);

                student.Email = model.Email;
                student.Name = model.Name;
                student.ClassName = model.ClassName;

                string uniqueFileName = ProcessUploadedFile(model);
                if (model.Photo!=null) 
                {
                    if (model.ExistingPhotoPath != null) 
                    {
                        string filePathth = Path.Combine(hostingEnvironment.WebRootPath,"images",model.ExistingPhotoPath);
                        System.IO.File.Delete(filePathth);
                    }
                    uniqueFileName = ProcessUploadedFile(model);
                }

                student.PhotoPath = uniqueFileName;
                Student updateStudent =  _studentRepository.Update(student);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(StudentCreateViewModel model) 
        {
            string uniqueFileName = null;
            //foreach (var photo in model.Photos) 
            //{
            //必须将图像上传到wwwroot中的images文件夹
            //而要获取wwwroot文件夹的路径，我们需要注入asp.net core提供的HostingEnvironment服务
            //通过HostingEnvironment服务去获取wwwroot文件夹的路径  
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线 
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //因为使用了非托管资源，所以需要手动进行释放
            using (var fileStream = new FileStream(filePath,FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
            //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/images文件夹中
            //model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            // }
            return uniqueFileName;
        }
    }
}