using Case_School.Data;
using Case_School.Models;
using Case_School.Models.ViewModels;
using Case_School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_School.Handlers
{
    public interface IFirstHandler
    {
        public Task InsertClassStudent(int numStudent, int numClass);
        public Task InsertSubject(SubjectViewModel subject);
    }
    public class FirstHandler : IFirstHandler
    {
        private readonly IUnitOfWork _uow;

        public FirstHandler(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task InsertClassStudent(int numStudent, int numClass)
        {
            for(int i = 0; i < numClass; i++)
            {
                List<Student> students = new Student().GenerateStudent(numStudent);
                Class newClass = new Class().GenerateClass(students);
                await _uow.Repository<Class>().Insert(newClass);
            }
            await _uow.Commit();
        }

        public async Task InsertSubject(SubjectViewModel subjectViewModel)
        {
            int j = 0;
            for (int i = 0; i < subjectViewModel.Names.Count(); i++, j+=3)
            {
                var weightList = new WeightProof().generateListWeight(subjectViewModel.Weights.GetRange(j, j+2).ToList());
                Subject subject = new Subject(Guid.NewGuid().ToString(), subjectViewModel.Names[i], weightList);
                await _uow.Repository<Subject>().Insert(subject);
            }
            await _uow.Commit();
        }

        public async Task GenerateAllGrade()
        {
            IEnumerable<Student> students = await _uow.Repository<Student>().FindAll();
            IEnumerable<Subject> subjects = await _uow.Repository<Subject>().FindAll();
            var grades = new StudentGrade().GenerateGrade(students, subjects);
            foreach (var grade in grades)
            {
                await _uow.Repository<StudentGrade>().Insert(grade);
            }
           await  _uow.Commit();
        }

        public async Task GenerateAllAverage()
        {

        }
    }
}
