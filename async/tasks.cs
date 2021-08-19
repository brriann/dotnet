using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Databases
{
    [TestClass]
    public class Test_Databases
    {
        [TestMethod]
        public void Test_DB_Sync()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(sqlSelect, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = reader[0].ToString();
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Test_DB_Async()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlCommand = new SqlCommand(sqlSelect, sqlConnection);
                var callback = new AsyncCallback(DataAvailable);
                var ar = sqlCommand.BeginExecuteReader(callback, sqlCommand);

                ar.AsyncWaitHandle.WaitOne();
            }
        }

        private static void DataAvailable(IAsyncResult ar)
        {
            var sqlCommand = ar.AsyncState as SqlCommand;
            using (var reader = sqlCommand.EndExecuteReader(ar))
            {
                while (reader.Read())
                {
                    var data = reader[0].ToString();
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Networking
{
    [TestClass]
    public class Test_Download
    {
        string url = "http://deelay.me/5000/http://www.delsink.com";

        [TestMethod]
        public void Test_Download_delsinkdotcom_Synchronous()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var httpResponseInfo = httpRequestInfo.GetResponse();

            var responseStream = httpResponseInfo.GetResponseStream();
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }

        [TestMethod]
        public void Test_Download_delsinkdotcom_BeginEnd()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var callback = new AsyncCallback(HttpResponseAvailable);
            var ar = httpRequestInfo.BeginGetResponse(callback, httpRequestInfo);

            ar.AsyncWaitHandle.WaitOne();
        }
        private static void HttpResponseAvailable(IAsyncResult ar)
        {
            var httpRequestInfo = ar.AsyncState as HttpWebRequest;
            var httpResponseInfo = httpRequestInfo.EndGetResponse(ar) as HttpWebResponse;

            var responseStream = httpResponseInfo.GetResponseStream(); // downloading the page contents
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }
    }
}


//
// DEMO: Task.ContinueWith chaining, Network
//

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Networking
{
    [TestClass]
    public class Test_Download
    {
        string url = "http://deelay.me/5000/http://www.delsink.com";

        [TestMethod]
        public void Test_Download_delsinkdotcom_Synchronous()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var httpResponseInfo = httpRequestInfo.GetResponse();

            var responseStream = httpResponseInfo.GetResponseStream();
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }

        [TestMethod]
        public void Test_Download_delsinkdotcom_BeginEnd()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            var callback = new AsyncCallback(HttpResponseAvailable);
            var ar = httpRequestInfo.BeginGetResponse(callback, httpRequestInfo);

            ar.AsyncWaitHandle.WaitOne();
        }
        private static void HttpResponseAvailable(IAsyncResult ar)
        {
            var httpRequestInfo = ar.AsyncState as HttpWebRequest;
            var httpResponseInfo = httpRequestInfo.EndGetResponse(ar) as HttpWebResponse;

            var responseStream = httpResponseInfo.GetResponseStream(); // downloading the page contents
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }

        [TestMethod]
        public void Test_Download_DelsinkCOM_AsyncTask()
        {
            var httpRequestInfo = HttpWebRequest.CreateHttp(url);
            Task<WebResponse> taskWebResponse = httpRequestInfo.GetResponseAsync();
            Task taskContinuation = taskWebResponse.ContinueWith(HttpResponseContinuation,
                TaskContinuationOptions.OnlyOnRanToCompletion);

            Task.WaitAll(taskWebResponse, taskContinuation);
        }
        private static void HttpResponseContinuation(Task<WebResponse> taskResponse)
        {
            var httpResponseInfo = taskResponse.Result as HttpWebResponse;

            var responseStream = httpResponseInfo.GetResponseStream(); // downloading the page contents
            using (var sr = new StreamReader(responseStream))
            {
                var webPage = sr.ReadToEnd();
            }
        }
    }
}



//
// DEMO: Task.ContinueWith chaining, Database
//

using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Databases
{
    [TestClass]
    public class Test_Databases
    {
        [TestMethod]
        public void Test_DB_Sync()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(sqlSelect, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = reader[0].ToString();
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Test_DB_Async()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlCommand = new SqlCommand(sqlSelect, sqlConnection);
                var callback = new AsyncCallback(DataAvailable);
                var ar = sqlCommand.BeginExecuteReader(callback, sqlCommand);

                ar.AsyncWaitHandle.WaitOne();
            }
        }

        private static void DataAvailable(IAsyncResult ar)
        {
            var sqlCommand = ar.AsyncState as SqlCommand;
            using (var reader = sqlCommand.EndExecuteReader(ar))
            {
                while (reader.Read())
                {
                    var data = reader[0].ToString();
                }
            }
        }

        [TestMethod]
        public void Test_DB_AsyncTask()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            var sqlConnection = new SqlConnection(connectionString);
            Task taskSqlConnection = sqlConnection.OpenAsync();

            taskSqlConnection.ContinueWith((Task tx, object state) =>
            {
                var sqlConn = state as SqlConnection;
                Assert.IsTrue(sqlConn.State == System.Data.ConnectionState.Open);

                var sqlCommand = new SqlCommand(sqlSelect, sqlConn);
                Task<SqlDataReader> taskDataReader = sqlCommand.ExecuteReaderAsync();
                Task taskProcessData = taskDataReader.ContinueWith((Task<SqlDataReader> txx) =>
                {
                    using (var reader = txx.Result)
                    {
                        while (reader.Read())
                        {
                            var data = reader[0].ToString();
                        }

                        mre.Set();
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }, sqlConnection, TaskContinuationOptions.OnlyOnRanToCompletion);

            mre.WaitOne();
        }
        ManualResetEvent mre = new ManualResetEvent(false);
    }
}


//
// DEMO: Task Parallel library
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int ix = 0; ix < 100; ix++)
            //{
            //    Console.Write(ix);
            //}

            //Parallel.For(0, 100, (ix) => { Console.Write(ix); });

            var numbers = new List<int>();
            for (int ix = 0; ix < 100; ix++)
            {
                numbers.Add(ix);
            }
            Parallel.ForEach<int>(numbers, (ix) => { });
        }
    }
}






