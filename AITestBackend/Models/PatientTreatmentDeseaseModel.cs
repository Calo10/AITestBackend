using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class PatientTreatmentDeseaseModel
    {
        public string Identification { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }

        #region PatientTreatmentDesease

        public static Response SaveTreatmentDeseases(PatientTreatmentDeseaseModel patienttreatmentdesease, MySqlConnection conn = null)
        {
            if (conn != null)
            {
                conn = ConecctionModel.conn;

                string SP = AppManagement.SP_InsertPatientsTreatmentDeseases;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@identification", patienttreatmentdesease.Identification);
                cmd.Parameters.AddWithValue("@type", patienttreatmentdesease.Type);
                cmd.Parameters.AddWithValue("@Description", patienttreatmentdesease.Description);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();

                if (rdr.RecordsAffected != 0)
                    return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveTreatment_Success };

                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Failure };
            }
            else
            {
                using (conn = ConecctionModel.conn)
                {
                    conn.Open();

                    string SP = AppManagement.SP_InsertPatientsTreatmentDeseases;
                    MySqlCommand cmd = new MySqlCommand(SP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@identification", patienttreatmentdesease.Identification);
                    cmd.Parameters.AddWithValue("@type", patienttreatmentdesease.Type);
                    cmd.Parameters.AddWithValue("@Description", patienttreatmentdesease.Description);

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    rdr.Close();

                    if (rdr.RecordsAffected != 0)
                        return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveTreatment_Success };

                    return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Failure };

                }
            }

            
        }

        public static PatientTreatmentDeseaseList GetAllTreatmentDeseasest(string identification)
        {
            List<PatientTreatmentDeseaseModel> treatments = new List<PatientTreatmentDeseaseModel>();

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAllTreatmentDeseases;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@identification", identification);


                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PatientTreatmentDeseaseModel treatment = new PatientTreatmentDeseaseModel();
                    treatment.Identification = rdr["id_patient"].ToString();
                    treatment.Type = Convert.ToInt32(rdr["type"]);
                    treatment.Description = rdr["description"].ToString();

                    treatments.Add(treatment);
                }

                rdr.Close();

                return new PatientTreatmentDeseaseList { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllTreatments_Success, PatientTreatmentDeseases = treatments };
            }
        }

        #endregion
    }
}
