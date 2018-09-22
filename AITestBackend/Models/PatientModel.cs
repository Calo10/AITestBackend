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
                            Identification = rdr["id_patient"].ToString(),
                            Name = rdr["name"].ToString(),
                            BirthDate = Convert.ToDateTime(rdr["birth_date"]),
                            Ethnic =  new EthnicModel()
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

        public static ResponsePatients SavePatient(PatientModel patient)
        {
            throw new NotImplementedException();
        }
    }
}
