using Case_School.Data;
using Case_School.Models;
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
        public Task InsertSubject(List<String> objects, List<double> Weights);
    }
    public class FirstHandler : IFirstHandler
    {
        private readonly IUnitOfWork _uow;

        public FirstHandler(CaseSchoolContext dbContext)
        {
            this._uow = new UnitOfWork(dbContext);
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

        public async Task InsertSubject(List<String> names, List<double> Weights)
        {
            int j = 0;
            for (int i = 0; i < names.Count(); i++, j+=3)
            {
                var weightList = new WeightProof().generateListWeight((List<double>)Weights.GetRange(j, j+2).ToList());
                Subject subject = new Subject(Guid.NewGuid().ToString(), names[i], weightList);
                await _uow.Repository<Subject>().Insert(subject);
            }
            await _uow.Commit();
        }
    }
}
