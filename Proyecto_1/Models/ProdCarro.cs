using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1.Models
{
    public class ProdCarro
    {

        private contextLlantera db = new contextLlantera();
        private List<llantas> llantas;
        public ProdCarro()
        {
            llantas = db.llantas.ToList();
        }

        public List<llantas> findAll()
        {
            return this.llantas;
        }

        public llantas find(int id)
        {
            llantas pp;
            pp= this.llantas.Single(p => p.Id.Equals(id));
            return pp;

        }
    }
}