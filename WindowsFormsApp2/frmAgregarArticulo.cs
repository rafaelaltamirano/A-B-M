using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace WindowsFormsApp2
{
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;

        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        //hago el mismo metodo que arriba pero este recive un articulo por parametro
        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // creo obj negocioNegocio y lo instancio, xq lo voy a necesitar para llamar al metodo de alta
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            // creo obj articulo
            //Articulo articulo = new Articulo();
            try
            {
                // si el articulo ya viene precargado no hago nada de lo contrario creo uno nuevo
                if (articulo == null)

                    articulo = new Articulo();
                articulo.Modelo = txtNombre.Text;
                //como es un objeto, recibe un ojeto de el combo box osea el seleccionado
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;       
                articulo.Descripcion = txtDescripcion.Text;               
                articulo.Precio = Convert.ToDouble(txtVenta.Text);
              //FALTA CONSEGUIR LA RUTA ANTERIOR DEL PICBOX
                articulo.Imagen = picBoxImg.ImageLocation;

                articulo.Costo = Convert.ToDouble(txtCosto.Text);
                articulo.Iva = Convert.ToDouble(txtIva.Text);           
                articulo.Dolar = Convert.ToBoolean(cboxDolares.Checked);
                articulo.Estado = true;

                // si el articulo .id es distinto de 0 significa que ya existe por lo tanto 
                //mando todo a la funcion modificar
                if (articulo.Id != 0) articuloNegocio.modificar(articulo);
                //de lo contrario mando todo  al metodo cargar
                else articuloNegocio.agregar(articulo);
                Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
             //declaro objeto negocio de tipo articuloNegocio 
            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            try
            {
               // listo el combo box            
                cboCategoria.DataSource = catNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Nombre";
                cboCategoria.SelectedIndex = -1;

                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Nombre";
                cboMarca.SelectedIndex = -1;

                // si el articulo viene con datos previos
                if(articulo !=null)
                {
                    //precargo dichos datos
                    Text = "Modificar";
                    txtNombre.Text = articulo.Modelo;
                    cboCategoria.SelectedValue =articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca;
                    txtDescripcion.Text = articulo.Descripcion;
                    picBoxImg.Image = Image.FromFile(articulo.Imagen);
                    txtVenta.Text = Convert.ToString(articulo.Precio);
                    txtCosto.Text = Convert.ToString(articulo.Costo);
                    txtIva.Text = Convert.ToString(articulo.Iva);
                    if (articulo.Dolar == true) cboxDolares.Checked = true;
                    else cboxDolares.Checked = false;
     
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // condicion que valida que el ingreso sea solo numerico
            if((e.KeyChar < 48 || e.KeyChar > 59 ) && e.KeyChar !=8)
            {
                e.Handled = true;
            }

        }


        private void btnAdjuntar_Click(object sender, EventArgs e)
        {

            try
            {
                // comando para imagen
                // tipo de archivo que abre el explorador
                OpenFileDialog adjuntar = new OpenFileDialog();
                adjuntar.InitialDirectory = "c:\\";
                adjuntar.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (adjuntar.ShowDialog() == DialogResult.OK)
                {
                    picBoxImg.ImageLocation = adjuntar.FileName;// para mostrar imagen predeterminada 
                }
                else
                {
                    MessageBox.Show("No se seleccionó imagen.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

      
        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // condicion que valida que el ingreso sea numerico
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // condicion que valida que permite la tecla back space
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // condicion que verigica si hay decimal
            else if ((e.KeyChar == '.') && (!txtVenta.Text.Contains(".")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
   
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void picBoxImg_Click(object sender, EventArgs e)
        {

        }
    }
}
