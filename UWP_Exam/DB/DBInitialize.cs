using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Exam.Empty;

namespace UWP_Exam.DB
{
    public class DBInitialize
    {
        public static bool CreateContact()
        {
            SQLiteConnection conn = new SQLiteConnection("exam.db");
            string sql = @"CREATE TABLE IF NOT EXISTS PersonalContact
            (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            Name VARCHAR( 140 ),
            Phone CHAR(15) NOT NULL UNIQUE
            );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }
        public static bool InsertContact(Person personal)
        {
            SQLiteConnection conn = new SQLiteConnection("exam.db");
            try
            {
                using (var personalTransaction =
                    conn.Prepare("INSERT INTO PersonalContact" +
                    "(Name, Phone) VALUES(?, ? )"))
                {
                    personalTransaction.Bind(1, personal.Name);
                    personalTransaction.Bind(2, personal.Phone);

                    personalTransaction.Step();
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

        }

        public static List<Person> GetList()
        {
            SQLiteConnection conn = new SQLiteConnection("exam.db");
            //Person person = null;
            List<Person> listPerson = new List<Person>();
            try
            {
                using (var statement = conn.Prepare("SELECT * FROM PersonalContact  "))
                {
                    while (statement.Step() == SQLitePCL.SQLiteResult.ROW)
                    {
                        var person = new Person();
                        person.Name = (string)statement[1];
                        person.Phone = (string)statement[2];

                        listPerson.Add(person);
                    }
                }
                return listPerson;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error list" + e);
                return null;
            }
        }

        public static List<Person> ListTransactionByName(string name)
        {
            var list = new List<Person>();
            try
            {
                var cnn = new SQLiteConnection("exam.db");
                using (var stt = cnn.Prepare($"SELECT * FROM PersonalContact WHERE Name Like '%{name}%' LIMIT 1"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new Person()
                        {
                            Name = (string)stt["Name"],
                            Phone = (string)stt["Phone"],
                        };
                        list.Add(personal);
                    }
                }
                //Debug.WriteLine(list[0]);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }
    }
    
}
