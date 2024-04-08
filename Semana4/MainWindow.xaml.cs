using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Semana4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public class Proveedor
        {
            public int idProveedor { get; set; }
            public string nombreCompañia { get; set; }
            public string nombreContacto { get; set; }
            public string cargocontacto { get; set; }
            public string direccion { get; set; }
        }

        public class Categoria
        {
            public int idcategoria { get; set; }
            public string nombrecategoria { get; set; }
            public string descripcion { get; set; }
            public bool Activo { get; set; }
            public string CodCategoria { get; set; }
        }
        private string connectionString = "Data Source=DAVILA-FERNANDO\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=Davila;Password=Davila12";
        private DataTable dataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            CargarDatos();
            CargarCategorias();
        }

        private void CargarDatos()
        {
            try
            {
                List<Proveedor> proveedores = new List<Proveedor>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ListarProveedores", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int idProveedor = reader.GetInt32(reader.GetOrdinal("idProveedor"));
                        string nombreCompañia = reader.GetString(reader.GetOrdinal("nombreCompañia"));
                        string nombreContacto = reader.GetString(reader.GetOrdinal("nombreContacto"));
                        string cargocontacto = reader.GetString(reader.GetOrdinal("cargocontacto"));
                        string direccion = reader.GetString(reader.GetOrdinal("direccion"));
                        proveedores.Add(new Proveedor { idProveedor = idProveedor, nombreCompañia = nombreCompañia, nombreContacto = nombreContacto, cargocontacto = cargocontacto, direccion = direccion });
                    }
                    connection.Close();
                }
                dataGridProveedores.ItemsSource = proveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                List<Categoria> categorias = new List<Categoria>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ListarCategorias", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int idcategoria = reader.GetInt32(reader.GetOrdinal("idcategoria"));
                        string nombrecategoria = reader.GetString(reader.GetOrdinal("nombrecategoria"));
                        string descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                        bool Activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
                        string CodCategoria = reader.GetString(reader.GetOrdinal("CodCategoria"));
                        categorias.Add(new Categoria { idcategoria = idcategoria, nombrecategoria = nombrecategoria, descripcion = descripcion, Activo = Activo, CodCategoria = CodCategoria });
                    }
                    connection.Close();
                }
                dataGridCategorias.ItemsSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
