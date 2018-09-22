using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AITestBackend.Models
{
    public class EthnicModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static ResponseEthnicGroup GetAll()
        {
            List<EthnicModel> ethnics = null;

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAllEthnicGroup;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    ethnics = new List<EthnicModel>();

                    while (rdr.Read())
                    {
                        ethnics.Add(new EthnicModel()
                        {
                            Id = Convert.ToInt32(rdr["id_ethnic_group"]),
                            Name = rdr["name"].ToString()
                        });
                    }
                }

                return new ResponseEthnicGroup { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllEthnicGroup_Success, Ethnics = ethnics };
            }
        }
    }
}
