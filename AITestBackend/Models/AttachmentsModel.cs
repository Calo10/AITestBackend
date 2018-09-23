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

        public static Response SavePatient(AttachmentsModel attachments)
        {
            try
            {
                using (MySqlConnection conn = ConecctionModel.conn)
                {
                    conn.Open();
                    int idresult = 0;

                    string SP = AppManagement.SP_LoginUser;

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
            catch (Exception)
            {
                return new Response { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveAttachment_FailureDefault };
            }

        }

        public static ResponseAttachments GetAllAttachments(string identification)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
