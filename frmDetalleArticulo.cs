using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TP_INTEGRADOR_SIRYJ
{
    public partial class frmDetalleArticulo : Form
    {
        private Articulo articulo = null;
        public frmDetalleArticulo()
        {
            InitializeComponent();
        }

        public frmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Ver Detalle";
        }

        private void frmDetalleArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();

            try
            {
                
                if (articulo != null)
                {
                    txtId.Text = articulo.Id.ToString();
                    txtDescripcion.Text = articulo.Descripcion;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.Precio.ToString();    
                    txtCodigo.Text = articulo.Codigo;
                    txtMarca.Text = articulo.Marca.Descripcion;
                    txtCategoria.Text = articulo.Categoria.Descripcion;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
