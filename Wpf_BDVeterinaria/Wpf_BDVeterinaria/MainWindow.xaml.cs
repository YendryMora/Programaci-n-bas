using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_BDVeterinaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //instancia para conexión se conecta de una vez a la base de datos.
        clsConexion conex = new clsConexion();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Si el método devuelve un 0, quiere decir que no hay registros con ese id y se inserta en la BD.
            if (conex.consultar(int.Parse(txtId.Text)) == 0)
            {
                MessageBox.Show(conex.insertar(int.Parse(txtId.Text), txtNombre.Text));
                limpiar();
            }else
            {
                MessageBox.Show("ERROR: El registro ya existe");
            }

        }

        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            //Si el método devuelve un valor diferente de 0, quiere decir que hay registros con ese id.
            if (conex.consultar(int.Parse(txtId.Text)) != 0)
            {
                MessageBox.Show(conex.modificar(int.Parse(txtId.Text), txtNombre.Text));
                limpiar();
            }
            else
            {
                MessageBox.Show("ERROR: El registro no existe, no se puede modificar");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Si el método devuelve un valor diferente de 0, quiere decir que hay registros con ese id.
            if (conex.consultar(int.Parse(txtId.Text)) != 0)
            {
                MessageBox.Show(conex.eliminar(int.Parse(txtId.Text)));
                limpiar();
            }
            else
            {
                MessageBox.Show("El registro no existe, no se puede eliminar");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (conex.consultar(int.Parse(txtId.Text)) != 0)
            {
                //Se llena el campo de nombre con el resultado del método cargar.
                txtNombre.Text = conex.cargar(int.Parse(txtId.Text));
                
            }
            else
            {
                MessageBox.Show("No existe el registro consultado");  
            }

        }
        private void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
        }
    }
}
