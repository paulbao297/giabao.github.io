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
using Newtonsoft.Json;

namespace App
{
    public partial class Form1 : Form
    {
        public MongoDB mongo = new MongoDB();
        List<LoadData> doc = new List<LoadData>();
        private bool Is_Loading_Data = false;
        public Form1()
        {
            InitializeComponent();
            mongo.Connect();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            if (Is_Loading_Data) return;
            if (doc.Count <= 0) return;
            DataGridViewRow selectedRow = dataGridView2.CurrentRow; //dataGridView2.Rows[index];
            string getname = Convert.ToString(selectedRow.Cells["nameDataGridViewTextBoxColumn"].Value);
            Pos tmp = new Pos();
            tmp = binary_search(getname, 0, doc.Count() - 1);
            get_infor_allday(tmp.start, tmp.stop, true);

            //display infor base on getname value
            RecordData Infor_data = mongo.Get_AData(getname);
            namebx.Text = Infor_data.Name;
            idbx.Text = Infor_data.ID;
            phonebx.Text = Infor_data.Phone;
            agebx.Text = Infor_data.Age;
            mssvbx.Text = Infor_data.MSSV;
            majorbx.Text = Infor_data.Major;


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
                    (string.Compare(list[j].Name, middle.Name) == 0 && Convert.ToDateTime(list[j].Date) > Convert.ToDateTime(middle.Date)) ||
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
            Time_After_Set = Time_Sub.Subtract(TimeSet);
            tb.TimePerDay = Time_After_Set;
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
            for (int i=start+1;i<=stop;i++)
            {
                if (doc[i].Date != doc[i - 1].Date)
                {
                    tb = get_info_1day(start,display);
                    start = i;
                    timesum = timesum.Add(tb.TimePerDay);
                }

            }
            tb = get_info_1day(start,display);
            // calcu timesum
            timesum = timesum.Add(tb.TimePerDay);
            string timesum_string= string.Format("{0}:{1}:{2}", (int)timesum.TotalHours, timesum.Minutes, timesum.Seconds);
            table1 tb1 = new table1();
            tb1.Name = doc[start].Name;
            tb1._id = doc[start]._id;
            tb1.All_of_time = timesum_string;
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
            for (DateTime pick_value1 = dateTimePicker1.Value.Date ; pick_value1 <= pick_value2; pick_value1 = pick_value1.AddDays(1.0))
            {
                // pick intime and outime
                string value1 = pick_value1.ToString("dd/MM/yyyy");
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
                    start = i;
                }
            }
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
            Add_newData Infor_add = new Add_newData();
            Infor_add.ID = idbx.Text;
            Infor_add.Name = namebx.Text;
            Infor_add.Age = agebx.Text;
            Infor_add.Major = majorbx.Text;
            Infor_add.MSSV = mssvbx.Text;
            Infor_add.Phone = phonebx.Text;

            var Bson = JsonConvert.SerializeObject(Infor_add);
            var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
            mongo.Insert_document(tmp);

            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bit = new Bitmap(open.FileName);
                    //pictureBox1.Image = bit;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }

        }
    }

    public class MongoDB
    {
        private const string MONGO_URI = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "Attendance_checking";
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> CSDL_col;
        private IMongoCollection<BsonDocument> Cham_cong_col;
        private IMongoCollection<BsonDocument> Cham_cong_load_col;

        public void Connect()
        {
            client = new MongoClient(MONGO_URI);
            db = client.GetDatabase(DATABASE_NAME);
            CSDL_col = db.GetCollection<BsonDocument>("CSDL");
            Cham_cong_col = db.GetCollection<BsonDocument>("Cham_cong");
            Cham_cong_load_col = db.GetCollection<BsonDocument>("Cham_cong_load");
        }

        public  RecordData Get_AData(string Name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", Name);
            var Document = CSDL_col.Find(filter).FirstOrDefault(); //bsondoc type

            RecordData objects = BsonSerializer.Deserialize<RecordData>(Document); //transfer to recordData type
            return objects;  // return recordData type
            
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
}
public class Add_newData
{
    public string ID { get; set; }
    public string Age { get; set; }
    public string MSSV { get; set; }
    public string Major { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
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
}
public class table1
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string All_of_time { get; set; }

}

public class Pos
{
    public int start { get; set; }
    public int stop { get; set; }
}
