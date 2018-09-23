using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class EmailModel
    {        
        public static Response SendMail(string identification, string condition = "Roja")
        {
            try
            {
                string causes = string.Empty;
                string strEmail = System.IO.File.ReadAllText(@"..\AITestBackend\Util\EmailTemplate.html");
                var Patient = PatientModel.GetPatient(identification).Patient;
                var Parent = ParentModel.GetParent(Patient.IdParent).Parent;

                if (PatientCaseModel.GetAllPatientCases(Patient.Identification) != null && 
                    PatientCaseModel.GetAllPatientCases(Patient.Identification).IsSuccessful && 
                    PatientCaseModel.GetAllPatientCases(Patient.Identification).PatientCases.Count() > 0)
                {
                     causes = PatientCaseModel.GetAllPatientCases(Patient.Identification).PatientCases[0].Procesed_Data;
                }
                
                //string path = Server.MapPath($"~/EmailTemplate/index.html");
                


                strEmail.Replace("[Nombre del Encargado]",Parent.Name);
                strEmail.Replace("[NombreDelPaciente]", Patient.Name);
                strEmail.Replace("[NumeroCedula]", Parent.Identification);
                strEmail.Replace("[CondicionPaciente]", condition);
                strEmail.Replace("[SintomasPaciente]", causes);
                               

                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strEmail, System.Text.Encoding.UTF8, "text/html");
                MailMessage correo = new MailMessage(Parent.Email, Parent.Email);

                //   Agregar vista a email
                //correo.AlternateViews.Add(htmlView);
                correo.Subject = "Prueba Send Mail Programathon";
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                correo.Body = strEmail;

                // mail to

                //   Set SMTP Client;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(AppManagement.emailUser, AppManagement.emailPass),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                smtp.Send(correo);

                return new Response { IsSuccessful = true, ResponseMessage = "Enviado Exitosamente!" }; ;
            }
            catch (Exception ex)
            {
                return new Response { IsSuccessful = false, ResponseMessage = ex.Message };

            }
        }

    }
}
