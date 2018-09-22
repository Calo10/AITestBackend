using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class PatientModel : PersonModel
    {
        public static ResponsePatients GetAll()
        {
            List<PatientModel> patients = null;

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAllEthnicGroup;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    patients = new List<PatientModel>();

                    while (rdr.Read())
                    {
                        patients.Add(new PatientModel()
                        {
                            
                        });
                    }
                }
            }
            return new ResponsePatients { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllPatients_Success, Patients = patients };
        }
    }
}
