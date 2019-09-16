using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WindowsFormsApp2
{
    public partial class frmInicio : Form
    {
        private List<Articulo> lista;

        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            // declaro el objeto de tipo ArticuloNegocio
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                lista = negocio.listar();

                // muestro el origen de datos del listado de articulo osea el listar
                dgvListadoArticulos.DataSource = negocio.listar();
                dgvListadoArticulos.Columns[0].Visible = false;
                 dgvListadoArticulos.Columns[5].Visible = false;
                dgvListadoArticulos.Columns[8].Visible = false;
                dgvListadoArticulos.Columns[9].Visible = false;
                dgvListadoArticulos.Columns[10].Visible = false;
            }
            catch (Exception ex)
            {
                // muestro un mensaje de error de la excepcion
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();//la aplicacion se queda parada en el formulario de alta, una vez cerrado se actualiza
            cargarDatos();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            try
            {
                // que tome el id del articulo que estoy seleccionando ahora
                int id = ((Articulo)dgvListadoArticulos.CurrentRow.DataBoundItem).Id;
                //elimino el articulo con el id que seleccione arriba
                articulo.eliminar(id);
                //refresca la pagina
                cargarDatos();
            }
            catch (Exception)
            {                       
                throw;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;

            if (txtBuscar.Text == "")
            {
                listaFiltrada = lista;
            }
            else
            {
                listaFiltrada = lista.FindAll(k => k.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()) || k.Modelo.ToLower().Contains(txtBuscar.Text.ToLower()) || k.Marca.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()) || k.Categoria.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()));
            }
            dgvListadoArticulos.DataSource = listaFiltrada;

        }

        private void dgvListadoArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            // CRO UN OBJETO DE TIPO ARTICULO LLAMADO MODIFICAR
            Articulo modificar;
            // LE ASIGNO EL CONTENIDO DE LA FILA QUE TENGO MARCADA 
            modificar = (Articulo)dgvListadoArticulos.CurrentRow.DataBoundItem;
            frmAgregarArticulo frmModificar = new frmAgregarArticulo(modificar);
            frmModificar.ShowDialog();
            cargarDatos();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
