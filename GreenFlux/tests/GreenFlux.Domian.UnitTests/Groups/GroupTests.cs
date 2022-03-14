using GreenFlux.Application.Interfaces;
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities.GroupAggregate;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Infra.DataAccess.Persistence;
using GreenFlux.Infra.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Domian.UnitTests.Groups
{
    /// <summary>
    /// Unit Tests should run with EF in memory database
    /// GetDatabaseContext() will return an in memory db context whith a Guid as a DbName
    /// </summary>
    [TestFixture]
    public class GroupsTests
    {
        private Group _group;
        private IMock<IDateTime> _time;
        private IGroupRepository _groupRepository;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations = 5;
            _time = new Mock<IDateTime>();
            _unitOfWork = GetDatabaseContext();
            _groupRepository = new GroupRepository(_unitOfWork);

            _group = new Group("Test1", 1000);
            _groupRepository.Add(_group);
            _unitOfWork.SaveChanges();
        }

        #region Group

        [Test, Category("Group")]
        public void ShouldAddGroup()
        {
            _group = new Group("Test1", 1000);
            _groupRepository.Add(_group);
            _unitOfWork.SaveChanges();

            Assert.That(_group.Id, Is.EqualTo(2));
        }

        [Test, Category("Group")]
        public void ShouldRemoveGroup()
        {
            _groupRepository.Delete(_group);
            _unitOfWork.SaveChanges();

            Assert.That(_group.Id, Is.EqualTo(1));
        }

        [Test, Category("Group")]
        public void ShouldSetCapacity()
        {
            AddStation1ToGroup();
            AddConnector1With500ToStation1();

            _group.SetCapacity(500);
            _groupRepository.Update(_group);

            Assert.That(_group.CapacityInAmps, Is.EqualTo(500));
        }

        [Test, Category("Group")]
        public void ShouldThrowExceptionWhenSetCapacity()
        {
            AddStation1ToGroup();
            AddConnector1With500ToStation1();

            Assert.Throws<GroupCapacityLessThanSumMaxCurrentException>(
                () => _group.SetCapacity(300));
        }

        #endregion

        #region ChargeStation in Group

        [Test, Category("ChargeStation in Group")]
        public void ShouldAddChargeStationToGroup()
        {

            AddStation1ToGroup();

            Assert.Multiple(() =>
            {
                Assert.That(_group.ChargeStations.Count, Is.EqualTo(1));
                Assert.That(_group.ChargeStations.FirstOrDefault().Id, Is.EqualTo(1));
            });
        }

        [Test, Category("ChargeStation in Group")]
        public void ShouldRemoveChargeStationFromGroup()
        {
            AddStation1ToGroup();

            _group.RemoveChargeStation(1);

            Assert.IsTrue(!_group.ChargeStations.Any());
        }

        [Test, Category("ChargeStation in Group")]
        public void ShouldThrowExceptionWhenRemoveChargeStationFromGroup()
        {
            AddStation1ToGroup();

            Assert.Throws<ChargeStationNotFoundException>(
                () => _group.RemoveChargeStation(4));
        }

        [Test, Category("ChargeStation in Group")]
        public void ShouldUpdateChargeStationInGroup()
        {
            AddStation1ToGroup();
            _group.UpdateChargeStation(1, "Station2");

            Assert.That(_group.ChargeStations.FirstOrDefault(x => x.Id == 1).Name, Is.EqualTo("Station2"));
        }

        #endregion

        #region Connector in ChargeStation

        [Test, Category("Connector in ChargeStation")]
        public void ShouldAddConnectorToChargeStation()
        {
            AddStation1ToGroup();

            _group.AddConnectorToChargeStation(1, 1, 400);
            _unitOfWork.SaveChanges();

            var cs = _group.ChargeStations.FirstOrDefault(x => x.Id == 1);

            Assert.Multiple(() =>
            {
                Assert.That(cs.Connectors.Count, Is.EqualTo(1));
                Assert.That(cs.Connectors.FirstOrDefault().Id, Is.EqualTo(1));
            });
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldThrowGroupCapacityExceptionWhenAddingConnectorToChargeStation()
        {
            AddStation1ToGroup();

            Assert.Throws<GroupCapacityOverflowException>(
                () => _group.AddConnectorToChargeStation(1, 1, 5000)
                );
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldThrowChargeStationCapacityExceptionWhenAddingConnectorToChargeStation()
        {
            AddStation1ToGroup();

            for (int i = 1; i <= 5; i++)
                _group.AddConnectorToChargeStation(1, i, 50);

            Assert.Throws<ChargeStationCapacityOverflowException>(
                () => _group.AddConnectorToChargeStation(1, 1, 50)
                );
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldThrowConnectorAlreadyExistsExceptionWhenAddingConnectorToChargeStation()
        {
            AddStation1ToGroup();
            
            _group.AddConnectorToChargeStation(1, 1, 50);

            Assert.Throws<ConnectorAlreadyExistsException>(
                () => _group.AddConnectorToChargeStation(1, 1, 50)
                );
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldThrowConnectorNumberNotValidExceptionWhenAddingConnectorToChargeStation()
        {
            AddStation1ToGroup();

            var random = new Random();
            var number = random.Next(ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations + 1, ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations + 5);

            Assert.Throws<ConnectorNumberIsNotValidException>(
                () => _group.AddConnectorToChargeStation(1, 7, 50)
                );
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldRemoveConnectorFromChargeStation()
        {
            AddStation1ToGroup();
            AddConnector1With500ToStation1();


            _group.RemoveConnectorFromChargeStation(1, 1);

            var cs = _group.ChargeStations.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(!cs.Connectors.Any());
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldSetConnectorMaxCurrent()
        {
            AddStation1ToGroup();
            AddConnector1With500ToStation1();

            _group.SetConnectorMaxCurrent(1, 1, 30);

            var cs = _group.ChargeStations.FirstOrDefault(x => x.Id == 1);

            Assert.That(cs.Connectors.FirstOrDefault(x => x.Id == 1).MaxCurrentInAmps, Is.EqualTo(30));
        }

        [Test, Category("Connector in ChargeStation")]
        public void ShouldThrowGroupCapacityExceptionWhenSetConnectorMaxCurrent()
        {
            AddStation1ToGroup();
            AddConnector1With500ToStation1();

            Assert.Throws<GroupCapacityOverflowException>(
                () => _group.SetConnectorMaxCurrent(1, 1, 50000)
                );
        }
        
        #endregion

        private void AddStation1ToGroup()
        {
            _group.AddChargeStation("Station1");
            _groupRepository.Update(_group);
            _unitOfWork.SaveChanges();
        }

        private void AddConnector1With500ToStation1()
        {
            _group.AddConnectorToChargeStation(1, 1, 500);
            _groupRepository.Update(_group);
            _unitOfWork.SaveChanges();
        }

        private UnitOfWork GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<UnitOfWork>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new UnitOfWork(options, _time.Object);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }
    }
}
