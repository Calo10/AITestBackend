using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class PatientHistoryModel
    {
        public int IdHistory { get; set; }
        public string Identification { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
        public DateTime Date { get; set; }

        #region PatientHistory

        public static Response SaveAttachment(PatientHistoryModel history)
        {
            try
            {
                using (MySqlConnection conn = ConecctionModel.conn)
                {
                    conn.Open();
                    int idresult = 0;

                    string SP = AppManagement.SP_Save_Patient_Attachments;

                    MySqlCommand cmd = new MySqlCommand(SP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pidentification", history.Identification);
                    cmd.Parameters.AddWithValue("@pnote", history.Note);
                    cmd.Parameters.AddWithValue("@pphoto", history.Photo);

                    cmd.ExecuteNonQuery();


                    return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveAttachment_Success };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveAttachment_FailureDefault };
            }

        }

        public static ResponseHistoriesList GetAllHistories(PatientHistoryModel history)
        {
            List<PatientHistoryModel> histories = new List<PatientHistoryModel>();

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetAll_Histories;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pidentification", history.Identification);
                cmd.Parameters.AddWithValue("@pDate", history.Date);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PatientHistoryModel _history = new PatientHistoryModel();
                    _history.IdHistory =  Convert.ToInt32(rdr["id_history"].ToString());
                    _history.Identification = rdr["id_patient"].ToString();
                    _history.Note = rdr["note"].ToString();
                    if (!(rdr["date"] is DBNull))
                        _history.Date = Convert.ToDateTime(rdr["date"]);
                    _history.Photo = rdr["photo"].ToString();

                    histories.Add(_history);
                }

                rdr.Close();

                return new ResponseHistoriesList { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllHistories_Success, ResponseHistories = histories };
            }
        }

        //public static Response DeleteAttachment(AttachmentsModel attachment)
        //{
        //    using (MySqlConnection conn = ConecctionModel.conn)
        //    {
        //        conn.Open();

        //        string SP = AppManagement.SP_Delete_Attachments;
        //        MySqlCommand cmd = new MySqlCommand(SP, conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@pidentification", attachment.Identification);
        //        cmd.Parameters.AddWithValue("@pfilename", attachment.FileName);


        //        MySqlDataReader rdr = cmd.ExecuteReader();

        //        rdr.Close();

        //        if (rdr.RecordsAffected != 0)
        //            return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_DeleteAttachment_Success };

        //        return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_DeleteAttachment_Failure };
        //    }
        //}

        #endregion
    }
}
