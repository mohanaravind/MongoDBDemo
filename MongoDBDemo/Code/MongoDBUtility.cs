using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

using MongoDBDemo.Entity;

namespace MongoDBDemo.Code
{
    class DBUtility
    {
        String _strIPAddress = "127.0.0.1";
        String _strPort = "27017";

        /// <summary>
        /// Method to get the Mongo Server reference
        /// </summary>
        /// <returns>MongoServer</returns>
        private MongoServer getServer()
        {
            //Declarations
            StringBuilder sbConnectionString = new StringBuilder();

            //Construct the connection string
            sbConnectionString.Append("mongodb://");
            sbConnectionString.Append(_strIPAddress);
            sbConnectionString.Append(":");
            sbConnectionString.Append(_strPort);
            sbConnectionString.Append("/?safe=true");

            return MongoServer.Create(sbConnectionString.ToString());
        }

        /// <summary>
        /// Returns the names of database present in the server
        /// </summary>
        /// <returns></returns>
        public List<String> getDataBaseNames()
        {
            return getServer().GetDatabaseNames().ToList<String>();
        }

        /// <summary>
        /// Returns the tables present in this database
        /// </summary>
        /// <param name="strDataBaseName"></param>
        /// <returns></returns>
        public List<String> getTableNames(String strDataBaseName)
        {
            //Declarations
            MongoDatabase objMongoDatabase = getServer().GetDatabase(strDataBaseName);

            return objMongoDatabase.GetCollectionNames().ToList<String>();
        }

        /// <summary>
        /// Returns the table data
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public DataTable getData(String strDataBaseName, String strTableName)
        {
            //Declarations
            DataTable dtData = new DataTable();
            MongoCollection<User> colData = getServer().GetDatabase(strDataBaseName).GetCollection<User>(strTableName);
            MongoCursor<User> curData = colData.FindAll();

            //Set the columns            
            dtData.Columns.Add("First Name", typeof(String));
            dtData.Columns.Add("Last Name", typeof(String));
            dtData.Columns.Add("Id", typeof(String));

            try
            {
                //Create the data for the viewer
                foreach (User objUser in curData)
                {
                    DataRow drRow = dtData.NewRow();

                    drRow[0] = objUser.FirstName;
                    drRow[1] = objUser.LastName;
                    drRow[2] = objUser.Id;

                    //Add the row
                    dtData.Rows.Add(drRow);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



            return dtData;

        }

        /// <summary>
        /// Adds the Data base
        /// </summary>
        /// <param name="strDBName"></param>
        /// <returns></returns>
        public Boolean addData(String strDBName, String strTableName, User objData)
        {
            //Declarations
            Boolean blnFlag = false;
            MongoDatabase objDB = getServer().GetDatabase(strDBName);

            try
            {
                //Get the table
                MongoCollection objTable = objDB.GetCollection(strTableName);

                //Insert the data
                objTable.Insert(objData);
                objTable.Save(objData);
                blnFlag = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return blnFlag;
        }
    }
}
