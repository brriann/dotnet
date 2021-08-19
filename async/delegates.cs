using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingDelegates
{
    [TestClass]
    public class Test_Delegates
    {
        private void DoWork()
        {
            Debug.WriteLine("Hello World");
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
        }

        delegate void DoWorkDelegate();

        [TestMethod]
        public void Demo01()
        {
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            DoWorkDelegate m = new DoWorkDelegate(DoWork);

            AsyncCallback callback = new AsyncCallback(TheCallback);
            IAsyncResult ar = m.BeginInvoke(callback, m);
            // do more 

            ar.AsyncWaitHandle.WaitOne();
        }

        private static void TheCallback(IAsyncResult ar)
        {
            var m = ar.AsyncState as DoWorkDelegate;
            m.EndInvoke(ar); // this is where you use try/catch
        }
    }
}

namespace Networking
{
    [TestClass]
    public class Test_Download
    {
        string url = "http://deelay.me/5000/http://www.delsink.com";

        [TestMethod]
        public void Test_Download_DelsinkCOM()
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

namespace Databases
{
    [TestClass]
    public class Test_Databases
    {
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