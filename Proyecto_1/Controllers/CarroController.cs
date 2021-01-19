using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;


namespace Proyecto_1.Controllers
{
    public class CarroController : Controller
    {
        // GET: Carro
        public ActionResult Index()
        {

            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult Agregar(int id)
        {

            ProdCarro carro = new ProdCarro();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                llantas l = carro.find(id);
                String mod = l.modelo;
                cart.Add(new Item { llantas = carro.find(id), cantidad = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if(index != -1)
                {
                    cart[index].cantidad++;
                }
                else
                {
                    llantas l = carro.find(id);
                    String mod = l.modelo;
                    cart.Add(new Item { llantas = carro.find(id), cantidad =1});
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> carro = (List<Item>)Session["cart"];
            for (int i = 0; i < carro.Count; i++)
                if (carro[i].llantas.Id.Equals(id))
                    return i;
            return -1;
        }
        public ActionResult quitar(int id)
        {
            List<Item> cart= (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }

   
}