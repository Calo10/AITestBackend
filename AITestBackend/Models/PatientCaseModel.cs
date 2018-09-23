using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class PatientCaseModel
    {
        public DateTime RegistrationDate { get; set; }

        public string Id_Patient { get; set; }

        public string Id_Parent { get; set; }

        public string History { get; set; }

        public string Procesed_Data { get; set; }

        public static ResponsePatientCases GetAllPatientCases(string patientId)
        {
            List<PatientCaseModel> cases = null;

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAllPatientCases;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pidentification", patientId);

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    cases = new List<PatientCaseModel>();

                    while (rdr.Read())
                    {
                        cases.Add(new PatientCaseModel()
                        {
                            Id_Parent = rdr["id_user"].ToString(),
                            Id_Patient = rdr["id_patient"].ToString(),
                            History = rdr["history"].ToString(),
                            Procesed_Data = rdr["processed_data_nlu"].ToString(),
                            RegistrationDate = Convert.ToDateTime(rdr["registration_date"])
                            
                        });
                    }
                }
            }
            return new ResponsePatientCases { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllPatients_Success, PatientCases = cases };
        }
                
    }
}
