using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copy_Log_StoLoto
{
    public partial class Copy_Log_MPK : Form
    {
        public Copy_Log_MPK()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = 10000;
                progressBar1.Maximum = Count;
                progressBar1.Value = 0;
                string p1 = textBox4.Text;
                string p3 = textBox5.Text;
                char y = '.';
                if (p1 != null | p3 != null)
                {
                    if (p1 == null)
                    {
                        MessageBox.Show("Поле для ввода номера ОПС или ip (айпи) - пустое");
                        progressBar1.Value = 0;
                        textBox5.Text = null;
                        textBox4.Text = null;
                    }
                    if (p3 == null)
                    {
                        MessageBox.Show("Поле для ввода даты логов МПК - пустое");
                        progressBar1.Value = 0;
                        textBox5.Text = null;
                        textBox4.Text = null;
                    }
                    else
                    {
                        if ((p1.Length > 6 && p1[2] == y) | p1.Length == 6)
                        {
                            if (p1.Length > 6 && p1[2] == y)
                            {
                                progressBar1.Value = 500;
                                DateTime currentDate = DateTime.Now;
                                string NewDateFormat = currentDate.ToString("yyyy-MM-dd HH.mm.ss");
                                string MPK_DateTime_Now = currentDate.ToString("yyyyMMdd");

                                if (p3.Length >= 5)
                                {
                                    MessageBox.Show("Введите 4 числа, как указано в образце");
                                    progressBar1.Value = 0;
                                    textBox5.Text = null;
                                    textBox4.Text = null;
                                }
                                else
                                {
                                    char[] chr1 = { p3[0], p3[1] };
                                    string p4 = new string(chr1); //день
                                    char[] chr2 = { p3[2], p3[3] };
                                    string p5 = new string(chr2); //месяц
                                    string p6 = p5 + p4;
                                    int u = Convert.ToInt32(p5);
                                    int u1 = Convert.ToInt32(p4);
                                    if (u1 >= 32 | u >= 13)
                                    {
                                        if (u1 >= 32)
                                        {
                                            MessageBox.Show("Максимум дней в месяце 31, а не больше");
                                            progressBar1.Value = 0;
                                            textBox5.Text = null;
                                        }
                                        if (u >= 13)
                                        {
                                            MessageBox.Show("В году 12 месяцев, а не больше");
                                            progressBar1.Value = 0;
                                            textBox5.Text = null;
                                        }
                                    }
                                    else
                                    {
                                        string p7 = "2021" + p5 + p4;
                                        Directory.CreateDirectory(@"D:\MPK_LOG");

                                        string path_configuration_1 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130article-2020-07-01.json";
                                        string path_configuration_2 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130articletype-2020-07-01.json";
                                        string path_configuration_3 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130attributes-2020-07-01.json";
                                        string path_configuration_4 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130easops_map-2020-07-01.json";
                                        string path_configuration_5 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130easops_oper-2020-07-01.json";
                                        string path_configuration_6 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130mfk_sql-2020-07-01.json";
                                        string path_configuration_7 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130skdn-2020-07-01.json";
                                        string path_configuration_8 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130skdncommon-2020-07-01.json";
                                        string path_configuration_9 = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\data\f130uploadsettings-2020-07-01.json";
                                        string post_json = @"\\" + p1 + @"\c$\ProgramData\RussianPost\config03\post.json";
                                        string config_ini = @"\\" + p1 + @"\c$\Program Files (x86)\RussianPost\PostOffice\config.ini";

                                        string sdo_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\sdo02\log\main.log.2021" + p6;
                                        string config_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\config03\log\main.log.2021" + p6;
                                        string nsi_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\log\main.log.2021" + p6;
                                        string user_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\user21\log\main.log.2021" + p6;
                                        string trans_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\trans02\log\main.log.2021" + p6;

                                        string path_json_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\post.json";
                                        string path_config_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config.ini";
                                        string path_configuration_copy_1 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130article-2020-07-01.json";
                                        string path_configuration_copy_2 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130articletype-2020-07-01.json";
                                        string path_configuration_copy_3 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130attributes-2020-07-01.json";
                                        string path_configuration_copy_4 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130easops_map-2020-07-01.json";
                                        string path_configuration_copy_5 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130easops_oper-2020-07-01.json";
                                        string path_configuration_copy_6 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130mfk_sql-2020-07-01.json";
                                        string path_configuration_copy_7 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130skdn-2020-07-01.json";
                                        string path_configuration_copy_8 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130skdncommon-2020-07-01.json";
                                        string path_configuration_copy_9 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130uploadsettings-2020-07-01.json";

                                        string catalog_mpk = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat;
                                        string catalog_mpk_zip = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + ".zip";

                                        string sdo_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log\main.log.2021" + p6;
                                        string config_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log\main.log.2021" + p6;
                                        string nsi_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log\main.log.2021" + p6;
                                        string user_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log\main.log.2021" + p6;
                                        string trans_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log\main.log.2021" + p6;

                                        string sdo_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\sdo02\log\errors.log.2021" + p6;
                                        string config_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\config03\log\errors.log.2021" + p6;
                                        string nsi_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\log\errors.log.2021" + p6;
                                        string user_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\user21\log\errors.log.2021" + p6;
                                        string trans_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\trans02\log\errors.log.2021" + p6;

                                        string sdo_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log\errors.log.2021" + p6;
                                        string config_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log\errors.log.2021" + p6;
                                        string nsi_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log\errors.log.2021" + p6;
                                        string user_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log\errors.log.2021" + p6;
                                        string trans_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log\errors.log.2021" + p6;

                                        if (p7 == MPK_DateTime_Now)
                                        {
                                            progressBar1.Value = 1000;
                                            sdo_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\sdo02\log\main.log";
                                            config_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\config03\log\main.log";
                                            nsi_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\log\main.log";
                                            user_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\user21\log\main.log";
                                            trans_log = @"\\" + p1 + @"\c$\ProgramData\RussianPost\trans02\log\main.log";

                                            sdo_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\sdo02\log\errors.log";
                                            config_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\config03\log\errors.log";
                                            nsi_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\nsi02\log\errors.log";
                                            user_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\user21\log\errors.log";
                                            trans_log_errors = @"\\" + p1 + @"\c$\ProgramData\RussianPost\trans02\log\errors.log";

                                            sdo_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log\main.log";
                                            config_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log\main.log";
                                            nsi_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log\main.log";
                                            user_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log\main.log";
                                            trans_log_copy = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log\main.log";

                                            sdo_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log\errors.log";
                                            config_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log\errors.log";
                                            nsi_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log\errors.log";
                                            user_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log\errors.log";
                                            trans_log_copy_errors = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log\errors.log";

                                            textBox6.Text = $"\nПоиск логов MПК и файлов post.json, config.ini, файлов конфигурациии и копирование \nПо пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}";
                                            //MessageBox.Show($"По пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}");
                                            FileInfo fileInf_configuration_11 = new FileInfo(path_configuration_1);
                                            FileInfo fileInf_configuration_21 = new FileInfo(path_configuration_2);
                                            FileInfo fileInf_configuration_31 = new FileInfo(path_configuration_3);
                                            FileInfo fileInf_configuration_41 = new FileInfo(path_configuration_4);
                                            FileInfo fileInf_configuration_51 = new FileInfo(path_configuration_5);
                                            FileInfo fileInf_configuration_61 = new FileInfo(path_configuration_6);
                                            FileInfo fileInf_configuration_71 = new FileInfo(path_configuration_7);
                                            FileInfo fileInf_configuration_81 = new FileInfo(path_configuration_8);
                                            FileInfo fileInf_configuration_91 = new FileInfo(path_configuration_9);
                                            progressBar1.Value = 1500;
                                            FileInfo fileInf_sdo02_log1 = new FileInfo(sdo_log);
                                            FileInfo fileInf_config02_log1 = new FileInfo(config_log);
                                            FileInfo fileInf_nsi02_log1 = new FileInfo(nsi_log);
                                            FileInfo fileInf_user02_log1 = new FileInfo(user_log);
                                            FileInfo fileInf_trans02_log1 = new FileInfo(trans_log);

                                            FileInfo fileInf_sdo02_log1_errors = new FileInfo(sdo_log_errors);
                                            FileInfo fileInf_config02_log1_errors = new FileInfo(config_log_errors);
                                            FileInfo fileInf_nsi02_log1_errors = new FileInfo(nsi_log_errors);
                                            FileInfo fileInf_user02_log1_errors = new FileInfo(user_log_errors);
                                            FileInfo fileInf_trans02_log1_errors = new FileInfo(trans_log_errors);

                                            FileInfo fileInf_post_json1 = new FileInfo(post_json);
                                            FileInfo fileInf_config_ini1 = new FileInfo(config_ini);

                                            if (fileInf_configuration_11.Exists && fileInf_post_json1.Exists && fileInf_config_ini1.Exists)
                                            {
                                                progressBar1.Value = 2500;
                                                textBox6.Text = "\nФайлы - существуют\n";
                                                string catalog_1 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat;
                                                string catalog_2 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1;
                                                string catalog_3 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log";
                                                string catalog_4 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log";
                                                string catalog_5 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log";
                                                string catalog_6 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log";
                                                string catalog_7 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log";
                                                Directory.CreateDirectory(catalog_1);
                                                Directory.CreateDirectory(catalog_2);
                                                if (fileInf_sdo02_log1.Exists)
                                                {
                                                    progressBar1.Value = 3000;
                                                    Directory.CreateDirectory(catalog_3);
                                                    fileInf_sdo02_log1.CopyTo(sdo_log_copy, true);
                                                }
                                                if (fileInf_sdo02_log1_errors.Exists)
                                                {
                                                    progressBar1.Value = 3500;
                                                    fileInf_sdo02_log1_errors.CopyTo(sdo_log_copy_errors, true);
                                                }
                                                if (fileInf_config02_log1.Exists)
                                                {
                                                    progressBar1.Value = 4000;
                                                    Directory.CreateDirectory(catalog_4);
                                                    fileInf_config02_log1.CopyTo(config_log_copy, true);
                                                }
                                                if (fileInf_config02_log1_errors.Exists)
                                                {
                                                    progressBar1.Value = 4500;
                                                    fileInf_config02_log1_errors.CopyTo(config_log_copy_errors, true);
                                                }
                                                if (fileInf_nsi02_log1.Exists)
                                                {
                                                    progressBar1.Value = 5000;
                                                    Directory.CreateDirectory(catalog_5);
                                                    fileInf_nsi02_log1.CopyTo(nsi_log_copy, true);
                                                }
                                                if (fileInf_nsi02_log1_errors.Exists)
                                                {
                                                    progressBar1.Value = 5500;
                                                    fileInf_nsi02_log1_errors.CopyTo(nsi_log_copy_errors, true);
                                                }
                                                if (fileInf_user02_log1.Exists)
                                                {
                                                    progressBar1.Value = 6000;
                                                    Directory.CreateDirectory(catalog_6);
                                                    fileInf_user02_log1.CopyTo(user_log_copy, true);
                                                }
                                                if (fileInf_user02_log1_errors.Exists)
                                                {
                                                    progressBar1.Value = 6500;
                                                    fileInf_user02_log1_errors.CopyTo(user_log_copy_errors, true);
                                                }
                                                if (fileInf_trans02_log1.Exists)
                                                {
                                                    progressBar1.Value = 7000;
                                                    Directory.CreateDirectory(catalog_7);
                                                    fileInf_trans02_log1.CopyTo(trans_log_copy, true);
                                                }
                                                if (fileInf_trans02_log1_errors.Exists)
                                                {
                                                    progressBar1.Value = 7500;
                                                    fileInf_trans02_log1_errors.CopyTo(trans_log_copy_errors, true);
                                                }
                                                progressBar1.Value = 8000;
                                                //MessageBox.Show("\nФайлы - существуют\nВыполняется копирование файлов на диск \"D\" Вашего ПК ...");
                                                fileInf_configuration_11.CopyTo(path_configuration_copy_1, true);
                                                fileInf_configuration_21.CopyTo(path_configuration_copy_2, true);
                                                fileInf_configuration_31.CopyTo(path_configuration_copy_3, true);
                                                fileInf_configuration_41.CopyTo(path_configuration_copy_4, true);
                                                fileInf_configuration_51.CopyTo(path_configuration_copy_5, true);
                                                fileInf_configuration_61.CopyTo(path_configuration_copy_6, true);
                                                fileInf_configuration_71.CopyTo(path_configuration_copy_7, true);
                                                fileInf_configuration_81.CopyTo(path_configuration_copy_8, true);
                                                fileInf_configuration_91.CopyTo(path_configuration_copy_9, true);
                                                progressBar1.Value = 8500;
                                                fileInf_post_json1.CopyTo(path_json_copy, true);
                                                progressBar1.Value = 9000;
                                                fileInf_config_ini1.CopyTo(path_config_copy, true);
                                                //MessageBox.Show("Выполняется архивирование файлов...");
                                                //progressBar1.Value = 5800;
                                                ZipFile.CreateFromDirectory(catalog_mpk, catalog_mpk_zip);
                                                string path_delete = catalog_mpk;
                                                progressBar1.Value = 9500;
                                                Directory.Delete(path_delete, true);
                                                //progressBar1.Value = 9000;
                                                progressBar1.Value = 10000;
                                                textBox6.Text = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + ".zip";
                                                MessageBox.Show("\nАрхивирование завершено, создан архив с необходимыми файлами\n");
                                                progressBar1.Value = 0;
                                                textBox5.Text = null;
                                                textBox4.Text = null;

                                            }
                                            else
                                            {
                                                progressBar1.Value = 10000;
                                                MessageBox.Show($"\nПо пути: \n{post_json} \n{path_configuration_1}\n{config_ini}\nФайлы - не существуют\n");
                                                progressBar1.Value = 0;
                                                textBox5.Text = null;
                                                textBox4.Text = null;
                                            }
                                        }

                                        else
                                        {
                                            progressBar1.Value = 1000;
                                            textBox6.Text = "\nПоиск логов MПК и файлов post.json, config.ini, файлов конфигурациии и копирование";
                                            textBox6.Text = $"По пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}";
                                            FileInfo fileInf_configuration_1 = new FileInfo(path_configuration_1);
                                            FileInfo fileInf_configuration_2 = new FileInfo(path_configuration_2);
                                            FileInfo fileInf_configuration_3 = new FileInfo(path_configuration_3);
                                            FileInfo fileInf_configuration_4 = new FileInfo(path_configuration_4);
                                            FileInfo fileInf_configuration_5 = new FileInfo(path_configuration_5);
                                            FileInfo fileInf_configuration_6 = new FileInfo(path_configuration_6);
                                            FileInfo fileInf_configuration_7 = new FileInfo(path_configuration_7);
                                            FileInfo fileInf_configuration_8 = new FileInfo(path_configuration_8);
                                            FileInfo fileInf_configuration_9 = new FileInfo(path_configuration_9);
                                            progressBar1.Value = 1500;
                                            FileInfo fileInf_sdo02_log_errors = new FileInfo(sdo_log_errors);
                                            FileInfo fileInf_config02_log_errors = new FileInfo(config_log_errors);
                                            FileInfo fileInf_nsi02_log_errors = new FileInfo(nsi_log_errors);
                                            FileInfo fileInf_user02_log_errors = new FileInfo(user_log_errors);
                                            FileInfo fileInf_trans02_log_errors = new FileInfo(trans_log_errors);
                                            progressBar1.Value = 2000;
                                            FileInfo fileInf_sdo02_log = new FileInfo(sdo_log);
                                            FileInfo fileInf_config02_log = new FileInfo(config_log);
                                            FileInfo fileInf_nsi02_log = new FileInfo(nsi_log);
                                            FileInfo fileInf_user02_log = new FileInfo(user_log);
                                            FileInfo fileInf_trans02_log = new FileInfo(trans_log);

                                            FileInfo fileInf_post_json = new FileInfo(post_json);
                                            FileInfo fileInf_config_ini = new FileInfo(config_ini);
                                            progressBar1.Value = 2500;
                                            if (fileInf_configuration_1.Exists && fileInf_post_json.Exists && fileInf_config_ini.Exists)
                                            {
                                                //textBox6.Text = "\nФайлы - существуют\n";
                                                string catalog_1 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat;
                                                string catalog_2 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1;
                                                string catalog_3 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\sdo02 log";
                                                string catalog_4 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\config03 log";
                                                string catalog_5 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\nsi02 log";
                                                string catalog_6 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\user21 log";
                                                string catalog_7 = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + @"\trans02 log";
                                                progressBar1.Value = 3500;
                                                Directory.CreateDirectory(catalog_1);
                                                progressBar1.Value = 4500;
                                                Directory.CreateDirectory(catalog_2);
                                                if (fileInf_sdo02_log.Exists)
                                                {
                                                    progressBar1.Value = 5000;
                                                    Directory.CreateDirectory(catalog_3);
                                                    fileInf_sdo02_log.CopyTo(sdo_log_copy, true);
                                                }
                                                if (fileInf_sdo02_log_errors.Exists)
                                                {
                                                    progressBar1.Value = 5500;
                                                    fileInf_sdo02_log_errors.CopyTo(sdo_log_copy_errors, true);
                                                }

                                                if (fileInf_config02_log.Exists)
                                                {
                                                    progressBar1.Value = 6000;
                                                    Directory.CreateDirectory(catalog_4);
                                                    fileInf_config02_log.CopyTo(config_log_copy, true);
                                                }
                                                if (fileInf_config02_log_errors.Exists)
                                                {
                                                    progressBar1.Value = 6500;
                                                    fileInf_config02_log_errors.CopyTo(config_log_copy_errors, true);
                                                }

                                                if (fileInf_nsi02_log.Exists)
                                                {
                                                    progressBar1.Value = 7000;
                                                    Directory.CreateDirectory(catalog_5);
                                                    fileInf_nsi02_log.CopyTo(nsi_log_copy, true);
                                                }
                                                if (fileInf_nsi02_log_errors.Exists)
                                                {
                                                    progressBar1.Value = 7500;
                                                    fileInf_nsi02_log_errors.CopyTo(nsi_log_copy_errors, true);
                                                }

                                                if (fileInf_user02_log.Exists)
                                                {
                                                    progressBar1.Value = 8000;
                                                    Directory.CreateDirectory(catalog_6);
                                                    fileInf_user02_log.CopyTo(user_log_copy, true);
                                                }
                                                if (fileInf_user02_log_errors.Exists)
                                                {
                                                    progressBar1.Value = 8500;
                                                    fileInf_user02_log_errors.CopyTo(user_log_copy_errors, true);
                                                }

                                                if (fileInf_trans02_log.Exists)
                                                {
                                                    progressBar1.Value = 9000;
                                                    Directory.CreateDirectory(catalog_7);
                                                    fileInf_trans02_log.CopyTo(trans_log_copy, true);
                                                }
                                                if (fileInf_trans02_log_errors.Exists)
                                                {
                                                    fileInf_trans02_log_errors.CopyTo(trans_log_copy_errors, true);
                                                }
                                                //MessageBox.Show("\nФайлы - существуют\nВыполняется копирование файлов на диск \"D\" Вашего ПК ...");
                                                fileInf_configuration_1.CopyTo(path_configuration_copy_1, true);
                                                fileInf_configuration_2.CopyTo(path_configuration_copy_2, true);
                                                fileInf_configuration_3.CopyTo(path_configuration_copy_3, true);
                                                fileInf_configuration_4.CopyTo(path_configuration_copy_4, true);
                                                fileInf_configuration_5.CopyTo(path_configuration_copy_5, true);
                                                fileInf_configuration_6.CopyTo(path_configuration_copy_6, true);
                                                fileInf_configuration_7.CopyTo(path_configuration_copy_7, true);
                                                fileInf_configuration_8.CopyTo(path_configuration_copy_8, true);
                                                fileInf_configuration_9.CopyTo(path_configuration_copy_9, true);
                                                fileInf_post_json.CopyTo(path_json_copy, true);
                                                fileInf_config_ini.CopyTo(path_config_copy, true);
                                                //MessageBox.Show("\nВыполняется архивирование файлов...\n");
                                                progressBar1.Value = 9500;
                                                ZipFile.CreateFromDirectory(catalog_mpk, catalog_mpk_zip);
                                                string path_delete = catalog_mpk;
                                                Directory.Delete(path_delete, true);
                                                progressBar1.Value = 10000;
                                                MessageBox.Show("Архивирование файлов завершено\n");
                                                textBox6.Text = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + ".zip";
                                                progressBar1.Value = 0;
                                                textBox5.Text = null;
                                                textBox4.Text = null;
                                            }
                                            else
                                            {
                                                progressBar1.Value = 10000;
                                                MessageBox.Show($"\nПо пути: \n{post_json} \n{path_configuration_1}\n{config_ini}\nФайлы - не существуют\n");
                                                progressBar1.Value = 0;
                                                textBox5.Text = null;
                                                textBox4.Text = null;
                                            }
                                        }
                                    }



                                }
                            }
                        }
                    }
                }
                if (p1.Length == 6)
                {
                    progressBar1.Value = 1000;
                    DateTime currentDate = DateTime.Now;
                    string NewDateFormat = currentDate.ToString("yyyy-MM-dd HH.mm.ss");
                    string MPK_DateTime_Now = currentDate.ToString("yyyyMMdd");
                    string p2 = "n";
                    p3 = textBox5.Text;
                    if (p3.Length >= 5)
                    {
                        MessageBox.Show("Введите 4 числа, как указано в образце");
                        progressBar1.Value = 0;
                        textBox5.Text = null;
                    }
                    else
                    {
                        char[] chr1 = { p3[0], p3[1] };
                        string p4 = new string(chr1); //день
                        char[] chr2 = { p3[2], p3[3] };
                        string p5 = new string(chr2); //месяц
                        string p6 = p5 + p4;
                        int u = Convert.ToInt32(p5);
                        int u1 = Convert.ToInt32(p4);
                        if (u1 >= 32 | u >= 13)
                        {
                            if (u1 >= 32)
                            {
                                MessageBox.Show("Максимум дней в месяце 31, а не больше");
                                progressBar1.Value = 0;
                                textBox5.Text = null;
                            }
                            if (u >= 13)
                            {
                                MessageBox.Show("В году 12 месяцев, а не больше");
                                progressBar1.Value = 0;
                                textBox5.Text = null;
                            }
                        }
                        else
                        {
                            string p7 = "2021" + p5 + p4;
                            Directory.CreateDirectory(@"D:\MPK_LOG");

                            string path_configuration_1 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130article-2020-07-01.json";
                            string path_configuration_2 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130articletype-2020-07-01.json";
                            string path_configuration_3 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130attributes-2020-07-01.json";
                            string path_configuration_4 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130easops_map-2020-07-01.json";
                            string path_configuration_5 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130easops_oper-2020-07-01.json";
                            string path_configuration_6 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130mfk_sql-2020-07-01.json";
                            string path_configuration_7 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130skdn-2020-07-01.json";
                            string path_configuration_8 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130skdncommon-2020-07-01.json";
                            string path_configuration_9 = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\data\f130uploadsettings-2020-07-01.json";
                            string post_json = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\config03\post.json";
                            string config_ini = @"\\r40-" + p1 + "-" + p2 + @"\c$\Program Files (x86)\RussianPost\PostOffice\config.ini";

                            string sdo_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\sdo02\log\main.log.2021" + p6;
                            string config_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\config03\log\main.log.2021" + p6;
                            string nsi_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\log\main.log.2021" + p6;
                            string user_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\user21\log\main.log.2021" + p6;
                            string trans_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\trans02\log\main.log.2021" + p6;

                            string path_json_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\post.json";
                            string path_config_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config.ini";
                            string path_configuration_copy_1 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130article-2020-07-01.json";
                            string path_configuration_copy_2 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130articletype-2020-07-01.json";
                            string path_configuration_copy_3 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130attributes-2020-07-01.json";
                            string path_configuration_copy_4 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130easops_map-2020-07-01.json";
                            string path_configuration_copy_5 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130easops_oper-2020-07-01.json";
                            string path_configuration_copy_6 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130mfk_sql-2020-07-01.json";
                            string path_configuration_copy_7 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130skdn-2020-07-01.json";
                            string path_configuration_copy_8 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130skdncommon-2020-07-01.json";
                            string path_configuration_copy_9 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1 + @"\f130uploadsettings-2020-07-01.json";

                            string catalog_mpk = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat;
                            string catalog_mpk_zip = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + ".zip";

                            string sdo_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log\main.log.2021" + p6;
                            string config_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log\main.log.2021" + p6;
                            string nsi_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log\main.log.2021" + p6;
                            string user_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log\main.log.2021" + p6;
                            string trans_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log\main.log.2021" + p6;

                            string sdo_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\sdo02\log\errors.log.2021" + p6;
                            string config_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\config03\log\errors.log.2021" + p6;
                            string nsi_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\log\errors.log.2021" + p6;
                            string user_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\user21\log\errors.log.2021" + p6;
                            string trans_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\trans02\log\errors.log.2021" + p6;

                            string sdo_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log\errors.log.2021" + p6;
                            string config_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log\errors.log.2021" + p6;
                            string nsi_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log\errors.log.2021" + p6;
                            string user_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log\errors.log.2021" + p6;
                            string trans_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log\errors.log.2021" + p6;

                            if (p7 == MPK_DateTime_Now)
                            {
                                progressBar1.Value = 2000;
                                sdo_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\sdo02\log\main.log";
                                config_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\config03\log\main.log";
                                nsi_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\log\main.log";
                                user_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\user21\log\main.log";
                                trans_log = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\trans02\log\main.log";

                                sdo_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\sdo02\log\errors.log";
                                config_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\config03\log\errors.log";
                                nsi_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\nsi02\log\errors.log";
                                user_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\user21\log\errors.log";
                                trans_log_errors = @"\\r40-" + p1 + "-" + p2 + @"\c$\ProgramData\RussianPost\trans02\log\errors.log";

                                sdo_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log\main.log";
                                config_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log\main.log";
                                nsi_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log\main.log";
                                user_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log\main.log";
                                trans_log_copy = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log\main.log";

                                sdo_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log\errors.log";
                                config_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log\errors.log";
                                nsi_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log\errors.log";
                                user_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log\errors.log";
                                trans_log_copy_errors = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log\errors.log";

                                //MessageBox.Show($"\nПоиск логов MПК и файлов post.json, config.ini, файлов конфигурациии и копирование\nПо пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}");
                                //$"По пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}");
                                FileInfo fileInf_configuration_11 = new FileInfo(path_configuration_1);
                                FileInfo fileInf_configuration_21 = new FileInfo(path_configuration_2);
                                FileInfo fileInf_configuration_31 = new FileInfo(path_configuration_3);
                                FileInfo fileInf_configuration_41 = new FileInfo(path_configuration_4);
                                FileInfo fileInf_configuration_51 = new FileInfo(path_configuration_5);
                                FileInfo fileInf_configuration_61 = new FileInfo(path_configuration_6);
                                FileInfo fileInf_configuration_71 = new FileInfo(path_configuration_7);
                                FileInfo fileInf_configuration_81 = new FileInfo(path_configuration_8);
                                FileInfo fileInf_configuration_91 = new FileInfo(path_configuration_9);

                                FileInfo fileInf_sdo02_log1 = new FileInfo(sdo_log);
                                FileInfo fileInf_config02_log1 = new FileInfo(config_log);
                                FileInfo fileInf_nsi02_log1 = new FileInfo(nsi_log);
                                FileInfo fileInf_user02_log1 = new FileInfo(user_log);
                                FileInfo fileInf_trans02_log1 = new FileInfo(trans_log);

                                FileInfo fileInf_sdo02_log1_errors = new FileInfo(sdo_log_errors);
                                FileInfo fileInf_config02_log1_errors = new FileInfo(config_log_errors);
                                FileInfo fileInf_nsi02_log1_errors = new FileInfo(nsi_log_errors);
                                FileInfo fileInf_user02_log1_errors = new FileInfo(user_log_errors);
                                FileInfo fileInf_trans02_log1_errors = new FileInfo(trans_log_errors);

                                FileInfo fileInf_post_json1 = new FileInfo(post_json);
                                FileInfo fileInf_config_ini1 = new FileInfo(config_ini);

                                if (fileInf_configuration_11.Exists && fileInf_post_json1.Exists && fileInf_config_ini1.Exists)
                                {
                                    progressBar1.Value = 3000;
                                    //MessageBox.Show("\nФайлы - существуют\n");
                                    string catalog_1 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat;
                                    string catalog_2 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1;
                                    string catalog_3 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log";
                                    string catalog_4 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log";
                                    string catalog_5 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log";
                                    string catalog_6 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log";
                                    string catalog_7 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log";
                                    Directory.CreateDirectory(catalog_1);
                                    Directory.CreateDirectory(catalog_2);
                                    if (fileInf_sdo02_log1.Exists)
                                    {
                                        progressBar1.Value = 4000;
                                        Directory.CreateDirectory(catalog_3);
                                        fileInf_sdo02_log1.CopyTo(sdo_log_copy, true);
                                    }
                                    if (fileInf_sdo02_log1_errors.Exists)
                                    {
                                        fileInf_sdo02_log1_errors.CopyTo(sdo_log_copy_errors, true);
                                    }
                                    if (fileInf_config02_log1.Exists)
                                    {
                                        progressBar1.Value = 5000;
                                        Directory.CreateDirectory(catalog_4);
                                        fileInf_config02_log1.CopyTo(config_log_copy, true);
                                    }
                                    if (fileInf_config02_log1_errors.Exists)
                                    {
                                        fileInf_config02_log1_errors.CopyTo(config_log_copy_errors, true);
                                    }
                                    if (fileInf_nsi02_log1.Exists)
                                    {
                                        progressBar1.Value = 6000;
                                        Directory.CreateDirectory(catalog_5);
                                        fileInf_nsi02_log1.CopyTo(nsi_log_copy, true);
                                    }
                                    if (fileInf_nsi02_log1_errors.Exists)
                                    {
                                        fileInf_nsi02_log1_errors.CopyTo(nsi_log_copy_errors, true);
                                    }
                                    if (fileInf_user02_log1.Exists)
                                    {
                                        progressBar1.Value = 7000;
                                        Directory.CreateDirectory(catalog_6);
                                        fileInf_user02_log1.CopyTo(user_log_copy, true);
                                    }
                                    if (fileInf_user02_log1_errors.Exists)
                                    {
                                        fileInf_user02_log1_errors.CopyTo(user_log_copy_errors, true);
                                    }
                                    if (fileInf_trans02_log1.Exists)
                                    {
                                        progressBar1.Value = 8000;
                                        Directory.CreateDirectory(catalog_7);
                                        fileInf_trans02_log1.CopyTo(trans_log_copy, true);
                                    }
                                    if (fileInf_trans02_log1_errors.Exists)
                                    {
                                        fileInf_trans02_log1_errors.CopyTo(trans_log_copy_errors, true);
                                    }
                                    progressBar1.Value = 9000;
                                    //MessageBox.Show("\nФайлы - существуют\nВыполняется копирование файлов на диск \"D\" Вашего ПК ...");
                                    fileInf_configuration_11.CopyTo(path_configuration_copy_1, true);
                                    fileInf_configuration_21.CopyTo(path_configuration_copy_2, true);
                                    fileInf_configuration_31.CopyTo(path_configuration_copy_3, true);
                                    fileInf_configuration_41.CopyTo(path_configuration_copy_4, true);
                                    fileInf_configuration_51.CopyTo(path_configuration_copy_5, true);
                                    fileInf_configuration_61.CopyTo(path_configuration_copy_6, true);
                                    fileInf_configuration_71.CopyTo(path_configuration_copy_7, true);
                                    fileInf_configuration_81.CopyTo(path_configuration_copy_8, true);
                                    fileInf_configuration_91.CopyTo(path_configuration_copy_9, true);
                                    fileInf_post_json1.CopyTo(path_json_copy, true);
                                    fileInf_config_ini1.CopyTo(path_config_copy, true);
                                    progressBar1.Value = 9500;
                                    //MessageBox.Show("\nВыполняется архивирование файлов...\n");
                                    ZipFile.CreateFromDirectory(catalog_mpk, catalog_mpk_zip);
                                    string path_delete = catalog_mpk;
                                    progressBar1.Value = 10000;
                                    Directory.Delete(path_delete, true);
                                    MessageBox.Show("Архивирование файлов завершено\n");
                                    textBox6.Text = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + ".zip";
                                    progressBar1.Value = 0;
                                    textBox5.Text = null;
                                    textBox4.Text = null;
                                }
                                else
                                {
                                    progressBar1.Value = 10000;
                                    MessageBox.Show($"\nПо пути: \n{post_json} \n{path_configuration_1}\n{config_ini}\nФайлы - не существуют\n");
                                    progressBar1.Value = 0;
                                    textBox5.Text = null;
                                    textBox4.Text = null;
                                }
                            }
                            else
                            {
                                progressBar1.Value = 1000;
                                //MessageBox.Show($"\nПоиск логов MПК и файлов post.json, config.ini, файлов конфигурациии и копирование\nПо пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}");
                                //MessageBox.Show($"По пути: \n{post_json} \n{ path_configuration_1}\n{config_ini}");
                                FileInfo fileInf_configuration_1 = new FileInfo(path_configuration_1);
                                FileInfo fileInf_configuration_2 = new FileInfo(path_configuration_2);
                                FileInfo fileInf_configuration_3 = new FileInfo(path_configuration_3);
                                FileInfo fileInf_configuration_4 = new FileInfo(path_configuration_4);
                                FileInfo fileInf_configuration_5 = new FileInfo(path_configuration_5);
                                FileInfo fileInf_configuration_6 = new FileInfo(path_configuration_6);
                                FileInfo fileInf_configuration_7 = new FileInfo(path_configuration_7);
                                FileInfo fileInf_configuration_8 = new FileInfo(path_configuration_8);
                                FileInfo fileInf_configuration_9 = new FileInfo(path_configuration_9);

                                FileInfo fileInf_sdo02_log_errors = new FileInfo(sdo_log_errors);
                                FileInfo fileInf_config02_log_errors = new FileInfo(config_log_errors);
                                FileInfo fileInf_nsi02_log_errors = new FileInfo(nsi_log_errors);
                                FileInfo fileInf_user02_log_errors = new FileInfo(user_log_errors);
                                FileInfo fileInf_trans02_log_errors = new FileInfo(trans_log_errors);

                                FileInfo fileInf_sdo02_log = new FileInfo(sdo_log);
                                FileInfo fileInf_config02_log = new FileInfo(config_log);
                                FileInfo fileInf_nsi02_log = new FileInfo(nsi_log);
                                FileInfo fileInf_user02_log = new FileInfo(user_log);
                                FileInfo fileInf_trans02_log = new FileInfo(trans_log);

                                FileInfo fileInf_post_json = new FileInfo(post_json);
                                FileInfo fileInf_config_ini = new FileInfo(config_ini);
                                progressBar1.Value = 1500;
                                if (fileInf_configuration_1.Exists && fileInf_post_json.Exists && fileInf_config_ini.Exists)
                                {
                                    progressBar1.Value = 2000;
                                    //MessageBox.Show("\nФайлы - существуют\n");
                                    string catalog_1 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat;
                                    string catalog_2 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\Конфигурация ОПС " + p1;
                                    string catalog_3 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\sdo02 log";
                                    string catalog_4 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\config03 log";
                                    string catalog_5 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\nsi02 log";
                                    string catalog_6 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\user21 log";
                                    string catalog_7 = @"D:\MPK_LOG\File_From_ops-" + p1 + " " + NewDateFormat + @"\trans02 log";
                                    Directory.CreateDirectory(catalog_1);
                                    Directory.CreateDirectory(catalog_2);
                                    if (fileInf_sdo02_log.Exists)
                                    {
                                        progressBar1.Value = 3000;
                                        Directory.CreateDirectory(catalog_3);
                                        fileInf_sdo02_log.CopyTo(sdo_log_copy, true);
                                    }
                                    if (fileInf_sdo02_log_errors.Exists)
                                    {
                                        fileInf_sdo02_log_errors.CopyTo(sdo_log_copy_errors, true);
                                    }

                                    if (fileInf_config02_log.Exists)
                                    {
                                        progressBar1.Value = 4000;
                                        Directory.CreateDirectory(catalog_4);
                                        fileInf_config02_log.CopyTo(config_log_copy, true);
                                    }
                                    if (fileInf_config02_log_errors.Exists)
                                    {
                                        fileInf_config02_log_errors.CopyTo(config_log_copy_errors, true);
                                    }

                                    if (fileInf_nsi02_log.Exists)
                                    {
                                        progressBar1.Value = 5000;
                                        Directory.CreateDirectory(catalog_5);
                                        fileInf_nsi02_log.CopyTo(nsi_log_copy, true);
                                    }
                                    if (fileInf_nsi02_log_errors.Exists)
                                    {
                                        fileInf_nsi02_log_errors.CopyTo(nsi_log_copy_errors, true);
                                    }

                                    if (fileInf_user02_log.Exists)
                                    {
                                        progressBar1.Value = 6000;
                                        Directory.CreateDirectory(catalog_6);
                                        fileInf_user02_log.CopyTo(user_log_copy, true);
                                    }
                                    if (fileInf_user02_log_errors.Exists)
                                    {
                                        fileInf_user02_log_errors.CopyTo(user_log_copy_errors, true);
                                    }

                                    if (fileInf_trans02_log.Exists)
                                    {
                                        progressBar1.Value = 7000;
                                        Directory.CreateDirectory(catalog_7);
                                        fileInf_trans02_log.CopyTo(trans_log_copy, true);
                                    }
                                    if (fileInf_trans02_log_errors.Exists)
                                    {
                                        progressBar1.Value = 8000;
                                        fileInf_trans02_log_errors.CopyTo(trans_log_copy_errors, true);
                                    }
                                    //MessageBox.Show("\nФайлы - существуют\nВыполняется копирование файлов на диск \"D\" Вашего ПК ...");
                                    fileInf_configuration_1.CopyTo(path_configuration_copy_1, true);
                                    fileInf_configuration_2.CopyTo(path_configuration_copy_2, true);
                                    fileInf_configuration_3.CopyTo(path_configuration_copy_3, true);
                                    fileInf_configuration_4.CopyTo(path_configuration_copy_4, true);
                                    fileInf_configuration_5.CopyTo(path_configuration_copy_5, true);
                                    fileInf_configuration_6.CopyTo(path_configuration_copy_6, true);
                                    fileInf_configuration_7.CopyTo(path_configuration_copy_7, true);
                                    fileInf_configuration_8.CopyTo(path_configuration_copy_8, true);
                                    fileInf_configuration_9.CopyTo(path_configuration_copy_9, true);
                                    fileInf_post_json.CopyTo(path_json_copy, true);
                                    fileInf_config_ini.CopyTo(path_config_copy, true);
                                    progressBar1.Value = 9000;
                                    //MessageBox.Show("\nВыполняется архивирование файлов...\n");
                                    ZipFile.CreateFromDirectory(catalog_mpk, catalog_mpk_zip);
                                    string path_delete = catalog_mpk;
                                    Directory.Delete(path_delete, true);
                                    progressBar1.Value = 10000;
                                    MessageBox.Show("Архивирование файлов завершено\n");
                                    textBox6.Text = @"D:\MPK_LOG\File_From_ip-" + p1 + " " + NewDateFormat + ".zip";
                                    progressBar1.Value = 0;
                                    textBox5.Text = null;
                                    textBox4.Text = null;
                                }
                                else
                                {
                                    MessageBox.Show($"\nПо пути: \n{post_json} \n{path_configuration_1}\n{config_ini}\nФайлы - не существуют\n");
                                    progressBar1.Value = 10000;
                                    progressBar1.Value = 0;
                                    textBox5.Text = null;
                                    textBox4.Text = null;
                                }
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Некорректный ввод номера ОПС или ip (айпи) или даты");
                    progressBar1.Value = 0;
                    textBox5.Text = null;
                    textBox4.Text = null;
                }
                if (p1 == null & p3 == null)
                {
                    MessageBox.Show("Поле для ввода номера ОПС(ip) - пустое\nИ поле для ввода даты тоже пустое\nЗаполните поля, потом нажимайте кнопку");
                    progressBar1.Value = 0;
                    textBox5.Text = null;
                    textBox4.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}");
                progressBar1.Value = 0;
                textBox5.Text = null;
                textBox4.Text = null;
            }
        }
        //private void label6_Click(object sender, EventArgs e)
        //{

        //}
        //private void progressBar1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
