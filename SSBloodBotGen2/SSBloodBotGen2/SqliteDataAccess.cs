using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using Dapper;

namespace SSBloodBotGen2
{
    public class SqliteDataAccess
    {
        public static List<UserModel> LoadUser()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UserModel>("select * from User", new DynamicParameters());
                Console.WriteLine(output);
                return output.ToList();
            }
        }

        public static void SaveUser(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into User (UID, Name) values (@UID, @Name)", user);
            }
        }
        
        public static void SaveDonation(DonationModel donation)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into DonationLog (Donation, UID, Nickname, Target) values (@Donation, @UID, @Nickname, @Target)", donation);
            }
        }

        public static void FetchDonation()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("select * from DonationLog");
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
