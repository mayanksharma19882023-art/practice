using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using TestApplication.Controllers;
using TestApplication.Data;
using TestApplication.Models;

namespace TestApplicationTestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var mockDBContext = new Mock<ApplicationDbContext>();
            Moq.Language.Flow.IReturnsResult<ApplicationDbContext> returnsResult = mockDBContext.Setup(e=> e.Registration).Returns(IDbSet);
            //var mockService = new Mock<ApplicationDbContext>();
            //mockService.Setup(r=> r)
            RegistrationsController registrationsController = new RegistrationsController(mockDBContext.Object);
            List<int> list = new List<int>();
            var testStringResult = await registrationsController.Index();
            testStringResult.Should().Match(e => ((string)e).Contains("Index"));
        }
    }

    public class FakeDbSet<T> : System.Data.Entity.DbSet<T>, IDbSet<T> where T : class
    {
        List<T> _data;

        public FakeDbSet()
        {
            _data = new List<T>();
        }

        public override T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            return null;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public List<T> Local
        {
            get { return _data; }
        }

        public override IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
            return _data;
        }

        public override IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            for (int i = entities.Count() - 1; i >= 0; i--)
            {
                T entity = entities.ElementAt(i);
                if (_data.Contains(entity))
                {
                    Remove(entity);
                }
            }

            return this;
        }

        Type IQueryable.ElementType => _data.AsQueryable().ElementType;

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _data.GetEnumerator();
    }
}
