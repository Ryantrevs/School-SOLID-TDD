using Case_School.Data;
using Case_School.Models;
using Case_School.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test
{
    public class IntegrityOfDb
    {
        [Fact]
        public void MontagemDeBancoDeDadosUsandoTodasEntidades()
        {
            //arrage
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);

            var estudante = new Student(Guid.NewGuid().ToString(), false);
            var listaEstudante = new List<Student>() { estudante };
            var turma = new Class(Guid.NewGuid().ToString(), listaEstudante);
            var pesos = new List<WeightProof>() { new WeightProof(Guid.NewGuid().ToString(), new decimal(1.2)), new WeightProof(Guid.NewGuid().ToString(), new decimal(1.8)), new WeightProof(Guid.NewGuid().ToString(), new decimal(2.0)) };
            var materia = new Subject(Guid.NewGuid().ToString(), "Portugues", pesos);

            context.Student.Add(estudante);
            context.Class.Add(turma);
            foreach (var peso in pesos)
            {
                context.WeightProof.Add(peso);
            }
            context.Subject.Add(materia);

            //act
            var response = context.SaveChanges();

            //assert
            Assert.Equal(6, response);
        }

        [Fact]
        public void ConsultaDeBancoDeDadosUtilizandoJoin()
        {
            //arrage
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);

            var estudantes = new List<Student>() { new Student(Guid.NewGuid().ToString(), false), new Student(Guid.NewGuid().ToString(), true) };
            var listaEstudante = new List<Student>(estudantes);
            var turma = new Class(Guid.NewGuid().ToString(), listaEstudante);
            var pesos = new List<WeightProof>() { new WeightProof(Guid.NewGuid().ToString(), new decimal(1.2)), new WeightProof(Guid.NewGuid().ToString(), new decimal(1.8)), new WeightProof(Guid.NewGuid().ToString(), new decimal(2.0)) };
            var materia = new Subject(Guid.NewGuid().ToString(), "Portugues", pesos);
            var notas = new List<StudentGrade>() { new StudentGrade(Guid.NewGuid().ToString(), new decimal(8.5), estudantes[0], materia), new StudentGrade(Guid.NewGuid().ToString(), new decimal(6), estudantes[1], materia) };


            foreach (var estudante in estudantes)
            {
                context.Student.Add(estudante);

            }
            context.Class.Add(turma);
            foreach (var peso in pesos)
            {
                context.WeightProof.Add(peso);
            }
            foreach (var nota in notas)
            {
                context.StudentGrade.Add(nota);
            }

            context.Subject.Add(materia);
            context.SaveChanges();

            var contextStudent = context.Student;
            var contextStudentGrade = context.StudentGrade;

            //act
            var select = from s in context.Student join g in context.StudentGrade on s equals g.Student select new { s.Registration, s.Accredited, g.Average };

            //assert
            Console.WriteLine(select);
            Assert.Equal(2, select.Count());


        }
        [Fact]
        public void FuncionamentoDeRepositorio()
        {
            //arrange
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);
            IUnitOfWork uow = new UnitOfWork(context);
            IRepository<Subject> repo = uow.Repository<Subject>();
            repo.Insert(new Subject(Guid.NewGuid().ToString(), "teste", new List<WeightProof>()));
            //act
            var response = uow.Commit().IsCompletedSuccessfully;
            //asert
            Assert.Equal("True", response.ToString());
        }

        [Fact]
        public void QueryPersonalizada()
        {
            //arrange
            var options = new DbContextOptionsBuilder<CaseSchoolContext>()
                .UseInMemoryDatabase("DbSchool")
                .Options;
            var context = new CaseSchoolContext(options);
            IUnitOfWork uow = new UnitOfWork(context);
            IRepository<Subject> repo = uow.Repository<Subject>();
            repo.Insert(new Subject(Guid.NewGuid().ToString(), "teste", new List<WeightProof>()));
            uow.Commit();
            //act
            var items = repo.Queryable().ToList();
            //asert
            Assert.Equal(1,items.Count);
        }
    }
}
