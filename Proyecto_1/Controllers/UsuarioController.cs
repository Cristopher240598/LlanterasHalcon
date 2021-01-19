using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: Usuario
        public ActionResult Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                string correo = email;
                int rol = 0;


                using (db)
                {
                    var queryUsuario = from st in db.usuarios
                                        where st.correoElectronico == correo
                                        select st;
                    var listaUsuario = queryUsuario.ToList();
                    if (listaUsuario.Count > 0)
                    {
                        var usuario = queryUsuario.FirstOrDefault<usuarios>();
                        string[] nombres = usuario.nombre.ToString().Split(' ');
                        Session["name"] = nombres[0];
                        Session["usr"] = usuario.nombre;
                        Session["idUsuarioActual"] = usuario.Id;
                        rol = usuario.id_rol;
                    }
                }
                //1 = Admin CRUD empleados, paqueterías, proveedores, marcas
                //2 = Comprador CRUD categorias, subcategorias, llantas, compras, envíos
                //3 = Usuario

                if (rol == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (rol == 2)
                {
                    return RedirectToAction("Index", "Comprador");
                }
                else if (rol == 3)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}