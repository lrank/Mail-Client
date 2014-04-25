// Houssem Dellai
// houssem.dellai@ieee.org
// +216 95 325 964
// Studying Software Engineering
// in the National Engineering School of Sfax (ENIS)

using System;
using System.Windows;
using System.Net.Mail;
using System.Net;
using Limilabs.Mail;
using Limilabs.Client.IMAP;
using System.Collections.Generic;


namespace MailSender1
{
    class ReceiveMail
    {
        private string MailFrom;
        private string MailFromPass;
        private string MailHost;

        public ReceiveMail(string _MailFrom, string _MailFromPass) {
            MailFrom = _MailFrom;
            MailFromPass = _MailFromPass;
            MailHost = "";
        }

        public string receivemail(int ShowMailCount)
        {
            try
            {
                using (Imap imap = new Imap())
                {
                    if (MailFrom.Contains("sjtu"))
                    {
                        MailHost = "mail.sjtu.edu.cn";
                    }
                    else
                    {
                        MessageBox.Show("unknown host");
                        return null;
                    }
                    imap.Connect(MailHost);  // or ConnectSSL for SSL

                    //Console.WriteLine(MailFrom.Substring(0, MailFrom.IndexOf('@')));
                    imap.UseBestLogin(MailFrom.Substring(0, MailFrom.IndexOf('@')), MailFromPass);
                    // Receive all messages and display the subject
                    MailBuilder builder = new MailBuilder();

                    imap.SelectInbox();
                    List<long> uids = imap.GetAll();

                    Console.WriteLine(uids.Count);

                    string ret = "";

                    for (int i = 1; i <= ShowMailCount; ++i)
                    {
                        IMail email = builder.CreateFromEml(
                            imap.GetMessageByUID(uids[uids.Count - i]));
                        // Check signature
                        //if (email.IsSigned == true)
                        //    email.CheckSignature(true);
                        //Console.WriteLine(email.Subject);
                        //Console.WriteLine(email.Text);
                        ret += email.Subject + '\n' + email.Text + '\n';

                    }

                    imap.Close();
                    return ret;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;
        }
    }

    class SendMail
    {
        private string MailTo;
        private string MailFrom;      
        private string MailFromPass;
        private string Message;
        //private string Host;

        public SendMail(string _MailFrom, string _MailFromPass, string _MailTo, string _Message)
        {
            MailTo = _MailTo;
            MailFrom = _MailFrom;
            MailFromPass = _MailFromPass;
            Message = _Message;
            //Host = _Host;
        }

        public void sendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(MailFrom);
                mail.To.Add(new MailAddress(MailTo));
                mail.Body = Message;

                SmtpClient Smtp_Client = new SmtpClient();
                
                

                if (MailFrom.Contains("gmail"))
                {
                    Smtp_Client = new SmtpClient("smtp.gmail.com", 587);
                    Smtp_Client.EnableSsl = true;
                }
                else if (MailFrom.Contains("live"))
                {
                    Smtp_Client = new SmtpClient("smtp.live.com", 587);
                    Smtp_Client.EnableSsl = true;
                }
                else if (MailFrom.Contains("yahoo"))
                {
                    Smtp_Client = new SmtpClient("smtp.mail.yahoo.com", 587);
                    //Yahoo don't support SSL
                    Smtp_Client.EnableSsl = false;
                }
                else if (MailFrom.Contains("sjtu"))
                {
                    Smtp_Client = new SmtpClient("smtp.sjtu.edu.cn", 587);
                    Smtp_Client.EnableSsl = true;
                }

                Smtp_Client.Credentials = new NetworkCredential(MailFrom, MailFromPass);
                

                Smtp_Client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                Smtp_Client.SendAsync(mail, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message);
            else if (e.Cancelled)
                MessageBox.Show("Cancelled sending !");
            else
                MessageBox.Show("Mail successfully sent!");
        }
    }
}