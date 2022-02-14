using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Net5Crud.Clientes;
using Net5Crud.Clientes.Entities;
using Net5Crud.Clientes.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Net5.Deployment.TestNUnit
{
    [TestFixture]
    public class AlumnosControllerTests
    {
       
        private DbContextOptions<ApplicationDBContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
        .UseInMemoryDatabase(databaseName: "MempryDb")
        .Options;

       // private AlumnoRepository _alumnoRepository;
       // private ClientsController _controller;
        private ApiClients _apicontroller;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDb();
            // _alumnoRepository = new AlumnoRepository(new AlumnoContext(_dbContextOptions));
            _apicontroller = new ApiClients(new ApplicationDBContext(_dbContextOptions));
           // _controller = new AlumnosController(_alumnoRepository);
        }

        [Test]
        //public ActionResult GetAll()
        //{
        //    var result = await _apicontroller.GetAll();
        //    var alumnos = result.As<IEnumerable<Client>>();
        //    alumnos.Should().NotBeNullOrEmpty();
        //    alumnos.Count().Should().Be(4);
        //    //Assert.AreEqual(5, result.Count());
        //}
        //[Test]
        //public async Task<ActionResult> GetId()
        //{
        //    // var result = await _controller.GetAsync(new Guid("02540696-8994-42c7-b703-e630223883ab"));
        //    int  Id = 1;
        //    var result = await _apicontroller.GetId(Id);
                 
        //    var alumno = result.As<Client>();
        //    alumno.Should().NotBeNull();
        //    alumno.Nombres.Should().Be("Gabi");            
        //}

        private void SeedDb()
        {
            using var context = new ApplicationDBContext(_dbContextOptions);

            List<Client> alumno = new List<Client>
            {
                new Client{ Id = 1,Nombres="Gabi",Apellidos="Ramos",Edad="5",Nivel="Inicial", FechaRegistro=DateTime.Now },
                new Client{ Id = 2,Nombres="Pedro",Apellidos="Heoap",Edad="10",Nivel="Primaria", FechaRegistro=DateTime.Now },
                new Client{ Id = 3,Nombres="Maria",Apellidos="Neptuno",Edad="14",Nivel="Secundaria", FechaRegistro=DateTime.Now },
                new Client{ Id = 4,Nombres="Luisa",Apellidos="Caere",Edad="15",Nivel="Secundaria", FechaRegistro=DateTime.Now }
                
            };

            context.Clients.AddRange(alumno);
           
            context.SaveChanges();
        }
    }
}