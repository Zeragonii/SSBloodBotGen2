using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace SSBloodBotGen2
{
    public class UserModel
    {
        public int Id { get; set; }
        public long UID { get; set; }
        public string Name { get; set; }
    }
}
