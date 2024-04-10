using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Аптека
{
    public partial class DataForm : Form
    {
        DataBase database = new DataBase();

        int selectedRow;

        public DataForm()
        {
            InitializeComponent();
        }

        //создание столбцов
        private void CreateColumns()
        {
            //препарвты
            dataGV_Preparat.Columns.Add("ID", "ID");
            dataGV_Preparat.Columns.Add("id_Postavka", "ID поставщика");
            dataGV_Preparat.Columns.Add("id_Proizvod", "ID производитель");
            dataGV_Preparat.Columns.Add("id_Gruop", "ID группа препарата");
            dataGV_Preparat.Columns.Add("Name", "Наименование");
            dataGV_Preparat.Columns.Add("Unit_measurement", "Еденица измерения");
            dataGV_Preparat.Columns.Add("Price_Purchase", "Цена закупочная");
            dataGV_Preparat.Columns.Add("Price_Sale", "Цена реализации");
            //группа препаратов
            dataGV_Group.Columns.Add("ID", "ID");
            dataGV_Group.Columns.Add("Gruop_prepar", "Группа препарата");
            dataGV_Group.Columns.Add("Diseases", "Болезни");
            dataGV_Group.Columns.Add("Description", "Описание");
            //поставщики 
            dataGV_Pstavshik.Columns.Add("ID", "ID");
            dataGV_Pstavshik.Columns.Add("Name", "Название фирмы");
            dataGV_Pstavshik.Columns.Add("Predstav", "Представитель");
            dataGV_Pstavshik.Columns.Add("Post", "Должность");
            dataGV_Pstavshik.Columns.Add("Address", "Адрес");
            dataGV_Pstavshik.Columns.Add("City", "Город");
            dataGV_Pstavshik.Columns.Add("Strana", "Страна");
            dataGV_Pstavshik.Columns.Add("Number", "Номер");
            //сотрудники
            dataGV_Sotrudnik.Columns.Add("ID", "ID");
            dataGV_Sotrudnik.Columns.Add("Famil", "Фамилия");
            dataGV_Sotrudnik.Columns.Add("Name", "Имя");
            dataGV_Sotrudnik.Columns.Add("Otshest", "Отчество");
            dataGV_Sotrudnik.Columns.Add("Post", "Должность");
            dataGV_Sotrudnik.Columns.Add("Date_brith", "Дата рождения");
            dataGV_Sotrudnik.Columns.Add("Date_hiring", "Дата найма");
            dataGV_Sotrudnik.Columns.Add("Address", "Адрес");
            dataGV_Sotrudnik.Columns.Add("City", "Грод");
            dataGV_Sotrudnik.Columns.Add("Strana", "Страна");
            dataGV_Sotrudnik.Columns.Add("Number", "Номер");
            dataGV_Sotrudnik.Columns.Add("Salary", "Зарплата");
            //производители
            dataGV_Proizvoditel.Columns.Add("ID", "ID");
            dataGV_Proizvoditel.Columns.Add("Proizwod4", "Производитель");
            //клиенты
            dataGV_Klient.Columns.Add("ID", "ID");
            dataGV_Klient.Columns.Add("Name", "Название");
            dataGV_Klient.Columns.Add("Predstavit", "Представитель");
            dataGV_Klient.Columns.Add("Address", "Адрес");
            dataGV_Klient.Columns.Add("City", "Город");
            dataGV_Klient.Columns.Add("Strana", "Страна");
            dataGV_Klient.Columns.Add("Number", "Номер");
            dataGV_Klient.Columns.Add("Fax5", "Факс");
            //заказы
            dataGV_Zakaz.Columns.Add("ID", "ID");
            dataGV_Zakaz.Columns.Add("ID_klient", "ID клиента");
            dataGV_Zakaz.Columns.Add("ID_sotrud", "ID сотрудника");
            dataGV_Zakaz.Columns.Add("Prepar", "ID Препарат");
            dataGV_Zakaz.Columns.Add("Date_", "Дата размещения");
            dataGV_Zakaz.Columns.Add("Date_ispoln", "Дата прибытия");
            dataGV_Zakaz.Columns.Add("Kolli", "Колличество");
            dataGV_Zakaz.Columns.Add("Price", "Цена");
            dataGV_Zakaz.Columns.Add("Itog_price", "Итоговая цена");
        }

        //занесения тип данных в строки
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            //препараты
            if (dgw == dataGV_Preparat)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2),
                record.GetInt32(3), record.GetString(4), record.GetString(5),
                record.GetInt32(6), record.GetInt32(7));
            }

            //группа препаратов
            else if (dgw == dataGV_Group)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3));
            }

            //поставщики
            else if (dgw == dataGV_Pstavshik)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5),
                record.GetString(6), record.GetString(7));
            }

            //сотрудники
            else if (dgw == dataGV_Sotrudnik)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
               record.GetString(3), record.GetString(4), record.GetDateTime(5),
               record.GetDateTime(6), record.GetString(7), record.GetString(8), record.GetString(9),
               record.GetString(10), record.GetInt32(11));
            }

            //производители
            else if (dgw == dataGV_Proizvoditel)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1));
            }

            //клиенты 
            else if (dgw == dataGV_Klient)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                    record.GetString(3), record.GetString(4), record.GetString(5),
                    record.GetString(6), record.GetInt32(7));
            }

            //заказы
            else if (dgw == dataGV_Zakaz)
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2),
                   record.GetString(3), record.GetDateTime(4), record.GetDateTime(5),
                   record.GetInt32(6), record.GetInt32(7), record.GetInt32(8));
            }
        }

        //вывод данных в таблицу
        private void RefreshDgv(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = "";

            if (dgw == dataGV_Preparat)
            {
                queryString = $"SELECT * FROM Препараты";
            }
            else if (dgw == dataGV_Group)
            {
                queryString = $"SELECT * FROM Группа_препаратов";
            }
            else if (dgw == dataGV_Pstavshik)
            {
                queryString = $"SELECT * FROM Поставщики";
            }
            else if (dgw == dataGV_Sotrudnik)
            {
                queryString = $"SELECT * FROM Сотрудники";
            }
            else if (dgw == dataGV_Proizvoditel)
            {
                queryString = $"SELECT * FROM Производители";
            }
            else if (dgw == dataGV_Klient)
            {
                queryString = $"SELECT * FROM Клиенты";
            }
            else if (dgw == dataGV_Zakaz)
            {
                queryString = $"SELECT * FROM Заказы";
            }
            SqlCommand sqlCommand = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDgv(dataGV_Preparat);
            RefreshDgv(dataGV_Group);
            RefreshDgv(dataGV_Pstavshik);
            RefreshDgv(dataGV_Sotrudnik);
            RefreshDgv(dataGV_Proizvoditel);
            RefreshDgv(dataGV_Klient);
            RefreshDgv(dataGV_Zakaz);
        }

        private void Exite_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGV_Preparat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Preparat.Rows[selectedRow];

                txtB0_id_Postav.Text = row.Cells[1].Value.ToString();
                txtB0_id_Proizvod.Text = row.Cells[2].Value.ToString();
                txtB0_id_Group.Text = row.Cells[3].Value.ToString();
                txtB0_Name.Text = row.Cells[4].Value.ToString();
                cmbB0_Unit_measurement.Text = row.Cells[5].Value.ToString();
                txtB0_Price_Purchase.Text = row.Cells[5].Value.ToString();
                txtB0_Price_sale.Text = row.Cells[6].Value.ToString();
            }
        }

        private void dataGV_Group_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Group.Rows[selectedRow];

                txtB2_Group.Text = row.Cells[1].Value.ToString();
                richTB2_diseases.Text = row.Cells[2].Value.ToString();
                richTB2_description.Text = row.Cells[3].Value.ToString();
            }
        }

        private void dataGV_Pstavshik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Pstavshik.Rows[selectedRow];

                txtB3_name.Text = row.Cells[1].Value.ToString();
                txtB3_predstavit.Text = row.Cells[2].Value.ToString();
                cmbB3_post.Text = row.Cells[3].Value.ToString();
                txtB3_address.Text = row.Cells[4].Value.ToString();
                txtB3_city.Text = row.Cells[5].Value.ToString();
                txtB3_Strana.Text = row.Cells[6].Value.ToString();
                txtB3_Number.Text = row.Cells[7].Value.ToString();
            }
        }

        private void dataGV_Sotrudnik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Sotrudnik.Rows[selectedRow];

                txtB4_surname.Text = row.Cells[1].Value.ToString();
                txtB4_name.Text = row.Cells[2].Value.ToString();
                txtB4_otshest.Text = row.Cells[3].Value.ToString();
                txtB4_address.Text = row.Cells[4].Value.ToString();
                date4_brith.Text = row.Cells[5].Value.ToString();
                date4_hiring.Text = row.Cells[6].Value.ToString();
                cmbB4_post.Text = row.Cells[7].Value.ToString();
                txtB4_strana.Text = row.Cells[8].Value.ToString();
                txtB4_number.Text = row.Cells[9].Value.ToString();
                txtB4_salary.Text = row.Cells[10].Value.ToString();
            }
        }

        private void dataGV_Proizvoditel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Proizvoditel.Rows[selectedRow];

                txtB5_proizvoditel.Text = row.Cells[1].Value.ToString();
            }
        }

        private void dataGV_Klient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Klient.Rows[selectedRow];

                txtB6_name.Text = row.Cells[1].Value.ToString();
                txtB6_predstavit.Text = row.Cells[2].Value.ToString();
                txtB6_address.Text = row.Cells[3].Value.ToString();
                txtB6_city.Text = row.Cells[4].Value.ToString();
                txtB6_strana.Text = row.Cells[5].Value.ToString();
                txtB6_number.Text = row.Cells[6].Value.ToString();
                txtB6_fax.Text = row.Cells[7].Value.ToString();
            }
        }

        private void dataGV_Zakaz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGV_Zakaz.Rows[selectedRow];

                txtB7_id_klient.Text = row.Cells[1].Value.ToString();
                txtB7_id_sotrud.Text = row.Cells[2].Value.ToString();
                txtB7_prepar.Text = row.Cells[3].Value.ToString();
                date7_plecement.Text = row.Cells[4].Value.ToString();
                date7_prib.Text = row.Cells[5].Value.ToString();
                txtB7_kolli.Text = row.Cells[6].Value.ToString();
                txtB7_prise.Text = row.Cells[7].Value.ToString();
                txtB7_itog_price.Text = row.Cells[8].Value.ToString();
            }
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGV_Preparat);
            Search(dataGV_Group);
            Search(dataGV_Pstavshik);
            Search(dataGV_Sotrudnik);
            Search(dataGV_Proizvoditel);
            Search(dataGV_Klient);
            Search(dataGV_Zakaz);
        }

        //поиск в textBox
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string serchString = " ";

            if (dgw == dataGV_Preparat)
            {
                serchString = $"SELECT * FROM Препараты where concat(ID, ID_поставщика, ID_производитель, ID_группа_препарата, Наименование, Еденица_измерения, Цена_закупочная, Цена_реализации) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Group)
            {
                serchString = $"SELECT * FROM Группа_препаратов where concat(Группа, Болезни, Описание) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Pstavshik)
            {
                serchString = $"SELECT * FROM Поставщики where concat(ID, Название_фирмы, Представитель, Должность, Адрес, Город, Страна, Номер) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Sotrudnik)
            {
                serchString = $"SELECT * FROM Сотрудники where concat(ID, Фамилия, Имя, Отчество, Должность, Дата_рождения, Дата_найма, Адрес, Город, Страна, Номер, Зарплата) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Proizvoditel)
            {
                serchString = $"SELECT * FROM Производители where concat(ID, Производитель) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Klient)
            {
                serchString = $"SELECT * FROM Клиенты where concat(ID, Название, Представитель, Адрес, Город, Страна, Номер, Факс) like '%{textBox_search.Text}%' ";
            }
            else if (dgw == dataGV_Zakaz)
            {
                serchString = $"SELECT * FROM Заказы where concat(ID, ID_клиента, ID_сотрудника, Дата_размещения, Дата_прибытия, Препарат, Количество, Цена,  Итоговая_стоимость) like '%{textBox_search.Text}%' ";
            }

            SqlCommand com = new SqlCommand(serchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }

        //добавить препараты
        private void Add0()
        {
            database.openConnection();

            var ID_поставщика = txtB0_id_Postav.Text;
            var ID_производитель = txtB0_id_Proizvod.Text;
            var ID_группа_препарата = txtB0_id_Group.Text;
            var Наименование = txtB0_Name.Text;
            string Еденица_измерения = cmbB0_Unit_measurement.Text;
            var Цена_закупочная = txtB0_Price_Purchase.Text;
            var Цена_реализации = txtB0_Price_sale.Text;

            var add1Query = $"INSERT INTO Препараты(ID_поставщика, ID_производитель, ID_группа_препарата, Наименование, Еденица_измерения, Цена_закупочная, Цена_реализации) VALUES" +
            $"('{ID_поставщика}', '{ID_производитель}', '{ID_группа_препарата}', '{Наименование}', '{Еденица_измерения}', '{Цена_закупочная}', '{Цена_реализации}')";

            var command = new SqlCommand(add1Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить группу
        private void Add2()
        {
            database.openConnection();

            var Группа = txtB2_Group.Text;
            string Болезни = richTB2_diseases.Text;
            string Описание = richTB2_description.Text;

            var add2Query = $"INSERT INTO Группа_препаратов(Группа, Болезни, Описание) VALUES('{Группа}', '{Болезни}', '{Описание}')";

            var command = new SqlCommand(add2Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить поставщика
        private void Add3()
        {
            database.openConnection();

            var Название_фирмы = txtB3_name.Text;
            var Представитель = txtB3_predstavit.Text;
            string Должность = cmbB3_post.Text;
            var Адрес = txtB3_address.Text;
            string Город = txtB3_city.Text;
            var Страна = txtB3_Strana.Text;
            var Номер = txtB3_Number.Text;

            var add3Query = $"INSERT INTO Поставщики(Название_фирмы, Представитель, Должность, Адрес, Город, Страна, Номер) VALUES" +
            $"('{Название_фирмы}', '{Представитель}', '{Должность}', '{Адрес}', '{Город}', '{Страна}', '{Номер}')";

            var command = new SqlCommand(add3Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить сотрудника
        private void Add4()
        {
            database.openConnection();

            var Фамилия = txtB4_surname.Text;
            var Имя = txtB4_name.Text;
            var Отчество = txtB4_otshest.Text;
            string Должность = cmbB4_post.Text;
            string Дата_рождения = date4_brith.Value.ToString("yyyy-MM-dd");
            string Дата_найма = date4_hiring.Value.ToString("yyyy-MM-dd");
            var Адрес = txtB4_address.Text;
            var Город = txtB4_city.Text;
            var Страна = txtB4_strana.Text;
            var Номер = txtB4_number.Text;
            var Зарплата = txtB4_salary.Text;

            var add4Query = $"INSERT INTO Сотрудники(Фамилия, Имя, Отчество, Должность, Дата_рождения, Дата_найма, Адрес, Город, Страна, Номер, Зарплата) VALUES('{Фамилия}', '{Имя}', '{Отчество}', '{Должность}', '{Дата_рождения}', '{Дата_найма}', '{Адрес}', '{Город}', '{Страна}', '{Номер}', '{Зарплата}')";

            var command = new SqlCommand(add4Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить производителя
        private void Add5()
        {
            database.openConnection();

            var Производитель = txtB5_proizvoditel.Text;

            var add5Query = $"INSERT INTO Производители(Производитель) VALUES ('{Производитель}')";

            var command = new SqlCommand(add5Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить клиента
        private void Add6()
        {
            database.openConnection();

            var Название = txtB6_name.Text;
            var Представитель = txtB6_predstavit.Text;
            var Адрес = txtB6_address.Text;
            var Город = txtB6_city.Text;
            var Страна = txtB6_strana.Text;
            var Номер = txtB6_number.Text;
            var Факс = txtB6_fax.Text;

            var add6Query = $"INSERT INTO Клиенты(Название, Представитель, Адрес, Город, Страна, Номер, Факс) VALUES" +
            $"('{Название}', '{Представитель}', '{Адрес}', '{Город}', '{Страна}', '{Номер}', '{Факс}')";

            var command = new SqlCommand(add6Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        //добавить заказы
        private void Add7()
        {
            database.openConnection();

            var ID_клиента = txtB7_id_klient.Text;
            var ID_сотрудника = txtB7_id_sotrud.Text;
            var Препарат = txtB7_prepar.Text;
            var Дата_размещения = date7_plecement.Value.ToString("yyyy-MM-dd");
            var Дата_прибытия = date7_prib.Value.ToString("yyyy-MM-dd");
            var Количество = txtB7_kolli.Text;
            var Цена = txtB7_prise.Text;
            var Итоговая_стоимость = txtB7_itog_price.Text;

            var add7Query = $"INSERT INTO Заказы(ID_клиента, ID_сотрудника, Препарат, Дата_размещения, " +
                $"Дата_прибытия, Количество, Цена, Итоговая_стоимость) VALUES" +
            $"('{ID_клиента}', '{ID_сотрудника}', '{Препарат}', '{Дата_размещения}', '{Дата_прибытия}', '{Количество}', '{Цена}', '{Итоговая_стоимость}')";

            var command = new SqlCommand(add7Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }


        private void btn_save0_Click(object sender, EventArgs e)
        {
            Add0();
            RefreshDgv(dataGV_Preparat);
        }

        private void btn_save2_Click(object sender, EventArgs e)
        {
            Add2();
            RefreshDgv(dataGV_Group);
        }

        private void btn_save3_Click(object sender, EventArgs e)
        {
            Add3();
            RefreshDgv(dataGV_Pstavshik);
        }

        private void btn_save4_Click(object sender, EventArgs e)
        {
            Add4();
            RefreshDgv(dataGV_Sotrudnik);
        }

        private void btn_save5_Click(object sender, EventArgs e)
        {
            Add5();
            RefreshDgv(dataGV_Proizvoditel);
        }

        private void btn_save6_Click(object sender, EventArgs e)
        {
            Add6();
            RefreshDgv(dataGV_Klient);
        }

        private void btn_save7_Click(object sender, EventArgs e)
        {
            Add7();
            RefreshDgv(dataGV_Zakaz);
        }


        private void delete0()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Preparat.Rows[index].Cells[0].Value);

            var delete1Query = $"DELETE FROM Препараты where id = '{id}' ";

            var command = new SqlCommand(delete1Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        private void delete2()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Group.Rows[index].Cells[0].Value);

            var delete2Query = $"DELETE FROM Группа_препаратов where id = '{id}'";

            var command = new SqlCommand(delete2Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        private void delete3()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Pstavshik.Rows[index].Cells[0].Value);

            var delete3Query = $"DELETE FROM Поставщики where id = '{id}'";

            var command = new SqlCommand(delete3Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }
        private void delete4()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Sotrudnik.Rows[index].Cells[0].Value);

            var delete4Query = $"DELETE FROM Сотрудники where id = '{id}'";

            var command = new SqlCommand(delete4Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }
        private void delete5()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Proizvoditel.Rows[index].Cells[0].Value);

            var delete5Query = $"DELETE FROM Производители where id = '{id} '";

            var command = new SqlCommand(delete5Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }
        private void delete6()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Klient.Rows[index].Cells[0].Value);

            var delete6Query = $"DELETE FROM Клиенты where id = '{id}'";

            var command = new SqlCommand(delete6Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }
        private void delete7()
        {
            database.openConnection();
            int index = selectedRow;
            var id = Convert.ToInt32(dataGV_Zakaz.Rows[index].Cells[0].Value);

            var delete7Query = $"DELETE FROM Заказы where id = '{id} '";

            var command = new SqlCommand(delete7Query, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
        }

        private void btn_delete0_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete0();
                RefreshDgv(dataGV_Preparat);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete2_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete2();
                RefreshDgv(dataGV_Group);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete3();
                RefreshDgv(dataGV_Pstavshik);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete4();
                RefreshDgv(dataGV_Sotrudnik);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete5();
                RefreshDgv(dataGV_Proizvoditel);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete6();
                RefreshDgv(dataGV_Klient);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_delete7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите безвозвратно удалить эту запись?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                delete7();
                RefreshDgv(dataGV_Zakaz);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn0_Add_Click(object sender, EventArgs e)
        {
            txtB0_id_Postav.Text = "";
            txtB0_id_Proizvod.Text = "";
            txtB0_id_Group.Text = "";
            txtB0_Name.Text = "";
            cmbB0_Unit_measurement.Text = "";
            txtB0_Price_Purchase.Text = "";
            txtB0_Price_sale.Text = "";
        }

        private void btn2_Add_Click(object sender, EventArgs e)
        {
            txtB2_Group.Text = "";
            richTB2_diseases.Text = "";
            richTB2_description.Text = "";
        }

        private void btn3_Add_Click(object sender, EventArgs e)
        {
            txtB3_name.Text = "";
            txtB3_predstavit.Text = "";
            cmbB3_post.Text = "";
            txtB3_address.Text = "";
            txtB3_city.Text = "";
            txtB3_Strana.Text = "";
            txtB3_Number.Text = "";
        }

        private void btn4_Add_Click(object sender, EventArgs e)
        {
            txtB4_surname.Text = "";
            txtB4_name.Text = "";
            txtB4_otshest.Text = "";
            txtB4_address.Text = "";
            date4_brith.Text = "";
            date4_hiring.Text = "";
            cmbB4_post.Text = "";
            txtB4_strana.Text = "";
            txtB4_number.Text = "";
            txtB4_salary.Text = "";
        }

        private void btn5_Add_Click(object sender, EventArgs e)
        {
            txtB5_proizvoditel.Text = "";
        }

        private void btn6_Add_Click(object sender, EventArgs e)
        {
            txtB6_name.Text = "";
            txtB6_predstavit.Text = "";
            txtB6_address.Text = "";
            txtB6_city.Text = "";
            txtB6_strana.Text = "";
            txtB6_number.Text = "";
            txtB6_fax.Text = "";
        }

        private void btn7_Add_Click(object sender, EventArgs e)
        {
            txtB7_id_klient.Text = "";
            txtB7_id_sotrud.Text = "";
            txtB7_prepar.Text = "";
            date7_plecement.Text = "";
            date7_prib.Text = "";
            txtB7_kolli.Text = "";
            txtB7_prise.Text = "";
            txtB7_itog_price.Text = "";
        }

        private void txtB7_kolli_TextChanged(object sender, EventArgs e)
        {
            if ((txtB7_kolli.Text != "") & (txtB7_prise.Text != ""))
            {
                txtB7_itog_price.Text = Convert.ToString(Convert.ToInt32(txtB7_kolli.Text) * Convert.ToInt32(txtB7_prise.Text));
            }
        }
    }
}
