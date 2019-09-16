using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public bool Dolar { get; set; }
        public double Iva { get; set; }
        public bool Estado { get; set; }
    }
}
