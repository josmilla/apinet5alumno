using System;
using System.Collections.Generic;
using Net5Crud.Clientes.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 
using HealthChecks.UI.Client;

namespace Net5Crud.Clientes.Controllers
{
   
    [Route("api/clients")]
    [ApiController]
    public class ApiClients : Controller
    {
        
        private readonly ApplicationDBContext _connection;
        public ApiClients(ApplicationDBContext connection)
        {
            _connection = connection;
        }
        [HttpGet]
        [Route("GetAll")]

        //public Task<IEnumerable<Client>> GetAll() 
        public ActionResult GetAll()
        {
            var data = _connection.Clients.ToList();

            //return Task.FromResult((IEnumerable<Client>)Ok(data));
            return Ok(data);
        }
        //[HttpGet]
        [HttpGet("GetById/{id}", Name = "GetById")]
        //[Route("GetById/{id}")]
        //[Route("GetById/{id}")]
        //public Task<IEnumerable<Client>> GetId(int Id)
        public ActionResult GetId(int Id)
        {

            var data = _connection.Clients.FirstOrDefault(a => a.Id == Id);
            //return Task.FromResult((IEnumerable<Client>)Ok(data));
            return Ok(data);
        }

        [HttpPut("GetById/{id}")]

        public ActionResult Actualizar (int Id, Client alumno)
        {
            var data = _connection.Clients.FirstOrDefault(a => a.Id == Id);
            data.Nombres = alumno.Nombres;
            data.Apellidos = alumno.Apellidos;
            data.Edad = alumno.Edad;
            data.Nivel = alumno.Nivel;
            data.FechaRegistro = alumno.FechaRegistro;
            return Ok(data);

        }

        [HttpDelete]
        [Route("GetById/{id}")]
        //public Task<IEnumerable<Client>> GetId(int Id)
        public ActionResult Delete(int Id)
        {

           // var data = _connection.Clients.FirstOrDefault(a => a.Id == Id);
            var data = _connection.Clients.Remove(_connection.Clients.FirstOrDefault(a => a.Id == Id));
            //var data1 = _connection.Clients.Find(a => a.Id == Id);
            //return Task.FromResult((IEnumerable<Client>)Ok(data));
            return Ok(data);
        }

    }
}
