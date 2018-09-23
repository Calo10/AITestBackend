using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class AttachmentsModel
    {
        public string Identification { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }

        #region Attachaments

        public static Response SaveAttachment(AttachmentsModel attachments)
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

                    cmd.Parameters.AddWithValue("@pidentification", attachments.Identification);
                    cmd.Parameters.AddWithValue("@pfilename", attachments.FileName);
                    cmd.Parameters.AddWithValue("@pcontent", attachments.Content);


                    MySqlParameter NroIdInvoice = new MySqlParameter("@result", idresult);
                    NroIdInvoice.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(NroIdInvoice);

                    cmd.ExecuteNonQuery();


                    idresult = Int32.Parse(cmd.Parameters["@result"].Value.ToString());
                    if (idresult != 0)
                    {
                        if (idresult == 1)//mas de 20 archivos 
                            return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveAttachment_Failure20 };
                        
                        else
                            return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveAttachment_FailureDefault };
                    }

                    else
                        return new Response { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SaveAttachment_Success };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveAttachment_FailureDefault };
            }

        }

<<<<<<< HEAD
        public static ResponseAttachmentsList GetAllAttachments(string identification)
        {
            List<AttachmentsModel> attachments = new List<AttachmentsModel>();

            using (MySqlConnection conn = ConecctionModel.conn)
            {
                conn.Open();
                string SP = AppManagement.SP_GetParent;
                MySqlCommand cmd = new MySqlCommand(SP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pidentification", identification);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AttachmentsModel attachment = new AttachmentsModel();
                    attachment.Identification = rdr["id_patient"].ToString();
                    attachment.FileName = rdr["file_name"].ToString();
                    attachment.Content = rdr["content"].ToString();

                    attachments.Add(attachment);
                }
=======
        public static ResponseAttachments GetAllAttachments(string identification)
        {
            throw new NotImplementedException();
        }

>>>>>>> 0e404a6cdd95f9442b65c71e7001e99ceacce189

                rdr.Close();

                return new ResponseAttachmentsList { IsSuccessful = true, ResponseMessage = AppManagement.MSG_GetAllAttachments_Success, ResponseAttachments = attachments };
            }
        }
        #endregion
    }
}
