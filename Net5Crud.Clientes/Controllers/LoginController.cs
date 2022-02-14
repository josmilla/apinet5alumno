using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Net5Crud.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Net5Crud.Clientes.Controllers
{

    public class LoginController : Controller
    {
        /// <summary>
        /// Constante para Inicializar la Sesión _User
        /// </summary>
        const string SessionUser = "_User";
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Interfaz para acceder a los valores del archivo de configuración
        /// </summary>
        /// <param name="configuration"></param>
        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public ActionResult Login(string returnUrl)
        /// <summary>
        /// Action para inicializar la carga de la vista del Login en base a los atributos de modelo usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View(new ClsUsuario());
        }

        /// <summary>
        /// Action de tipo POST para  para inicializar el proceso de validación e iniciar sessión en  base a los datos del modelo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(ClsUsuario model)
        {
            //Conexión a la base de datos
            string connectionString = Configuration["ConnectionStrings:Default"];
            //Estoy usando uso de ADO.Net para interactuar con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var list_users = new List<ClsUsuario>();
                //@1Inicio: Validar los controladores
                if (model.usuario == null || model.usuario.Equals("") ||
                    model.contrasena == null || model.contrasena.Equals(""))
                {
                    ModelState.AddModelError("", "Ingresar los datos solictiados");
                }//@1Final
                else
                {
                    connection.Open();//Abrir la conexión a la base de datos
                    SqlCommand com = new SqlCommand("GET_SEG_USUARIO", connection);//Referencia al procedimiento almacenado
                    com.CommandType = CommandType.StoredProcedure;//Se define el tipo de comando a utilizar
                    //Paso los parámetros de acuerdo a los datos cargados segun el modelo usario
                    com.Parameters.AddWithValue("@usuario", model.usuario);
                    com.Parameters.AddWithValue("@contrasena", model.contrasena);
                    SqlDataReader dr = com.ExecuteReader();//Ejecuto el comando a través de un DataReader
                    //@2Inicio: Recorro los datos y adiciono en la lista list_users los valores usuario y contrasena
                    while (dr.Read())
                    {
                        ClsUsuario clsUsuario = new ClsUsuario();
                        clsUsuario.usuario = Convert.ToString(dr["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dr["Contrasena"]);

                        list_users.Add(clsUsuario);//Adicionar en la lista
                    }
                    //@2Final 
                    //HttpContext.Session.Clear();
                    //@3Inicio: Match entre los valores ingresados y la lista
                    if (list_users.Any(p => p.usuario == model.usuario && p.contrasena == model.contrasena))
                    {
                        //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.usuario), });
                         HttpContext.Session.SetString(SessionUser, model.usuario);
                        //Iniciamos la sesión pasando el valor (nombre del usuario)
                       // HttpContext.Session.Clear();
                        return RedirectToAction("Index", "Clients");//Redireccionar a la vista usario (Lista de Usuarios)
                    }
                    else
                    {
                        ModelState.AddModelError("", "Datos ingresado no válido.");//Error personalizado
                    }
                }
                return View(model);
            }
        }

        /// <summary>
        /// Action para limpiar y cerrar la sesión de la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();//Limpiar la sesión
            return RedirectToAction("Login", "Login");//Redireccionar a la vista login
        }
    }
}
