using Case_School.Data;
using Case_School.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Case_School.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ApiController : Controller
    {
        private readonly IFirstHandler _handler;
        //private readonly CaseSchoolContext dbContext;

        public ApiController(CaseSchoolContext dbContext)
        {
            this._handler = new FirstHandler(dbContext);
        }

        public void InsertClassAndStudent(int numStudent, int numClass)
        {
            _handler.InsertClassStudent(numStudent, numClass);
            return;
        }

        [Route("/step2")]
        public void InsertSubjects(List<String> objects, List<double> Weights)
        {
            _handler.InsertSubject(objects, Weights);
            return;
        }
    }
}
