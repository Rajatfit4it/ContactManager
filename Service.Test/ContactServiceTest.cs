using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.DBModel;
using ViewModel;
using Moq;
using DAL.IRepositories;
using DAL;
using AutoMapper;

namespace Service.Test
{
    [TestClass]
    public class ContactServiceTest
    {

        private readonly Mock<IRepository<Contact>> _repository;
        private readonly Mock<IContactRepository> _contactRepository;
        private readonly Mock<IDbContext> _dbContext;

        public ContactServiceTest()
        {
            _dbContext = new Mock<IDbContext>();
            _repository = new Mock<IRepository<Contact>>();
            _contactRepository = new Mock<IContactRepository>();
            Mapper.Initialize(e =>
            {
                e.CreateMap<ContactVM, Contact>().ReverseMap();
            });

        }

        [TestMethod]
        public void Add()
        {
            

        }

        [TestMethod]
        public void Update()
        {

        }

        [TestMethod]
        public void GetAll()
        {

        }

        [TestMethod]
        public void GetAllWtihPageNo()
        {

        }

        [TestMethod]
        public void Get()
        {
            //var contactvm = new ContactVM() { Name = "unittest1", Email = "unittesting@unittesting.com", PhoneNo = "777777777" };
            //mock.Setup(x => x.Add(contactvm)).ReturnsAsync(100);
            _repository.Setup(e => e.Get(It.IsAny<int>())).ReturnsAsync(new Contact() { Name = "Third" });
            var _contactService = new ContactService(_repository.Object, _contactRepository.Object);
            //var mapperMock = new Mock<IMapper>();
            //_repository.Setup(x => x.Map<Contact, ContactVM>(It.IsAny<Contact>)).Returns(expectedResult);
            ContactVM contactvm = _contactService.Get(3).Result;
            Assert.IsNotNull(contactvm);
            Assert.AreEqual(contactvm.Name, "Third");

        }

        [TestMethod]
        public void GetTotalRecordsCount()
        {

        }

        public void Delete()
        {

        }
    }
}
