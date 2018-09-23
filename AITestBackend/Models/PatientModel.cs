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
        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public EthnicModel Ethnic { get; set; }

        public string IdParent { get; set; }

        public int Age { get; set; }

        public List<PatientTreatmentDeseaseModel> Treatments { get; set; }

        public List<AttachmentsModel> Attachments { get; set; }

        public static ResponsePatients GetAll(string parentId)
        {
            List<PatientModel> patients = null;

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAllPatients;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_id_parent", parentId);

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    patients = new List<PatientModel>();

                    while (rdr.Read())
                    {
                        patients.Add(new PatientModel()
                        {
                            Identification = rdr["id_patient"].ToString(),
                            Name = rdr["name"].ToString(),
                            BirthDate = Convert.ToDateTime(rdr["birth_date"]),
                            Age = Convert.ToInt32(rdr["age"]),
                            Ethnic = new EthnicModel()
                            {
                                Id = Convert.ToInt32(rdr["id_ethnic_group"]),
                                Name = rdr["ethnic_name"].ToString()
                            },
                            Gender = rdr["gender"].ToString()
                        });
                    }
                }
            }
            return new ResponsePatients { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllPatients_Success, Patients = patients };
        }

        public static ResponsePatient GetPatient(string identification)
        {
            PatientModel patient = null;

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetPatient;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_id_patient", identification);

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        patient = new PatientModel()
                        {
                            Identification = rdr["id_patient"].ToString(),
                            Name = rdr["name"].ToString(),
                            BirthDate = Convert.ToDateTime(rdr["birth_date"]),
                            Age = Convert.ToInt32(rdr["age"]),
                            Ethnic = new EthnicModel()
                            {
                                Id = Convert.ToInt32(rdr["id_ethnic_group"]),
                                Name = rdr["ethnic_name"].ToString()
                            },
                            Gender = rdr["gender"].ToString()
                        };
                    }
                }
            }

            if (patient.IsNotNull())
            {
                patient.Treatments = PatientTreatmentDeseaseModel.GetAllTreatmentDeseasest(patient.Identification).PatientTreatmentDeseases;

                //patient.Attachments = AttachmentsModel.GetAllAttachments(patient.Identification).Attachments;
            }

            return new ResponsePatient { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetPatient_Success, Patient = patient };
        }

        public static Response SavePatient(PatientModel patient)
        {
            MySqlTransaction tr = null;
            try
            {
                int rowAffected = 0;
                using (MySqlConnection conn = ConecctionModel.conn)
                {
                    conn.Open();

                    tr = conn.BeginTransaction();

                    string SP = AppManagement.SP_InsertPatients;
                    MySqlCommand cmd = new MySqlCommand(SP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_patient", patient.Identification);
                    cmd.Parameters.AddWithValue("@id_parent", patient.IdParent);
                    cmd.Parameters.AddWithValue("@name", patient.Name);
                    cmd.Parameters.AddWithValue("@birth_date", patient.BirthDate);
                    cmd.Parameters.AddWithValue("@age", patient.Age);
                    cmd.Parameters.AddWithValue("@gender", patient.Gender);
                    cmd.Parameters.AddWithValue("@id_ethnic_group", patient.Ethnic.Id);

                    rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected != 0)
                    {
                        if (patient.Treatments.IsNotNull())
                        {
                            foreach (var treatment in patient.Treatments)
                            {
                                if (!PatientTreatmentDeseaseModel.SaveTreatmentDeseases(treatment, conn).IsSuccessful)
                                {
                                    tr.Rollback();
                                    return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Failure };
                                }
                            }
                        }
                        tr.Commit();
                        return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveTreatment_Success };
                    }
                    else
                    {
                        tr.Rollback();
                        return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Failure };
                    }
                }
            }
            catch (Exception ex)
            {
                tr.Rollback();

                //if ()
                //{

                //}
                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Failure };
            }



        }
    }
}
