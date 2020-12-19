using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfArchivoPlano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ruta;//variable global
        public int posi;
        public MainWindow()
        {
            InitializeComponent();
            limpiar();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            registrar();
        }

        //**************************MÉTODO PARA REGISTRAR***************************

        private void registrar()
        {
            string id, nombre, telefono;
            id = txtId.Text + "\t";
            nombre = txtNom.Text + "\t";
            telefono = txtTel.Text + "\t";

            string[] frase = { id, nombre, telefono, ruta };//ruta es una variable global
            if (!(buscar(txtId.Text)))
            {
                using (System.IO.StreamWriter archivo =
                    new System.IO.StreamWriter(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt", true))
                {
                    archivo.WriteLine(frase[0] + frase[1] + frase[2] + frase[3] + "\n");

                }
                MessageBox.Show("Registro insertado correctamente");
                limpiar();
            }else
            {
                MessageBox.Show("ERROR: EL ID YA SE ENCUENTRA REGISTRADO");
                limpiar();
            }

        }
//*****************************FIN DE REGISTRAR***************************************************
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //if (buscar(txtId.Text))
            //{
            //    btnModificar.IsEnabled = true;
            //    btnEliminar.IsEnabled = true;
            //    btnFoto.IsEnabled = true;
            //    btnRegistrar.IsEnabled = false;
            //}

            if (buscarLinq(txtId.Text))
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnFoto.IsEnabled = true;
                btnRegistrar.IsEnabled = false;
            }

        }

//**********************************MÉTODO BUSCAR*************************************************

        private bool buscar (string id)
        {
            string frase;
            bool encontro = false;
            int i = 0;

            System.IO.StreamReader archivo = new System.IO.StreamReader(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt");
            {//inicia a leer el archivo plano
                while ((frase = archivo.ReadLine()) != null)//recorre cada línea del archivo
                {
                    string[] valores = frase.Split('\t');//almacena cada dato en cada posición del vector.
                    if (valores[0].Contains(id))//si la id ingresada esta en el vector imprime el resto de datps en los campos.
                    {
                        txtNom.Text = valores[1];
                        txtTel.Text = valores[2];
                        imgFoto.Source = new BitmapImage(new Uri(valores[3]));//recuperar la ruta de la imagen
                        ruta = valores[3];
                        encontro = true;
                    }
                }
    
            }
            archivo.Close();
            return encontro; 
        }

        //*******************************************CONSULTA CON LINQ**********************************

        private bool buscarLinq(string id)
        {
            bool encontro = false;
            string ruta = @"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt";

            List<string> texto = (from valor in File.ReadAllLines(ruta)
                                  where valor.Contains(id) select valor).ToList();

            foreach (string valor in texto)
            {
                string[] vector = valor.Split('\t');
                
                if(vector[0].Contains(id))
                {
                    txtNom.Text = vector[1];
                    txtTel.Text = vector[2];
                    imgFoto.Source = new BitmapImage(new Uri(vector[3]));
                    encontro = true;
                }
            }
            return encontro;
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//abre un cuadro de diálogo para seleccionar imágenes
            openFileDialog1.Filter = "Mis imagenes(.jpg)|*.jpg|All Files(*.*)|*.*";//titulo del cuadro de diálogo.
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;//no permite seleccionar más de una imágen. Si se quiere seleccionar varias se coloca true.

            bool? respuesta = openFileDialog1.ShowDialog();//respuesta de interección con el usuario
            if (respuesta == true)
            {
                imgFoto.Source = new BitmapImage(new Uri(openFileDialog1.FileName));//se obtiene la ruta de la imagen.
                ruta = openFileDialog1.FileName;//asigna la ruta a la variable global ruta.

            }
        }

        private void btnDer_Click(object sender, RoutedEventArgs e)
        {
            posi -= 90;
            imgFoto.LayoutTransform = new RotateTransform(posi);//rota 90 grados
        }

        private void btnIzq_Click(object sender, RoutedEventArgs e)
        {
            posi += 90;
            imgFoto.LayoutTransform = new RotateTransform(posi);//rota 90 grados
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modificar(txtId.Text, txtNom.Text, txtTel.Text, ruta);

        }

//*********************************MÉTODO MODIFICAR*************************************************

        public void modificar (string id, string nombre, string tel, string ruta)
        {
            string frase;
            int i = 0;


            System.IO.StreamReader archivo = new System.IO.StreamReader(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt");
            System.IO.StreamWriter archivo2 = new System.IO.StreamWriter(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\respaldo.txt", true);
            {//inicia a leer el archivo plano
                while ((frase = archivo.ReadLine()) != null)//recorre cada línea del archivo
                {
                    string[] valores = frase.Split('\t');//almacena cada dato en cada posición del vector.
                    if (valores[0].Contains(id))//si la id ingresada esta en el vector imprime el resto de datps en los campos.
                    {
                        archivo2.WriteLine(id + "\t" + nombre + "\t" + tel + "\t" + ruta + "\n");//escribe la linea con los cambio del formulario
                    }else
                    {
                        archivo2.WriteLine(frase);//escribe la linea igual
                    }
                }
            }
            archivo.Close();
            archivo.Dispose();
            archivo2.Close();
            archivo2.Dispose();
            File.Delete(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt");
            File.Copy(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\respaldo.txt", @"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\prueba.txt");
            File.Delete(@"F:\UTN\I-2017\Progra_III\WpfArchivoPlano\respaldo.txt");

            limpiar();
            //File.Create(@"D:\pr.txt");
        }
        //********************************METODO LIMPIAR************************************************

        public void limpiar()
        {
            txtId.Text = "";
            txtTel.Text = "";
            txtNom.Text = "";
            imgFoto.Source = null;
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnRegistrar.IsEnabled = true;

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            eliminar(txtId.Text);
        }

//*******************************MÉTODO ELIMINAR**************************************************
        private void eliminar (string id)
        {
            string frase;
            int i = 0;

            System.IO.StreamReader archivo = new System.IO.StreamReader(@"D:\pr.txt");
            System.IO.StreamWriter archivo2 = new System.IO.StreamWriter(@"D:\back.txt", true);
            {//inicia a leer el archivo plano
                while ((frase = archivo.ReadLine()) != null)//recorre cada línea del archivo
                {
                    string[] valores = frase.Split('\t');//almacena cada dato en cada posición del vector.
                    if (!valores[0].Contains(id))//si la id ingresada NO esta en el vector, guarda la la línea en el respaldo.
                    {
                        archivo2.WriteLine(valores[0] + "\t" + valores[1] + "\t" + valores[2] + "\t" + valores[3] + "\n");
                    }

                }
            }
            archivo.Close();
            archivo.Dispose();
            archivo2.Close();
            archivo2.Dispose();
            File.Delete(@"D:\pr.txt");
            File.Copy(@"D:\back.txt", @"D:\pr.txt");
            File.Delete(@"D:\back.txt");
            //File.Create(@"D:\pr.txt");

            limpiar();
        }


    }
//*************************************************************************************
    public class datos
    {
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string ruta { get; set; }
    }
}
