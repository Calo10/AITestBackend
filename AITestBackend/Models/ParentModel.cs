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
    }
}
