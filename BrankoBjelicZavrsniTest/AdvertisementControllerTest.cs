using AutoMapper;
using BrankoBjelicZavrsni.Controllers;
using BrankoBjelicZavrsni.Interfaces;
using BrankoBjelicZavrsni.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BrankoBjelicZavrsniTest
{
    public class AdvertisementControllerTest
    {
        [Fact]
        public void GetAdvertisement_ValidId_ReturnsObject()
        {
            //Arrange
            Advertisement advertisement = new Advertisement()
            {
                Id = 1,
                Name = "Komforna porodicna kuca",
                EstateType = "Kuca",
                YearConstructed = 1987,
                EstatePrice = 110000,
                AgencyId = 3,
                Agency = new Agency() { Id = 3, Name = "Fast nekretnine", YearFounded = 2005 }
            };
            AdvertisementDTO advertisementDTO = new AdvertisementDTO()
            {
                Id = 1,
                Name = "Komforna porodicna kuca",
                EstateType = "Kuca",
                YearConstructed = 1987,
                EstatePrice = 110000,
                AgencyId = 3,
                AgencyName = "Fast nekretnine"
            };

            var mockRepository = new Mock<IAdvertisementRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(advertisement);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AdvertisementProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new AdvertisementsController(mockRepository.Object, mapper);

            //Act
            var actionResult = controller.GetAdvertisement(1) as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            AdvertisementDTO detailDTO = (AdvertisementDTO)actionResult.Value;
            Assert.Equal(advertisementDTO, detailDTO);
        }

        [Fact]
        public void PutAdvertisement_validRequest_returnsObject()
        {
            //Arrange
            Advertisement advertisement = new Advertisement()
            {
                Id = 1,
                Name = "Komforna porodicna kuca",
                EstateType = "Kuca",
                YearConstructed = 1987,
                EstatePrice = 110000,
                AgencyId = 3,
                Agency = new Agency() { Id = 3, Name = "Fast nekretnine", YearFounded = 2005 }
            };
            var mockRepository = new Mock<IAdvertisementRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AdvertisementProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new AdvertisementsController(mockRepository.Object, mapper);

            //Act
            var actionResult = controller.PutAdvertisement(1, advertisement) as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Advertisement resultAd = (Advertisement)actionResult.Value;
            Assert.Equal(advertisement, resultAd);


        }



    }
}
