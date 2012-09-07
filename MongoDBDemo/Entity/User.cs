using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;

namespace MongoDBDemo.Entity
{
    #region Entity

    class User
    {
        public ObjectId Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }

    #endregion
}
