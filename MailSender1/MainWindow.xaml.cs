// Houssem Dellai
// houssem.dellai@ieee.org
// +216 95 325 964
// Studying Software Engineering
// in the National Engineering School of Sfax (ENIS)

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MailSender1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mouseEnterEvent(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Width == 86)
            {
                b.Margin = new Thickness(b.Margin.Left - 10, b.Margin.Top - 5, b.Margin.Right, b.Margin.Bottom);
                b.Height += 10;
                b.Width += 20;
                b.FontSize += 5;
            }
        }

        private void mouseLeaveEvent(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Width == 106)
            {
                b.Margin = new Thickness(b.Margin.Left + 10, b.Margin.Top + 5, b.Margin.Right, b.Margin.Bottom);
                b.Height -= 10;
                b.Width -= 20;
                b.FontSize -= 5;
            }
        }

        private void sendButtonClick(object sender, RoutedEventArgs e)
        {
            /*if (!(mailFrom.ToString().Contains("@gmail.com") 
                || mailFrom.ToString().Contains("@yahoo") 
                || mailFrom.ToString().Contains("@live")
                || mailFrom.ToString().Contains("@hotmail")))
            {
                MessageBox.Show("You have to use Gmail or Yahoo or MSN !");
            }
            else*/
            if (passwd.Password.Length == 0)
            {
                MessageBox.Show("You have to enter your password !");
            }
            else
            {
                string bodyMail = new TextRange(mailBody.Document.ContentStart, mailBody.Document.ContentEnd).Text;

                SendMail sm = new SendMail(mailFrom.Text, passwd.Password.ToString(), mailTo.Text, bodyMail);
                sm.sendMail();
                //ReceiveMail rm = new ReceiveMail(mailFrom.Text, passwd.Password.ToString());
                //rm.receivemail(3);
            }
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void receiveButtonClick(object sender, RoutedEventArgs e)
        {
            if (passwd.Password.Length == 0)
            {
                MessageBox.Show("You have to enter your password !");
            }
            else
            {
                ReceiveMail rm = new ReceiveMail(mailFrom.Text, passwd.Password.ToString());

                receivetext.Text = rm.receivemail(3);
            }
        }
    }
}
