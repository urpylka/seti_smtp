using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace seti_smtp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public static void SendMail(string smtpServer, string from, string password, string[] mailto, string subject, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                foreach(string to in mailto) mail.To.Add(new MailAddress(to));
                mail.Subject = subject;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile)) mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                client.Timeout = 1; //чтобы исключить долгое ожидание
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {

                if (textBox5 == null)
                    SendMail(textBox1.Text, textBox2.Text, textBox3.Text, richTextBox1.Text.Split(' '), textBox4.Text, richTextBox2.Text);
                else
                    SendMail(textBox1.Text, textBox2.Text, textBox3.Text, richTextBox1.Text.Split(' '), textBox4.Text, richTextBox2.Text, textBox5.Text);
            }
            catch(Exception e1)
            {
                flag = false;
                richTextBox3.AppendText(e1.ToString());
            }
            if (flag) richTextBox3.AppendText("Сообщение успешно отправлено.");
        }
    }
}
