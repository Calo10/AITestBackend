using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class ParentModel : PersonModel
    {

        public string password { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }

        public static ResponseParent GetParent(string identification)
        {
            ParentModel parentModel = new ParentModel();

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetParent;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@identification", identification);


                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parentModel.identification = rdr["idCurrency"].ToString();
                    parentModel.name = rdr["Code"].ToString();
                    parentModel.lastname1 = rdr["Country"].ToString();
                    parentModel.lastname2 = rdr["Description"].ToString();
                }

                rdr.Close();

                return new ResponseParent { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetParent_Success, Parent = parentModel };
            }
        }


        #region Login

        public static Response Login(ParentModel parent)
        {
            try
            {
                using (MySqlConnection conn = ConecctionModel.conn)
                {
                    conn.Open();
                    int idresult = 0;

                    string SP = AppManagement.SP_LoginUser;

                    MySqlCommand cmd = new MySqlCommand(SP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@identification", parent.identification);
                    cmd.Parameters.AddWithValue("@password", parent.password);


                    MySqlParameter NroIdInvoice = new MySqlParameter("@result", idresult);
                    NroIdInvoice.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(NroIdInvoice);

                    cmd.ExecuteNonQuery();


                    idresult = Int32.Parse(cmd.Parameters["@result"].Value.ToString());
                    if (idresult != 0)
                    {
                        if (idresult == 1)//usuario no existe
                            return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_Login_Failure1 };
                        if (idresult == 2)//contraseña incorrecta
                            return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_Login_Failure2 };
                        else
                            return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_Login_FailureDefault };
                    }

                    else
                        return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_Login_Success };
                }
            }
            catch (Exception)
            {
                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_Login_FailureDefault };
            }

        }

        #endregion
    }
}
