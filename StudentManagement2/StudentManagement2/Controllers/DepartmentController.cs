﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement2.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: /<controller>/
        public string List()
        {
            return "DepartmentsController中的List()方法";
        }

        public string Details()
        {
            return "DepartmentsController中的Details()方法";
        }
    }
}
