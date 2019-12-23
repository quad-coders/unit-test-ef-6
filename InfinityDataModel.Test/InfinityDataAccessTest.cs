using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace InfinityDataModel.Test
{
    [TestClass]
    public class InfinityDataAccessTest
    {
        [TestMethod]
        public void TestGetMisc()
        {
            // Data to be returned
            var data = new List<Misc>();
            data.Add(new Misc { Data = "Data1", Description = "Description1" });
            data.Add(new Misc { Data = "Data2", Description = "Description2" });
            data.Add(new Misc { Data = "Data3", Description = "Description3" });

            // Create mock data
            var mockData = new Mock<DbSet<Misc>>();
            mockData.As<IQueryable<Misc>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockData.As<IQueryable<Misc>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockData.As<IQueryable<Misc>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockData.As<IQueryable<Misc>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc)
                .Returns(mockData.Object);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = dataAccess.GetMisc();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Description1", result.FirstOrDefault(o => o.Data == "Data1").Description);
        }

        [TestMethod]
        public async Task TestGetMiscAsync()
        {
            // Data to be returned
            var data = new List<Misc>();
            data.Add(new Misc { Data = "Data1", Description = "Description1" });
            data.Add(new Misc { Data = "Data2", Description = "Description2" });
            data.Add(new Misc { Data = "Data3", Description = "Description3" });

            // Create mock data
            var mockData = GenerateMockDbSet(data.AsQueryable());

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc)
                .Returns(mockData.Object);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = await dataAccess.GetMiscAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Description1", result.FirstOrDefault(o => o.Data == "Data1").Description);
        }

        [TestMethod]
        public void TestInsertMisc()
        {
            // Data to be returned
            var data = new Mock<DbSet<Misc>>();

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc)
                .Returns(data.Object);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = dataAccess.InsertMisc("UnitTestData1", "UnitTestDescription1");
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.Add(It.IsAny<Misc>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task TestInsertMiscAsync()
        {
            // Data to be returned
            var data = new Misc { MiscId = 1, Data = "Data1", Description = "Description1" };

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc.Add(It.IsAny<Misc>()))
                .Returns(data);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = await dataAccess.InsertMiscAsync("UnitTestData1", "UnitTestDescription1");
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.Add(It.IsAny<Misc>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public void TestUpdateMisc()
        {
            // Data to be returned
            var data = new Misc { MiscId = 1, Data = "Data1", Description = "Description1" };

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc.Find(It.IsAny<object[]>()))
                .Returns(data);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = dataAccess.UpdateMisc(1, "UnitTestData1", "UnitTestDescription1");
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.Find(It.IsAny<object[]>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task TestUpdateMiscAsync()
        {
            // Data to be returned
            var data = new Misc { MiscId = 1, Data = "Data1", Description = "Description1" };

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc.FindAsync(It.IsAny<object[]>()))
                .Returns(Task.FromResult(data));

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = await dataAccess.UpdateMiscAsync(1, "UnitTestData1", "UnitTestDescription1");
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.FindAsync(It.IsAny<object[]>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public void TestDeleteMisc()
        {
            // Data to be returned
            var data = new Misc { MiscId = 1, Data = "Data1", Description = "Description1" };

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc.Find(It.IsAny<object[]>()))
                .Returns(data);

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = dataAccess.DeleteMisc(1);
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.Find(It.IsAny<object[]>()), Times.Once);
            mockContext.Verify(m => m.Misc.Remove(It.IsAny<Misc>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task TestDeleteMiscAsync()
        {
            // Data to be returned
            var data = new Misc { MiscId = 1, Data = "Data1", Description = "Description1" };

            // Create mock EF context
            var mockContext = new Mock<InfinityEntities>();
            mockContext
                .Setup(mock => mock.Misc.FindAsync(It.IsAny<object[]>()))
                .Returns(Task.FromResult(data));

            // Assert
            var dataAccess = new InfinityDataAccess(mockContext.Object);
            var result = await dataAccess.DeleteMiscAsync(1);
            Assert.IsTrue(result);
            mockContext.Verify(m => m.Misc.FindAsync(It.IsAny<object[]>()), Times.Once);
            mockContext.Verify(m => m.Misc.Remove(It.IsAny<Misc>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        private Mock<DbSet<T>> GenerateMockDbSet<T>(IQueryable<T> mockData)
            where T : class
        {
            var mock = new Mock<DbSet<T>>();
            mock.As<IDbAsyncEnumerable<T>>()
                .Setup(x => x.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(mockData.GetEnumerator()));

            mock.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(mockData.Provider));

            mock.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(mockData.Expression);

            mock.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(mockData.ElementType);

            mock.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(mockData.ElementType);

            return mock;
        }
    }
}
