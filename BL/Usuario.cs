using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    using (SqlCommand cmd = new SqlCommand("AddUsuario", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);

                        cmd.Connection.Open();
                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo realizar el registro del usuario";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result UpdateUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateUsuario", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);

                        cmd.Connection.Open();
                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar los datos del usuario";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result DeleteUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteUsuario", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; 

                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                        cmd.Connection.Open();
                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo consultar la informacion del usuario con el id " + IdUsuario;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result GetAllUsuario()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllUsuario", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();
                            da.Fill(tableUsuario);

                            result.Objects = new List<object>();

                            if (tableUsuario.Rows.Count > 0)
                            {
                                foreach (DataRow row in tableUsuario.Rows)
                                {
                                    ML.Usuario usuario = new ML.Usuario();

                                    usuario.IdUsuario = int.Parse(row[0].ToString());
                                    usuario.Nombre = row[1].ToString();
                                    usuario.ApellidoPaterno = row[2].ToString();
                                    usuario.ApellidoMaterno = row[3].ToString();
                                    usuario.Edad = int.Parse(row[4].ToString());
                                    usuario.Email = row[5].ToString();
                                    usuario.Contraseña = row[6].ToString();

                                    result.Objects.Add(usuario);
                                }

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "La tabla Usuarios no contiene ningun registro";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result GetByIdUsuarios(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    string query = "GetByIdUsuario";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        DataTable usuarios = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(usuarios);

                        if (usuarios.Rows.Count > 0)
                        {
                            DataRow row = usuarios.Rows[0];

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.Edad = int.Parse(row[4].ToString());
                            usuario.Email = row[5].ToString();
                            usuario.Contraseña = row[6].ToString();

                            result.Object = usuario;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo realizar la consultar la informacion del registro con el Id .\" + IdUsuario";
                        }
                    }
                }                  
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result UsuarioGetByCorreo(ML.Usuario usuarioCP)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecion()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetByEmailUsuario", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", usuarioCP.Email);
                        cmd.Parameters.AddWithValue("@Contraseña", usuarioCP.Contraseña);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable TableUsuario = new DataTable();
                            da.Fill(TableUsuario);

                            if (TableUsuario.Rows.Count > 0)
                            {
                                DataRow row = TableUsuario.Rows[0];
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Contraseña = row[5].ToString();

                                result.Object = usuario;
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
