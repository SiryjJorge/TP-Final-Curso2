using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {

        public string Nombre { get; set; }
        public MarcaDelProducto Marca { get; set; }
        public decimal Precio { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public int Id { get; set; }
        [DisplayName("Categoría")]
        public CategoriaDelProducto Categoria { get; set; }
        
        [DisplayName("Código")]
        public string Codigo { get; set; }             
        public string ImagenUrl { get; set; }
        
        
        
    }
}
