using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//librerias requeridas
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Wpf_BDVeterinaria
{
    class clsConexion
    {
        //variable para la conexión.
        SqlConnection sqlconex;
        //contiene la instrucción que se le envía a la BD.
        SqlCommand inst;
        //variable para leer registros.
        SqlDataReader rsregis;

        private string nom;

        public clsConexion()
        {
            try
            {
                sqlconex = new SqlConnection("Data Source =.; Initial Catalog = Veterinaria; Integrated Security=True");//Se eliminan el usuario y contraseña
                sqlconex.Open();
                MessageBox.Show("Conexión exitosa");
            }
            catch (Exception error)
            {
                MessageBox.Show("No se pudo realizar la conexión "+error.ToString());
            }
           
        }

        public string insertar (int id, string nombre)
        {
            string mensaje = "Registro insertado satisfactoriamente";
            try
            {
                //instrucción para insertar en la BD
                inst = new SqlCommand("insert into veterinario(codVete, nomVete) values(" + id + ",'" + nombre + "')", sqlconex);
                inst.ExecuteNonQuery();//ejecución de una acción sobre la base de datos (inserciones, modificaciones o eliminar).
            }
            catch (Exception error)
            {
                mensaje = "No se realizó la inserción " + error.ToString();
            }
            return mensaje;
        }

        public int consultar(int id)
        {
            int contador = 0;
            try
            {
                inst = new SqlCommand("select*from Veterinario where codVete=" + id + "", sqlconex);
                rsregis = inst.ExecuteReader();//Para ejecutar consultas
                while (rsregis.Read())//recorre uno por uno los registros que hay en la tabla
                {
                    contador++;//cuenta cuantos registros existen con el id que estamos buscadno
                }
                rsregis.Close();//cierra la consulta.
            }
            catch(Exception error) 
            {
                MessageBox.Show("No se logró realizar la consulta " + error.ToString());
            }
            return contador;
        }

        public string cargar(int id)//Carga los datos en el formulario cuando encuentra un registro. Es string porque solo se ocupa que devuelva el nombre.
        {
            inst = new SqlCommand("select*from Veterinario where codVete=" + id + "", sqlconex);
            rsregis = inst.ExecuteReader();
            if (rsregis.Read() == true)
            {
                nom = rsregis["nomVete"].ToString();
            }
            rsregis.Close();//Cierra la consulta.
            return nom;
        }

        public string modificar(int id, string nombre)
        {
            string mensaje = "Registro modificado satisfactoriamente";
            try
            {
                //instrucción para modificar en la BD
                inst = new SqlCommand("update Veterinario set nomVete='" + nombre + "' where codVete=" + id + "", sqlconex);
                inst.ExecuteNonQuery();//ejecución de una acción sobre la base de datos (inserciones, modificaciones o eliminar).
            }
            catch (Exception error)
            {
                mensaje = "No se realizó la modificación " + error.ToString();
            }
            return mensaje;
        }

        public string eliminar(int id)
        {
            string mensaje = "Registro eliminado satisfactoriamente";
            try
            {
                //instrucción para eliminar en la BD
                inst = new SqlCommand("delete from Veterinario where codVete="+id+"", sqlconex);
                inst.ExecuteNonQuery();//ejecución de una acción sobre la base de datos (inserciones, modificaciones o eliminar).
            }
            catch (Exception error)
            {
                mensaje = "No se pudo eliminar " + error.ToString();
            }
            return mensaje;
        }
    }
}
