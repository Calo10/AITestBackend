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

        public static Response SaveTreatmentDeseases(ParentModel parentModel)
        {
            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                int idresult = 0;

                string SP = AppManagement.SP_InsertPatientsTreatmentDeseases;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_user", parentModel.Identification);
                cmd.Parameters.AddWithValue("@name", parentModel.Name);
                cmd.Parameters.AddWithValue("@password", parentModel.Password);
                cmd.Parameters.AddWithValue("@phone", parentModel.Mobile);
                cmd.Parameters.AddWithValue("@parent", 1);
                cmd.Parameters.AddWithValue("@email", parentModel.Email);

                MySqlParameter NroIdInvoice = new MySqlParameter("@result", idresult);
                NroIdInvoice.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(NroIdInvoice);

                cmd.ExecuteNonQuery();


                idresult = Int32.Parse(cmd.Parameters["@result"].Value.ToString());

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();

                if (idresult != 0)
                {
                    if (idresult == 1)//email duplicado
                        return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveParent_FailureEmail };
                    if (idresult == 2)//identificacion duplicada
                        return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveParent_FailureIdentification };

                    else
                        return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveParent_FailureDefault };
                }

                else
                    return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveParent_Success };
            }
        }



        #endregion
    }
}
