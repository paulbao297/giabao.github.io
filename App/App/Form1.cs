using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace App
{
    public partial class Form1 : Form
    {
        public MongoDB mongo = new MongoDB();
        List<LoadData> doc = new List<LoadData>();
        public ObjectId GetId = new ObjectId();
        List<RecordData> DL_csdl = new List<RecordData>();
        Doc_for_del_sal DFDSal = new Doc_for_del_sal();
        private bool Is_Loading_Data = false;
        public string Time_all;
        public string name_of_file;
        public Form1()
        {
            InitializeComponent();
            mongo.Connect();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("WELCOME TO SYSTEM, SIR !!!");           
        }

        private void bt_get_Click(object sender, EventArgs e)
        {

           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private Pos binary_search(string key, int start, int stop)  //search bin find out obj base on key
        {
            int middle = 0;
            while (start<=stop)
            {
                middle = (int)((start + stop) / 2);
                if (doc[middle].Name == key)
                    break;
                if (string.Compare(doc[middle].Name, key) < 0)
                    start = middle + 1;
                else if (string.Compare(doc[middle].Name, key) > 0)
                    stop = middle - 1;
            }
            Pos res = new Pos();

            int tmp = middle;
            while (tmp >= 0 && doc[tmp].Name == key)
            {
                res.start = tmp--;
            }

            tmp = middle;
            while (tmp <= doc.Count()-1 && doc[tmp].Name == key)
            {
                res.stop = tmp++;
            }

            return res;
        }

        private void handlerselection()
        {
            try
            {
                if (Is_Loading_Data) return;
                if (doc.Count <= 0) return;
                DataGridViewRow selectedRow = dataGridView2.CurrentRow; //dataGridView2.Rows[index];
                string getname = Convert.ToString(selectedRow.Cells["nameDataGridViewTextBoxColumn"].Value);
                Pos tmp = new Pos();
                tmp = binary_search(getname, 0, doc.Count() - 1);
                get_infor_allday(tmp.start, tmp.stop, true);
            }
            catch
            {
                MessageBox.Show("THIS OBJECT DOESN'T EXIST!!");
            }
            

        }
        private void handlerselection2()
        {
            if (Is_Loading_Data) return;
            /*if (doc.Count <= 0) return;*/
            DataGridViewRow selRow = dataGridView3.CurrentRow;
            string getname2 = Convert.ToString(selRow.Cells["nameDataGridViewTextBoxColumn1"].Value);
            //display infor base on getname value
            RecordData Infor_data = mongo.Get_AData(getname2);
            namebx.Text = Infor_data.Name;
            idbx.Text = Infor_data.ID;
            phonebx.Text = Infor_data.Phone;
            agebx.Text = Infor_data.Age;
            mssvbx.Text = Infor_data.MSSV;
            majorbx.Text = Infor_data.Major;
            GetId = Infor_data.image_id;
            sal_grade_bx.Text = Infor_data.Sal_grade;
            var _file = mongo.GetFile(Infor_data.image_id);
            var buffer = new byte[_file.Length];
            _file.Read(buffer, 0, (int)_file.Length);
            using(var newFs = new FileStream("E:\\Folder\\default_path_img.jpg", FileMode.Create))
            {
                newFs.Write(buffer, 0, buffer.Length);
                pictureBox1.Image = Image.FromStream(newFs);
            }
            
            
            


            //name_bxx.Text = Infor_data.Name;
            //ID_bxx.Text = Infor_data.ID;
            //Time_all_bxx.Text = Time_all;
        }
        private void handlerselection3()
        {
            try
            {
                if (Is_Loading_Data) return;
                if (doc.Count <= 0) return;
                DataGridViewRow selectedRow = dataGridView4.CurrentRow; //dataGridView4.Rows[index];
                string getname_3 = Convert.ToString(selectedRow.Cells["nameDataGridViewTextBoxColumn2"].Value);
                Pos tmp = new Pos();
                tmp = binary_search(getname_3, 0, doc.Count() - 1);
                table1 table = get_infor_allday(tmp.start, tmp.stop, false);

                RecordData Infor_data3 = mongo.Get_AData(getname_3);
                name_bxx.Text = Infor_data3.Name;
                ID_bxx.Text = Infor_data3.ID;
                Time_all_bxx.Text = table.All_of_time;
                time_all_transfer_bxx.Text = table.All_of_time_TSpan.ToString();
                Overtime_bxx.Text = table.Overtime.ToString();
                from_bxx.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                to_bxx.Text = dateTimePicker2.Value.ToString("dd/MM/yyyy");
                string tam = Infor_data3.Sal_grade;
                if (tam == "1")
                {
                    Salperhour_bxx.Text = "40,000";
                    Otime_rate_bxx.Text = "1.5";
                    Fund_bxx.Text = "30,000";
                }
                if (tam == "2")
                {
                    Salperhour_bxx.Text = "42,000";
                    Otime_rate_bxx.Text = "1.6";
                    Fund_bxx.Text = "35,000";
                }
                if (tam == "3")
                {
                    Salperhour_bxx.Text = "44,100";
                    Otime_rate_bxx.Text = "1.7";
                    Fund_bxx.Text = "40,000";
                }
                if(tam == "4")
                {
                    Salperhour_bxx.Text = "46,305";
                    Otime_rate_bxx.Text = "1.8";
                    Fund_bxx.Text = "45,000";
                }
                if (tam == "5")
                {
                    Salperhour_bxx.Text = "48,620";
                    Otime_rate_bxx.Text = "1.9";
                    Fund_bxx.Text = "50,000";
                }
                Double timeall = (TimeSpan.Parse(time_all_transfer_bxx.Text)).TotalHours;
                //Console.WriteLine(timeall);
                Double overtime_all = (TimeSpan.Parse(Overtime_bxx.Text)).TotalHours;
                //Console.WriteLine(overtime_all);
                Double SalPerHour = Convert.ToDouble(Salperhour_bxx.Text);
                //Console.WriteLine(SalPerHour);
                Double OvertimeRate = Convert.ToDouble(Otime_rate_bxx.Text);
                //Console.WriteLine(OvertimeRate);
                Double Fund = Convert.ToDouble(Fund_bxx.Text);
                //Console.WriteLine(Fund);

                Double Total = ((timeall - overtime_all) * SalPerHour) + (overtime_all * SalPerHour * OvertimeRate) + Fund;
                //Console.WriteLine(Total);
                Total_sal_bxx.Text = string.Format("{0:#,###,###.##}", Total);

                /*
                //save to tinh luong
                save_tinh_cong save_tinh_cong = new save_tinh_cong();
                save_tinh_cong.Name = name_bxx.Text;
                save_tinh_cong.ID = ID_bxx.Text;
                save_tinh_cong.Time_all = Time_all_bxx.Text;
                save_tinh_cong.Overtime = Overtime_bxx.Text;
                save_tinh_cong.Sal_per_hour = Salperhour_bxx.Text;
                save_tinh_cong.Overtime_rate = Otime_rate_bxx.Text;
                save_tinh_cong.Fund = Fund_bxx.Text;
                save_tinh_cong.Total = Total_sal_bxx.Text;
                save_tinh_cong.From_date = from_bxx.Text;
                save_tinh_cong.To_date = to_bxx.Text;

                var Bson = JsonConvert.SerializeObject(save_tinh_cong);
                var temp = BsonSerializer.Deserialize<BsonDocument>(Bson);
                mongo.Insert_salary(temp);
                */
            }
            catch
            {
                MessageBox.Show("THIS OBJECT DOESN'T EXIST!!");
            }

        }
        private void handlerselection4()
        {
            try
            {
                if (Is_Loading_Data) return;
                DataGridViewRow selRow = dataGridView5.CurrentRow;
                string getname5 = Convert.ToString(selRow.Cells["nameDataGridViewTextBoxColumn3"].Value);
                string getID5 = Convert.ToString(selRow.Cells["iDDataGridViewTextBoxColumn4"].Value);
                string getFromDate5 = Convert.ToString(selRow.Cells["fromdateDataGridViewTextBoxColumn"].Value);
                string getToDate5 = Convert.ToString(selRow.Cells["todateDataGridViewTextBoxColumn"].Value);
                DFDSal.Name = getname5;
                DFDSal.ID = getID5;
                DFDSal.From_date = getFromDate5;
                DFDSal.To_date = getToDate5;
            }
            catch
            {
                MessageBox.Show("Blank");
            }        
        }
        

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            handlerselection();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            handlerselection();
        }

        private void recordDataBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void swap(IList<LoadData> list, int indexA, int indexB)  // swap 2 obj
        {
            var tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        private void quicksort(List<LoadData> list, int start, int stop) // quicksort algorithm
        {
            if (start >= stop)
                return;
            LoadData middle = list[(int)((start + stop) / 2)];

            int i = start;
            int j = stop;
            while (i<=j)
            {
                while (string.Compare(list[i].Name,middle.Name)<0 ||
                    (string.Compare(list[i].Name, middle.Name) == 0 && Convert.ToDateTime(list[i].Date) < Convert.ToDateTime(middle.Date)) ||
                    (string.Compare(list[i].Name, middle.Name)==0 && Convert.ToDateTime(list[i].Date) == Convert.ToDateTime(middle.Date) && Convert.ToDateTime(list[i].Time)< Convert.ToDateTime(middle.Time)))
                {
                    i++;
                }
                while (string.Compare(list[j].Name, middle.Name) > 0 ||
                    (string.Compare(list[j].Name, middle.Name) == 0 && (Convert.ToDateTime(list[j].Date) > Convert.ToDateTime(middle.Date))) ||
                    (string.Compare(list[j].Name, middle.Name) == 0 && Convert.ToDateTime(list[j].Date) == Convert.ToDateTime(middle.Date) && Convert.ToDateTime(list[j].Time) > Convert.ToDateTime(middle.Time)))
                {
                    j--;
                }
                if (i<=j)
                {
                    swap(list, i, j);
                    i++;
                    j--;
                }
            }
            quicksort(list, start, j);
            quicksort(list, i, stop);
        }

        private table2 get_info_1day(int start, bool display)
        {
            string Date = "";
            string InTime = "";
            string OutTime = "";
            string Name = "";

            int stop = start;
            while (stop+1<doc.Count()-1) // make sure that same name same date to calcu time
            {
                if (doc[stop + 1].Name != doc[start].Name)
                    break;
                if (doc[stop + 1].Date != doc[start].Date)
                    break;
                stop++;
            }

            DateTime In;
            DateTime Out;
            table2 tb;
            //insert tab1
            table1 tb1;
            TimeSpan TimeSet;
            TimeSpan Time_Sub;
            TimeSpan Time_After_Set;
            TimeSpan default_time_per_day = new TimeSpan(08, 00, 00);
            TimeSpan default_overtime = new TimeSpan(00, 00, 00);
            TimeSpan non = new TimeSpan(00, 00, 00);
            TimeSpan part_time = new TimeSpan(05, 00, 00);

            InTime = doc[start].Time;
            OutTime = doc[stop].Time;
            In = Convert.ToDateTime(InTime);
            Out = Convert.ToDateTime(OutTime);

            tb = new table2();
            tb.Date = doc[start].Date;
            tb.InTime = InTime;
            tb.OutTime = OutTime;
            TimeSet = new TimeSpan(01, 00, 00); //off 1h midday
            Time_Sub = Out.Subtract(In);
            if (Time_Sub <= part_time)
            {
                Time_Sub = Time_Sub.Add(TimeSet);
            }
            Time_After_Set = Time_Sub.Subtract(TimeSet);
            tb.TimePerDay = Time_After_Set;
            TimeSpan overtime = Time_After_Set.Subtract(default_time_per_day);
            if (overtime >= default_overtime)
            {
                tb.Overtime = overtime;
            }
            else
            {
                tb.Overtime = non;
            }
            
            //int x = (int)Time_After_Set.TotalHours; // convert timespan/day to int
            if (display)
                table2BindingSource.Add(tb);  //display on datagridview 2 - table 1

            return tb;
        }

        private table1 get_infor_allday(int start, int stop, bool display)
        {
            if (display)
                table2BindingSource.Clear();
            table2 tb;
            TimeSpan timesum = new TimeSpan(0, 0, 0);
            TimeSpan over_time_sum = new TimeSpan(0, 0, 0);
            for (int i=start+1;i<=stop;i++)
            {
                if (doc[i].Date != doc[i - 1].Date)
                {
                    tb = get_info_1day(start,display);
                    start = i;
                    timesum = timesum.Add(tb.TimePerDay);
                    over_time_sum = over_time_sum.Add(tb.Overtime);
                }

            }
            tb = get_info_1day(start,display);
            // calcu timesum
            timesum = timesum.Add(tb.TimePerDay);
            over_time_sum = over_time_sum.Add(tb.Overtime);
            string timesum_string= string.Format("{0}:{1}:{2}", (int)timesum.TotalHours, timesum.Minutes, timesum.Seconds);
            //string timesum_all= timesum.TotalHours.ToString();
            table1 tb1 = new table1();
            tb1.Name = doc[start].Name;
            tb1._id = doc[start]._id;
            tb1.All_of_time = timesum_string;
            tb1.Overtime = over_time_sum;
            tb1.All_of_time_TSpan = timesum;
            //Time_all = timesum_all;
            return tb1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Is_Loading_Data = true;
            doc.Clear();
            table2BindingSource.Clear(); //clear tab2 each time click load
            table1BindingSource.Clear();
            TimeSpan All_of_time = new TimeSpan(00, 00, 00);
            DateTime pick_value2 = dateTimePicker2.Value.Date;
            string Salperhour_box = "";
            string Otime_rate_box = "";
            string Fund_box = "";
            for (DateTime pick_value1 = dateTimePicker1.Value.Date ; pick_value1 <= pick_value2; pick_value1 = pick_value1.AddDays(1.0))
            {
                // pick intime and outime
                string value1 = pick_value1.ToString("MM/dd/yyyy");
                var docs = mongo.Load_cham_cong(value1);
                foreach (BsonDocument docu in docs)
                {

                    var data = BsonSerializer.Deserialize<LoadData>(docu);
                    doc.Add(data);
                }
            }
            quicksort(doc, 0, doc.Count() - 1);
            LoadData fake = new LoadData();
            fake.Name = "###";
            doc.Add(fake);

            Is_Loading_Data = false;
            int start = 0;
            for (int i=1; i<doc.Count();i++)
            {
                if (doc[i].Name!=doc[i-1].Name)
                {
                    
                    table1BindingSource.Add(get_infor_allday(start,i-1,false));
                    
                    
                    var a = get_infor_allday(start, i - 1, false);
                    RecordData Infor_data = mongo.Get_AData(a.Name);

                    string tam = Infor_data.Sal_grade;
                    if (tam == "1")
                    {
                        Salperhour_box = "40,000";
                        Otime_rate_box = "1.5";
                        Fund_box = "30,000";
                    }
                    if (tam == "2")
                    {
                        Salperhour_box = "42,000";
                        Otime_rate_box = "1.6";
                        Fund_box = "35,000";
                    }
                    if (tam == "3")
                    {
                        Salperhour_box = "44,100";
                        Otime_rate_box = "1.7";
                        Fund_box = "40,000";
                    }
                    if (tam == "4")
                    {
                        Salperhour_box = "46,305";
                        Otime_rate_box = "1.8";
                        Fund_box = "45,000";
                    }
                    if (tam == "5")
                    {
                        Salperhour_box = "48,620";
                        Otime_rate_box = "1.9";
                        Fund_box = "50,000";
                    }
                    Double timeall = (TimeSpan.Parse(a.All_of_time_TSpan.ToString())).TotalHours;
                    Double overtime_all = (TimeSpan.Parse(a.Overtime.ToString())).TotalHours;
                    Double SalPerHour = Convert.ToDouble(Salperhour_box);
                    Double OvertimeRate = Convert.ToDouble(Otime_rate_box);
                    Double Fund = Convert.ToDouble(Fund_box);
                    Double Total = ((timeall - overtime_all) * SalPerHour) + (overtime_all * SalPerHour * OvertimeRate) + Fund;
                    //save to tinh luong
                    save_tinh_cong save_tinh_cong = new save_tinh_cong();
                    save_tinh_cong.Name = Infor_data.Name;
                    save_tinh_cong.ID = Infor_data.ID;
                    save_tinh_cong.Time_all = a.All_of_time;
                    save_tinh_cong.Overtime = a.Overtime.ToString();
                    save_tinh_cong.Sal_per_hour = Salperhour_box;
                    save_tinh_cong.Overtime_rate = Otime_rate_box;
                    save_tinh_cong.Fund = Fund_box;
                    save_tinh_cong.Total = string.Format("{0:#,###,###.##}", Total);
                    save_tinh_cong.From_date = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    save_tinh_cong.To_date = dateTimePicker2.Value.ToString("dd/MM/yyyy");

                    var Bson = JsonConvert.SerializeObject(save_tinh_cong);
                    var temp = BsonSerializer.Deserialize<BsonDocument>(Bson);
                    mongo.Insert_salary(temp);
                    start = i;

                    
                }
            }
            
            Is_Loading_Data = true;
            savetinhcongcheckBindingSource.Clear();
            var stcs = mongo.Get_salary();
            foreach (BsonDocument stc in stcs)
            {
                save_tinh_cong_check data = BsonSerializer.Deserialize<save_tinh_cong_check>(stc);
                savetinhcongcheckBindingSource.Add(data);

            }
            Is_Loading_Data = false;
        }

        

         
        

        private void recordDataBindingSource_CurrentChanged_1(object sender, EventArgs e)
        {

        }



        private void table1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bt_add_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void mssvbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bt_check_Click(object sender, EventArgs e)
        {
            Is_Loading_Data = true;
            recordDataBindingSource.Clear();
            pictureBox1.Image = null;
            var DLs =mongo.Get_csdl();
            foreach(BsonDocument DL in DLs)
            {
                
                RecordData DL_data = BsonSerializer.Deserialize<RecordData>(DL);
                //DL_csdl.Add(DL_data);
                recordDataBindingSource.Add(DL_data);
                //string a = DL_data.ContentImage;
                //byte[] bytes = Encoding.ASCII.GetBytes(a);
                //Bitmap bit = Image.FromStream(new MemoryStream(bytes));
                //byte[] bytes = DL_data.ContentImage;
                //pictureBox1.Image = Image.FromStream(new MemoryStream(bytes));

            }
            Is_Loading_Data = false;
        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            handlerselection2();
        }
        private void DataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            handlerselection2();
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectId objectId = mongo.MongoGrid(name_of_file);
                Add_newData Infor_add = new Add_newData();
                Infor_add.ID = idbx.Text;
                Infor_add.Name = namebx.Text;
                Infor_add.Age = agebx.Text;
                Infor_add.Major = majorbx.Text;
                Infor_add.MSSV = mssvbx.Text;
                Infor_add.Phone = phonebx.Text;
                Infor_add.image_id = objectId;
                Infor_add.Sal_grade = sal_grade_bx.Text;


                var Bson = JsonConvert.SerializeObject(Infor_add);
                var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
                mongo.Insert_document(tmp);
                MessageBox.Show("ADDED !!!");
            }
            catch
            {
                MessageBox.Show("WAIT !!! FILL YOU NEW PROFILE AND PICK A PICTURE!!!");
            }
            
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            Add_newData Infor_add = new Add_newData();
            Infor_add.ID = idbx.Text;
            Infor_add.Name = namebx.Text;
            Infor_add.Age = agebx.Text;
            Infor_add.Major = majorbx.Text;
            Infor_add.MSSV = mssvbx.Text;
            Infor_add.Phone = phonebx.Text;
            Infor_add.image_id = GetId;
            Infor_add.Sal_grade = sal_grade_bx.Text;

            var Bson = JsonConvert.SerializeObject(Infor_add);
            var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
            mongo.Delete_document(tmp);
            mongo.Delete_image(Infor_add.image_id);
            recordDataBindingSource.RemoveCurrent();
            MessageBox.Show("DELETED !!!");
        }

        

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //handlerselection3();
        }
        private void DataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            Salperhour_bxx.Clear();
            Otime_rate_bxx.Clear();
            Fund_bxx.Clear();
            Total_sal_bxx.Clear();
            handlerselection3();
        }

        private void bt_calcu_Click(object sender, EventArgs e)
        {
            try
            {
                //tinh luong de update
                Double timeall = (TimeSpan.Parse(time_all_transfer_bxx.Text)).TotalHours;
                //Console.WriteLine(timeall);
                Double overtime_all = (TimeSpan.Parse(Overtime_bxx.Text)).TotalHours;
                //Console.WriteLine(overtime_all);
                Double SalPerHour = Convert.ToDouble(Salperhour_bxx.Text);
                //Console.WriteLine(SalPerHour);
                Double OvertimeRate = Convert.ToDouble(Otime_rate_bxx.Text);
                //Console.WriteLine(OvertimeRate);
                Double Fund = Convert.ToDouble(Fund_bxx.Text);
                //Console.WriteLine(Fund);
                
                Double Total = ((timeall - overtime_all) * SalPerHour) + (overtime_all * SalPerHour * OvertimeRate) + Fund;
                //Console.WriteLine(Total);
                Total_sal_bxx.Text = string.Format("{0:#,###,###.##}", Total);

                //save to bang luong
                save_tinh_cong save_tinh_cong = new save_tinh_cong();
                save_tinh_cong.Name = name_bxx.Text;
                save_tinh_cong.ID = ID_bxx.Text;
                save_tinh_cong.Time_all = Time_all_bxx.Text;
                save_tinh_cong.Overtime = Overtime_bxx.Text;
                save_tinh_cong.Sal_per_hour = Salperhour_bxx.Text;
                save_tinh_cong.Overtime_rate = Otime_rate_bxx.Text;
                save_tinh_cong.Fund = Fund_bxx.Text;
                save_tinh_cong.Total = Total_sal_bxx.Text;
                save_tinh_cong.From_date = from_bxx.Text;
                save_tinh_cong.To_date = to_bxx.Text;

                var Bson = JsonConvert.SerializeObject(save_tinh_cong);
                var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
                mongo.Insert_salary(tmp);

                Is_Loading_Data = true;
                savetinhcongcheckBindingSource.Clear();
                var stcs = mongo.Get_salary();
                foreach (BsonDocument stc in stcs)
                {
                    save_tinh_cong_check data = BsonSerializer.Deserialize<save_tinh_cong_check>(stc);
                    savetinhcongcheckBindingSource.Add(data);

                }
                Is_Loading_Data = false;

                MessageBox.Show("UPDATED & SAVED !!!");

                
            }
            catch
            {
                MessageBox.Show("ERROR OCCUR !!");
            }
            
        }

        private void bt_save_Click(object sender, EventArgs e)
        {/*
            save_tinh_cong save_tinh_cong = new save_tinh_cong();
            save_tinh_cong.Name = name_bxx.Text;
            save_tinh_cong.ID = ID_bxx.Text;
            save_tinh_cong.Time_all = Time_all_bxx.Text;
            save_tinh_cong.Overtime = Overtime_bxx.Text;
            save_tinh_cong.Sal_per_hour = Salperhour_bxx.Text;
            save_tinh_cong.Overtime_rate = Otime_rate_bxx.Text;
            save_tinh_cong.Fund = Fund_bxx.Text;
            save_tinh_cong.Total = Total_sal_bxx.Text;
            save_tinh_cong.From_date = from_bxx.Text;
            save_tinh_cong.To_date = to_bxx.Text;

            var Bson = JsonConvert.SerializeObject(save_tinh_cong);
            var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
            mongo.Insert_salary(tmp);
            MessageBox.Show("SAVED !!!");*/

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void bt_check_sal_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CHOOSE YES - REPLACE THE OLD ONE, OR  NO - SAVE WITH NEW NAME BY YOURSELF !!!");
            try
            {
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from gridview";
                // storing header part in Excel  
                for (int i = 1; i < dataGridView5.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView5.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView5.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView5.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView5.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // save the application  
                workbook.SaveAs("E:\\ExportFromApp.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();
            }
            catch
            {
                MessageBox.Show("YOU CHOSE NO, SO YOU CAN SAVE WITH OTHER NAMES AT EXCEL: FILE/SAVE AS.. !!!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            handlerselection4();
        }
        private void DataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            handlerselection4();
        }

        private void bt_del_sal_Click(object sender, EventArgs e)
        {
            mongo.Delete_sal(DFDSal.Name, DFDSal.ID, DFDSal.From_date, DFDSal.To_date);
            savetinhcongcheckBindingSource.RemoveCurrent();
        }

        private void bt_chose_pic_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    
                    Bitmap bit = new Bitmap(open.FileName);
                    name_of_file=open.FileName;
                    pictureBox1.Image = bit;                   
                }

        }
    }
    
    public class MongoDB
    {
        private const string MONGO_URI = "mongodb+srv://VuGiaBao:bao0902429190@cluster0-c4dmj.azure.mongodb.net/face_recognition?authSource=admin&replicaSet=Cluster0-shard-0&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=true";
        private const string DATABASE_NAME = "Attendance_checking";
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> CSDL_col;
        private IMongoCollection<BsonDocument> CSDL_image_col;
        private IMongoCollection<BsonDocument> Cham_cong_col;
        private IMongoCollection<BsonDocument> Cham_cong_load_col;
        private IMongoCollection<BsonDocument> Dang_nhap_col;
        private IMongoCollection<BsonDocument> Tinh_luong_col;

        public ObjectId MongoGrid(string fileName)
        {
            var server = client.GetServer();
            var database = server.GetDatabase(DATABASE_NAME);
            //var gridFs = new MongoGridFS(database);
            var gridFinfo = database.GridFS.Upload(fileName);
            return (ObjectId)gridFinfo.Id;        
        }
        public Stream GetFile(ObjectId id)
        {
            var server = client.GetServer();
            var database = server.GetDatabase(DATABASE_NAME);
            var file = database.GridFS.FindOneById(id);
            return file.OpenRead();
        }
        public void Connect()
        {
            client = new MongoClient(MONGO_URI);
            db = client.GetDatabase(DATABASE_NAME);
            CSDL_col = db.GetCollection<BsonDocument>("CSDL");
            CSDL_image_col = db.GetCollection<BsonDocument>("CSDL_image");
            Cham_cong_col = db.GetCollection<BsonDocument>("Cham_cong");
            Cham_cong_load_col = db.GetCollection<BsonDocument>("Cham_cong_load");
            Dang_nhap_col = db.GetCollection<BsonDocument>("Dang_nhap");
            Tinh_luong_col = db.GetCollection<BsonDocument>("Tinh_luong");
        }

        public RecordData Get_AData(string Name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", Name);
            var Document = CSDL_col.Find(filter).FirstOrDefault(); //bsondoc type

            RecordData objects = BsonSerializer.Deserialize<RecordData>(Document); //transfer to recordData type
            return objects;  // return recordData type

        }
        
        public List<BsonDocument> Get_csdl()
        {
            var Document3 = CSDL_col.Find(new BsonDocument()).ToList();
            return Document3;
        }
        public List<BsonDocument> Get_salary()
        {
            var Document4 = Tinh_luong_col.Find(new BsonDocument()).ToList();
            return Document4;
        }

        public List<BsonDocument> Load_cham_cong(string date)
        {
            var filter1 = Builders<BsonDocument>.Filter.Eq("Date", date);//& Builders<BsonDocument>.Filter.Eq("Name", name);
            var Document1 = Cham_cong_load_col.Find(filter1).ToList();
            return Document1;

        }

        public List<BsonDocument> Load_cham_cong_2(string date)
        {
            var filter2 = Builders<BsonDocument>.Filter.Eq("Date", date);
            var Document2 = Cham_cong_load_col.Find(filter2).ToList();
            return Document2;

        }

        public void Insert_document(BsonDocument name)
        {
            CSDL_col.InsertOne(name);
        }
        public void Insert_salary(BsonDocument name)
        {
            Tinh_luong_col.InsertOne(name);
        }

        public void Delete_document(BsonDocument name)
        {
            CSDL_col.DeleteOne(name);
        }
        public void Delete_image(ObjectId id)
        {
            var server = client.GetServer();
            var database = server.GetDatabase(DATABASE_NAME);
            database.GridFS.DeleteById(id);
        }
        public void Delete_sal(string name, string ID, string From_date, string To_date)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", name) & Builders<BsonDocument>.Filter.Eq("ID", ID) & Builders<BsonDocument>.Filter.Eq("From_date", From_date) & Builders<BsonDocument>.Filter.Eq("To_date", To_date);
            Tinh_luong_col.DeleteOne(filter);
        }


        public dang_nhap Dang_nhap(string Name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", Name);
            var Document = Dang_nhap_col.Find(filter).FirstOrDefault(); //bsondoc type

            dang_nhap objects = BsonSerializer.Deserialize<dang_nhap>(Document); //transfer to recordData type
            return objects;
        }

        public void Insert(BsonDocument name)
        {
            Dang_nhap_col.InsertOne(name);
        }
        
    }
}

public class RecordData
{
    public ObjectId _id { get; set; }
    public string ID { get; set; }
    public string Age { get; set; }
    public string MSSV { get; set; }
    public string Major { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Sal_grade { get; set; }
    public ObjectId image_id { get; set; }
}
public class Add_newData
{
    public string ID { get; set; }
    public string Age { get; set; }
    public string MSSV { get; set; }
    public string Major { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Sal_grade { get; set; }
    public ObjectId image_id { get; set; }

}
public class LoadData
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
}
public class table2
{
    public string Date { get; set; }
    public string InTime { get; set; }
    public string OutTime { get; set; }
    public TimeSpan TimePerDay { get; set; }
    public TimeSpan Overtime { get; set; }
}
public class table1
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string All_of_time { get; set; }
    public TimeSpan All_of_time_TSpan { get; set; }
    public TimeSpan Overtime { get; set; }

}

public class Pos
{
    public int start { get; set; }
    public int stop { get; set; }
}
public class dang_nhap
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string Pass { get; set; }

}
public class dang_ky
{
    public string Name { get; set; }
    public string Pass { get; set; }
}

public class save_tinh_cong
{
    public string Name { get; set; }
    public string ID { get; set; }
    public string Time_all { get; set; }
    public string Overtime { get; set; }
    public string Fund { get; set; }
    public string Sal_per_hour { get; set; }
    public string Overtime_rate { get; set; }
    public string Total { get; set; }
    public string From_date { get; set; }
    public string To_date { get; set; }

}
public class save_tinh_cong_check
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string ID { get; set; }
    public string Time_all { get; set; }
    public string Overtime { get; set; }
    public string Fund { get; set; }
    public string Sal_per_hour { get; set; }
    public string Overtime_rate { get; set; }
    public string Total { get; set; }
    public string From_date { get; set; }
    public string To_date { get; set; }

}
public class Doc_for_del_sal
{
    public string Name { get; set; }
    public string ID { get; set; }
    public string From_date { get; set; }
    public string To_date { get; set; }
}