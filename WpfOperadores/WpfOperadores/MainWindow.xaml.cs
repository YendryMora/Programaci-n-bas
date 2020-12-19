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

namespace WpfOperadores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)rbtSumar.IsChecked)
                {
                    sumar();
                }
                else if ((bool)rbtRestar.IsChecked)
                {
                    restar();
                }
                else if ((bool)rbtMultiplicar.IsChecked)
                {
                    multiplicar();
                }
                else if ((bool)rbtDividir.IsChecked)
                {
                    dividir();
                }
                else if ((bool)rbtResiduo.IsChecked)
                {
                    residuo();
                }
            }

            catch (Exception error)
            {
                txtExpresion.Text = " "; //le asigna un espacio en blanco al textBlock expresión.
                txtResultado.Text = error.Message; //se le asigna el contenido del mensaje de error.
            }
        }

        //******************************** METODOS ***************************

        private void sumar()
        {
            int oper1 = int.Parse(txtValor1.Text);//Convierte los valores ingresados a enteros.
            int oper2 = int.Parse(txtValor2.Text);
            int salida = 0;
            salida = oper1 + oper2;

            txtExpresion.Text = txtValor1.Text + " + " + txtValor2.Text;
            txtResultado.Text = salida.ToString();//convierte el entero de salida a string.
        }

        private void restar()
        {
            int oper1 = int.Parse(txtValor1.Text);//Convierte los valores ingresados a enteros.
            int oper2 = int.Parse(txtValor2.Text);
            int salida = 0;
            salida = oper1 - oper2;

            txtExpresion.Text = txtValor1.Text + " - " + txtValor2.Text;
            txtResultado.Text = salida.ToString();//convierte el entero de salida a string.
        }

        private void multiplicar()
        {
            int oper1 = int.Parse(txtValor1.Text);//Convierte los valores ingresados a enteros.
            int oper2 = int.Parse(txtValor2.Text);
            int salida = 0;
            salida = oper1 * oper2;

            txtExpresion.Text = txtValor1.Text + " * " + txtValor2.Text;
            txtResultado.Text = salida.ToString();//convierte el entero de salida a string.
        }

        private void dividir()
        {
            int oper1 = int.Parse(txtValor1.Text);//Convierte los valores ingresados a enteros.
            int oper2 = int.Parse(txtValor2.Text);
            int salida = 0;
            salida = oper1 / oper2;

            txtExpresion.Text = txtValor1.Text + " / " + txtValor2.Text;
            txtResultado.Text = salida.ToString();//convierte el entero de salida a string.
        }

        private void residuo()
        {
            int oper1 = int.Parse(txtValor1.Text);//Convierte los valores ingresados a enteros.
            int oper2 = int.Parse(txtValor2.Text);
            int salida = 0;

            salida = oper1 / oper2;
            string result = salida.ToString();

            while (salida >= oper2)
            {
                salida = salida / oper2;
                result = result + salida;
            }
            txtExpresion.Text = txtValor1.Text + " % " + txtValor2.Text;
            txtResultado.Text = result;
        }
    }

    
}
