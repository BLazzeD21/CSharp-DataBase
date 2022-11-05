using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool change;
        SqlConnection conn1 = new SqlConnection();
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxCars.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPoliceman.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPersons.DropDownStyle = ComboBoxStyle.DropDownList;
            ChangeTables.DropDownStyle = ComboBoxStyle.DropDownList;
            string sConnString1 = @"Data Source=05C06PC11\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True";
            conn1.ConnectionString = sConnString1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cars' AND xtype='U')
                CREATE TABLE [Cars]
                ([CarsID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
                [Марка] varchar(50),
                [Модель] varchar(50), 
                [Цвет] varchar(50), 
                [Номер машины] varchar(50))";

            SqlCommand cmdCreate = new SqlCommand(sql, conn1);
            cmdCreate.ExecuteNonQuery();

            MessageBox.Show("Таблица создана!");
            conn1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn1.Close();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "DROP TABLE [Cars]";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Таблица удалена!");
            conn1.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "Insert into Cars (Марка, Модель, Цвет, [Номер Машины] )  values ('Kia','Rio','Красный','B895УP')";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Данные добавлены в таблицу!");
            conn1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSqlComm = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons' AND xtype='U')
                               CREATE TABLE [Persons](
                               [PersonsID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
                               [Фамилия] [varchar](50) NULL,
                               [Имя] [varchar](50) NULL, 
                               [Отчество] [varchar](50) NULL,
                               [Количество нарушений] [integer] NULL,
                               [Нарушение] [varchar](50) NULL,
                               [Дата нарушения] [varchar](50) NULL,
                               [Город] [varchar](50) NULL,
                               [Место нарушения] [varchar](50) NULL,
                               [Наказание] [varchar](50) NULL
                               )";

            SqlCommand cmdCreate = new SqlCommand(sSqlComm, conn1);
            cmdCreate.ExecuteNonQuery();

            MessageBox.Show("Таблица создана!");
            conn1.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "DROP TABLE [Persons]";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Таблица удалена!");
            conn1.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "Insert into Persons (Фамилия, Имя, Отчество, [Количество нарушений], Нарушение, [Дата нарушения],Город,[Место нарушения],Наказание )  values ('Краснов', 'Олег', 'Олегович',0,'Превышение скорости','24.05.2020','Тосно','Чехова 6','Штраф: 500 рублей')";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Данные добавлены в таблицу!");
            conn1.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSqlComm = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Policeman' AND xtype='U')
                               CREATE TABLE [Policeman](
                               [PolicemanID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
                               [Фамилия сотрудника] [varchar](50) NULL,
                               [Имя сотрудника] [varchar](50) NULL, 
                               [Должность] [varchar](50) NULL,
                               [Участок] [varchar](50) NULL
                               )";

            SqlCommand cmdCreate = new SqlCommand(sSqlComm, conn1);
            cmdCreate.ExecuteNonQuery();

            MessageBox.Show("Таблица создана!");
            conn1.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "DROP TABLE [Policeman]";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Таблица удалена!");
            conn1.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sSql = "Insert into Policeman ( [Фамилия сотрудника],[Имя сотрудника],Должность,Участок)  values ('Олегов', 'Олег', 'Инспектор','Чехова')";
            SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
            cmdDrop.ExecuteNonQuery();

            MessageBox.Show("Данные добавлены в таблицу!");
            conn1.Close();
        }

        private void ID_TextChanged(object sender, EventArgs e)
        {
            conn1.Open();
            TablesItems.Items.Clear();
            NumberOfIntruder.Text = "Нарушитель № " + ID.Text;

            if (ID.Text != "")
            {

                SqlCommand CarsChanged = new SqlCommand("Select Марка, Модель, Цвет, [Номер Машины] from Cars where CarsID =@CarsID", conn1);
                CarsChanged.Parameters.AddWithValue("@CarsID", int.Parse(ID.Text));
                SqlDataReader tr = CarsChanged.ExecuteReader();

                while (tr.Read())
                {
                    TablesItems.Items.Add("Марка: " + tr.GetValue(0).ToString());
                    TablesItems.Items.Add("Модель: " + tr.GetValue(1).ToString());
                    TablesItems.Items.Add("Цвет: " + tr.GetValue(2).ToString());
                    TablesItems.Items.Add("Номер: " + tr.GetValue(3).ToString());
                }
                tr.Close();


                SqlCommand PersonsChanged = new SqlCommand("Select Фамилия, Имя, Отчество, [Количество нарушений], Нарушение, [Дата нарушения],Город,[Место нарушения],Наказание from Persons where PersonsID =@PersonsID", conn1);
                PersonsChanged.Parameters.AddWithValue("@PersonsID", int.Parse(ID.Text));
                SqlDataReader tp = PersonsChanged.ExecuteReader();

                while (tp.Read())
                {
                    TablesItems.Items.Add("Фамилия: " + tp.GetValue(0).ToString());
                    TablesItems.Items.Add("Имя: " + tp.GetValue(1).ToString());
                    TablesItems.Items.Add("Отчество: " + tp.GetValue(2).ToString());
                    TablesItems.Items.Add("Количество нарушений: " + tp.GetValue(3).ToString());
                    TablesItems.Items.Add("Нарушение: " + tp.GetValue(4).ToString());
                    TablesItems.Items.Add("Дата нарушения: " + tp.GetValue(5).ToString());
                    TablesItems.Items.Add("Город: " + tp.GetValue(6).ToString());
                    TablesItems.Items.Add("Место нарушения: " + tp.GetValue(7).ToString());
                    TablesItems.Items.Add("Наказание: " + tp.GetValue(8).ToString());


                }
                tp.Close();
                SqlCommand PolicemanChanged = new SqlCommand("Select [Фамилия сотрудника],[Имя сотрудника],Должность,Участок from Policeman where PolicemanID =@PolicemanID", conn1);
                PolicemanChanged.Parameters.AddWithValue("@PolicemanID", int.Parse(ID.Text));
                SqlDataReader tm = PolicemanChanged.ExecuteReader();
                while (tm.Read())
                {
                    TablesItems.Items.Add("Фамилия сотрудника: " + tm.GetValue(0).ToString());
                    TablesItems.Items.Add("Имя сотрудника: " + tm.GetValue(1).ToString());
                    TablesItems.Items.Add("Должность: " + tm.GetValue(2).ToString());
                    TablesItems.Items.Add("Участок: " + tm.GetValue(3).ToString());
                }
                tm.Close();

            }
            conn1.Close();
        }

        private void CarsID_TextChanged(object sender, EventArgs e)
        {
            conn1.Open();
            LabelCheckMarka.Text = null;
            LabelCheckModel.Text = null;
            LabelCheckColor.Text = null;
            LabelCheckNumber.Text = null;
            if (CarsID.Text != "")
            {
                SqlCommand CarsChanged = new SqlCommand("Select Марка, Модель, Цвет, [Номер Машины] from Cars where CarsID =@CarsID", conn1);
                CarsChanged.Parameters.AddWithValue("@CarsID", int.Parse(CarsID.Text));
                SqlDataReader tr = CarsChanged.ExecuteReader();
                while (tr.Read())
                {
                    LabelCheckMarka.Text = tr.GetValue(0).ToString();
                    LabelCheckModel.Text = tr.GetValue(1).ToString();
                    LabelCheckColor.Text = tr.GetValue(2).ToString();
                    LabelCheckNumber.Text = tr.GetValue(3).ToString();

                }
            }
            conn1.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            conn1.Open();
            if (AddMarka.Text == "" || AddModel.Text == "" || AddColor.Text == "" || AddNumber.Text == "")
            {
                MessageBox.Show("Недостаточно сведений", "Добавление машины", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                string sSql = "Insert into Cars ( Марка, Модель, Цвет, [Номер Машины] )  values ('" + AddMarka.Text + "','" + AddModel.Text + "','" + AddColor.Text + "','" + AddNumber.Text + "')";
                SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
                cmdDrop.ExecuteNonQuery();

                MessageBox.Show("Данные добавлены в базу данных!", "Добавление машины", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            conn1.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            conn1.Open();
            if (AddSite.Text == "" || AddPosition.Text == "" || AddPolicemanName.Text == "" || AddPolicemanSubname.Text == "")
            {
                MessageBox.Show("Недостаточно сведений", "Добавление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                string sSql = "Insert into Policeman ([Фамилия сотрудника],[Имя сотрудника],Должность,Участок )  values ('" + AddSite.Text + "','" + AddPosition.Text + "','" + AddPolicemanName.Text + "','" + AddPolicemanSubname.Text + "')";
                SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
                cmdDrop.ExecuteNonQuery();

                MessageBox.Show("Данные добавлены в базу данных!", "Добавление сотрудника", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            conn1.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            conn1.Open();
            if (AddSubname.Text == "" || AddName.Text == "" || AddPatronymic.Text == "" || AddQuantity.Text == "" || AddViolation.Text == "" || AddData.Text == "" || AddCity.Text == "" || AddPlace.Text == "" || AddPunishment.Text == "")
            {
                MessageBox.Show("Недостаточно сведений", "Добавление нарушителя", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sSql = "Insert into Persons (Фамилия, Имя, Отчество, [Количество нарушений], Нарушение, [Дата нарушения],Город,[Место нарушения],Наказание) values ('" + AddSubname.Text + "','" + AddName.Text + "','" + AddPatronymic.Text + "','" + AddQuantity.Text + "','" + AddViolation.Text + "','" + AddData.Text + "','" + AddCity.Text + "','" + AddPlace.Text + "','" + AddPunishment.Text + "')";
                SqlCommand cmdDrop = new SqlCommand(sSql, conn1);
                cmdDrop.ExecuteNonQuery();

                MessageBox.Show("Данные добавлены в базу данных!", "Добавление нарушителя", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            conn1.Close();
        }

        private void ChangeSize_Click(object sender, EventArgs e)
        {
            if (change)
            {
                this.Size = new Size(718, 584); change = false;
                ChangeSize.Text = "Режим разработчика";

            }
            else
            {
                this.Size = new Size(840, 584); change = true;
                ChangeSize.Text = "Обычный режим";

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TablesItems.Items.Clear();
            LabelCheckMarka.Text = null;
            LabelCheckModel.Text = null;
            LabelCheckColor.Text = null;
            LabelCheckNumber.Text = null;
            AddMarka.Text = null;
            AddModel.Text = null;
            AddColor.Text = null;
            AddNumber.Text = null;
            AddSite.Text = null;
            AddPosition.Text = null;
            AddPolicemanName.Text = null;
            AddPolicemanSubname.Text = null;
            AddPlace.Text = null;
            AddSubname.Text = null;
            AddName.Text = null;
            AddPunishment.Text = null;
            AddPatronymic.Text = null;
            NewValue.Text = null;
            AddQuantity.Text = null;
            AddViolation.Text = null;
            AddData.Text = null;
            AddCity.Text = null;
            CarsID.Text = null;
            ID.Text = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (NewValue.Text == "")
            {
                MessageBox.Show("Недостаточно сведений", "Исправление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (ChangeTables.Text == "Cars")
                {
                    string item = "";
                    if (comboBoxCars.Text == "Номер")
                    { item = "[Номер машины]"; }
                    else
                    { item = comboBoxCars.Text; }
                    conn1.Open();
                    SqlCommand cmdUpd = new SqlCommand();
                    string sSql = "Update Cars set " + item + " = '" + NewValue.Text + "' where CarsID = " + numericUpDownID.Value + "";
                    cmdUpd.CommandText = sSql;
                    cmdUpd.Connection = conn1;
                    cmdUpd.CommandType = CommandType.Text;
                    int iUpd = cmdUpd.ExecuteNonQuery();
                    MessageBox.Show("Исправлено! " + comboBoxCars.Text + ": " + NewValue.Text);
                    conn1.Close();
                }
                if (ChangeTables.Text == "Policeman")
                {
                    string item = "";
                    if (comboBoxPoliceman.Text == "Фамилия")
                    { item = "[Фамилия сотрудника]"; }
                    else
                    {
                        if (comboBoxPoliceman.Text == "Имя")
                        { item = "[Имя сотрудника]"; }
                        else
                        { item = comboBoxPoliceman.Text; }
                    }



                    conn1.Open();
                    SqlCommand cmdUpd = new SqlCommand();
                    string sSql = "Update Policeman set " + item + " = '" + NewValue.Text + "' where PolicemanID = " + numericUpDownID.Value + "";
                    cmdUpd.CommandText = sSql;
                    cmdUpd.Connection = conn1;
                    cmdUpd.CommandType = CommandType.Text;
                    int iUpd = cmdUpd.ExecuteNonQuery();
                    MessageBox.Show("Исправлено! " + comboBoxPoliceman.Text + ": " + NewValue.Text);
                    conn1.Close();
                }
                if (ChangeTables.Text == "Persons")
                {
                    string item = "";
                    item = ChangeTables.Text;
                    if (comboBoxPersons.Text == "Количество нарушений")
                    { item = "[Количество нарушений]"; }
                    else
                    {
                        if (comboBoxPersons.Text == "Дата нарушения")
                        { item = "[Дата нарушения]"; }
                        else
                        {
                            if (comboBoxPersons.Text == "Место нарушения")
                            { item = "[Место нарушения]"; }
                            else { item = comboBoxPersons.Text; }
                        }

                    }
               
            
      
                    conn1.Open();
                    SqlCommand cmdUpd = new SqlCommand();
                    string sSql = "Update Persons set " + item + " = '" + NewValue.Text + "' where PersonsID = " + numericUpDownID.Value + "";
                    cmdUpd.CommandText = sSql;
                    cmdUpd.Connection = conn1;
                    cmdUpd.CommandType = CommandType.Text;
                    int iUpd = cmdUpd.ExecuteNonQuery();
                    MessageBox.Show("Исправлено! " + comboBoxPersons.Text + ": " + NewValue.Text);
                    conn1.Close();
                }
            }

        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChangeTables.Text == "Cars")
            {
                comboBoxCars.Text = "Марка";
                comboBoxPoliceman.Text = null;
                comboBoxPersons.Text = null;
                comboBoxCars.Enabled = true;
                comboBoxPoliceman.Enabled = false;
                comboBoxPersons.Enabled = false;
            }
            if (ChangeTables.Text == "Persons")
            {
                comboBoxCars.Text = null;
                comboBoxPoliceman.Text = null;
                comboBoxPersons.Text = "Фамилия";
                comboBoxCars.Enabled = false;
                comboBoxPoliceman.Enabled = false;
                comboBoxPersons.Enabled = true;
            }
            if (ChangeTables.Text == "Policeman")
            {
                comboBoxCars.Text = null;
                comboBoxPersons.Text = null;
                comboBoxPoliceman.Text = "Фамилия";
                comboBoxCars.Enabled = false;
                comboBoxPoliceman.Enabled = true;
                comboBoxPersons.Enabled = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            conn1.Open();
            string sqlCars = "DELETE FROM Cars WHERE CarsID = 1";
            SqlCommand cmdDelete = new SqlCommand(sqlCars, conn1);
            cmdDelete.ExecuteNonQuery();
            string sqlPersons = "DELETE FROM Persons WHERE PersonsID = 1";
            SqlCommand cmdPersons = new SqlCommand(sqlPersons, conn1);
            cmdPersons.ExecuteNonQuery();
            string sqlPoliceman = "DELETE FROM Policeman WHERE PolicemanID = 1";
            SqlCommand cmdPoliceman = new SqlCommand(sqlPoliceman, conn1);
            cmdPoliceman.ExecuteNonQuery();



            conn1.Close();
        }
    }
}
