using Case_School.Controllers;
using Case_School.Data;
using Case_School.Models;
using Case_School.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Case_School.Handlers;
using Case_School.Models.ViewModels;

namespace Test
{
    public class TestOfController
    {
        [Fact]
        public void InserirNovasTurmasAlunos()
        {
            //arrange
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);
            IUnitOfWork uow = new UnitOfWork(context);
            IRepository<Class> classRepo = uow.Repository<Class>();
            IRepository<Student> studentRepo = uow.Repository<Student>();
            IFirstHandler handler = new Mock<FirstHandler>(uow).Object;

            var controlador = new ApiController(handler);
            controlador.InsertClassAndStudent(2, 2);
            //act
            ICollection<Class> response = (ICollection<Class>)classRepo.FindAll().Result;
            ICollection<Student> response2 = (ICollection<Student>)studentRepo.FindAll().Result;
            //assert
            Assert.Equal(2.ToString(), response.Count().ToString());
            Assert.Equal(4.ToString(), response2.Count().ToString());

        }

        [Fact]
        public void InserirNovasMaterias()
        {
            //arrange
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);
            IUnitOfWork uow = new UnitOfWork(context);
            IRepository<Subject> subjectRepo = uow.Repository<Subject>();
            IRepository<WeightProof> weightRepo = uow.Repository<WeightProof>();

            IFirstHandler handler = new Mock<FirstHandler>(uow).Object;

            var controlador = new ApiController(handler);
            var names = new List<String>() { "Portugues", "Ingles", "Historia" };
            var pesos = new List<double>() { 2.0, 1.4, 1.6, 1.5, 1.5, 3.0, 5.0, 1.0, 4.0 };
            var subjectVM = new SubjectViewModel(names, pesos);
            //act
            var response = subjectRepo.Queryable().Where(x=>x.Id != null).FirstOrDefault();
            ICollection<WeightProof> response2 = (ICollection<WeightProof>)weightRepo.FindAll().Result;
            //assert
            Assert.NotNull(response);
            Assert.Equal(9, response2.Count());
        }
    }
}
