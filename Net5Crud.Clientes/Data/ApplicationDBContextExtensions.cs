
using Net5Crud.Clientes.Entities;
 
using System;
using System.Collections.Generic;

namespace Net5Crud.Clientes.Data
{
    public static class ApplicationDBContextExtensions
    {
        public static void EnsureSeeDataForContext(this ApplicationDBContext context)
        {
            context.Clients.RemoveRange(context.Clients);
            context.SaveChanges();

            

            List<Client> alumnos = new List<Client>();

            alumnos.Add(new Client
            {
                Id = 1,
                Nombres = "Juan P",
                Apellidos = "Xbox Rojas",
                Edad = "10",
                Nivel = "Primaria",
                FechaRegistro = DateTime.Now
            });
            alumnos.Add(new Client
            {
                Id = 2,
                Nombres = "Maria Saly",
                Apellidos = "Castil Rers",
                Edad = "5",
                Nivel = "Inicial",
                FechaRegistro = DateTime.Now
            });
            alumnos.Add(new Client
            {
                Id = 3,
                Nombres = "Pedro Hiew",
                Apellidos = "Torres Y",
                Edad = "11",
                Nivel = "Primaria",
                FechaRegistro = DateTime.Now
            });
            alumnos.Add(new Client
            {
                Id = 4,
                Nombres = "Luis Mari",
                Apellidos = "Loerw Junae",
                Edad = "12",
                Nivel = "Secundaria",
                FechaRegistro = DateTime.Now
            });
            alumnos.Add(new Client
            {
                Id = 5,
                Nombres = "Mazil Pro",
                Apellidos = "Chai Po",
                Edad = "13",
                Nivel = "Secundaria",
                FechaRegistro = DateTime.Now
            });


             context.Clients.AddRange(alumnos);
             context.SaveChanges();

            //foreach (alumnos s in alumnos)
            //{
            //    context.Students.Add(s);
            //}
            //context.SaveChanges();
            //context.Usuario.RemoveRange(context.Usuario);
            //context.SaveChanges();

            //List<Usuario> usuario = new List<Usuario>();

            //usuario.Add(new Usuario
            //{
            //    id = 1,
            //    usuario = "admin",
            //    contrasena = "admin",
            //    intentos = 1,
            //    nivelSeg = 1,
            //    fechaReg = DateTime.Now
            //});
            //context.Usuario.AddRange(usuario);
            //context.SaveChanges();
        }
    }
}
