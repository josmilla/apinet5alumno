using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Net5Crud.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using HealthChecks.UI.Client;
using System.Linq;
using System.Threading.Tasks;

namespace Net5Crud.Clientes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string SessionUser = "_User";
        //public IConfiguration Configuration { get; }
        public IConfiguration Configuration { get; }
        public HomeController(ILogger<HomeController> logger , IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View(new ClsUsuario());
        }
        public IActionResult Index()
        {
            List<ClsUsuario> UsuariosList = new List<ClsUsuario>();

            

            string connectionString = Configuration["ConnectionStrings:Default"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = "select * from USUARIO";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ClsUsuario clsUsuario = new ClsUsuario();
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.usuario = Convert.ToString(dataReader["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["Contrasena"]);
                        clsUsuario.intentos = Convert.ToInt32(dataReader["Intentos"]);
                        clsUsuario.nivelSeg = Convert.ToDecimal(dataReader["NivelSeg"]);
                        clsUsuario.fechaReg = Convert.ToDateTime(dataReader["FechaReg"]);

                        UsuariosList.Add(clsUsuario);
                    }
                }

                connection.Close();
            }
            return View(UsuariosList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Usuario()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();//Limpiar la sesión
            return RedirectToAction("Login", "Login");//Redireccionar a la vista login
        }

    }
}
