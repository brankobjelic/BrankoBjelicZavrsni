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
    public class AgenciesControllerTest
    {

        [Fact]
        public void GetAgency_InvalidId_ReturnsNotFound()
        {


            //Arrange
            var mockRepository = new Mock<IAgencyRepository>();
            mockRepository.Setup(x => x.GetById(2)).Returns((Agency)null);


            var controller = new AgenciesController(mockRepository.Object);

            //Act
            var actionResult = controller.GetAgency(2) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
        }


    }
}
