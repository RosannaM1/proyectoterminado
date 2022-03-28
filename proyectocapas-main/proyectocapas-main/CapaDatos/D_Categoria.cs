using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidades;
using System.Data;

namespace CapaDatos
{
    public class D_Categoria
    {
        //Objeto de la clase SqlConnection con el string conexion para abrir o cerrar la conexion
        SqlConnection conexion = new SqlConnection("Data Source=.;Initial Catalog=MANTEMIENTO_PRODUCTO;Integrated Security=True");

        //Metodo que me trae una lista de la busqueda
        public List<E_Categoria> ListarCategorias(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARCATEGORIA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            LeerFilas = cmd.ExecuteReader();

            List<E_Categoria> Listar = new List<E_Categoria>();
            E_Categoria categoria = null;
            while (LeerFilas.Read())
            {
                Listar.Add(categoria = new E_Categoria(LeerFilas.GetInt32(0),
                    LeerFilas.GetString(1), LeerFilas.GetString(2),
                    LeerFilas.GetString(3)));

            }
            conexion.Close();
            return Listar;
        }

        
        //Metodo para insertar una categoria
        public void insertarCategoria(E_Categoria categoria)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARCATEGORIA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@NOMBRE", categoria.Nombre);
            cmd.Parameters.AddWithValue("@DESCRIPCION", categoria.Descripcion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        //Metodo para editar una categoria
        public void editarCategoria(E_Categoria categoria)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand("SP_EDITARCATEGORIA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDCATEGORIA", categoria.IdCategoria);
            cmd.Parameters.AddWithValue("@NOMBRE", categoria.Nombre);
            cmd.Parameters.AddWithValue("@DESCRIPCION", categoria.Descripcion);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }
       //Metodo para eliminar una categoria
        public void eliminarCategoria(E_Categoria categoria)
        {
            SqlCommand cmd = new SqlCommand("SP_ELIMINARCATEGORIA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@IDCATEGORIA", categoria.IdCategoria);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        //Metodo que me devuelve un Objeto de tipo DataSet con todos los datos de la tabla CATEGORIA
        public DataSet MostrarDatos()
        {

            conexion.Open();
            string qwery = "select * from CATEGORIA";
            SqlDataAdapter adaptador = new SqlDataAdapter(qwery, conexion);
            DataSet datos = new DataSet();
            adaptador.Fill(datos);
            conexion.Close();
            return datos;

        }


    }
}
