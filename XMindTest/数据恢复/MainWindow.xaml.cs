using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 数据恢复
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sqlstr =   string.Format("server = {0}; user = root;Charset=utf8;database = dtu_dev; port = 3306; password = {1}", host.Text,password.Text);

            
            MySqlConnection con = new MySqlConnection(sqlstr);//创建一个MySqlConnection对象
            try
            {
                
                con.Open();//打开数据库

            }
            catch (Exception)
            {

                return;
            }
            string path = "C:\\Users\\yang.bai\\Documents\\2w.sql";
            StreamReader reader = new StreamReader(path );
            string sql = null;
            int num = 1;
            MySqlCommand command = null;
            Task.Run(() => {
                while ((sql = reader.ReadLine()) != null)
                {
                    command = new MySqlCommand(sql, con);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("插入成功! num = " + num);
                    }
                    else
                    {
                        Console.WriteLine("插入失败！num = " + num);
                    }
                    num++;
                }

            });

            
 

        }
    }
}
