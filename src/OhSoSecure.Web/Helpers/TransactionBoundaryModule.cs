using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using OhSoSecure.Core.DataAccess;
using OhSoSecure.Core.IoC;

namespace OhSoSecure.Web.Helpers
{

    public class TransactionBoundaryModule : IHttpModule
    {
        Regex contentMatch = new Regex("/(content|scripts)/", RegexOptions.IgnoreCase); 

        public void Init(HttpApplication context)
        {
            context.BeginRequest += StartNewTransaction;
            context.EndRequest += EndCurrentTransaction;
        }

        public void Dispose() { }

        void StartNewTransaction(object sender, EventArgs e)
        {
            if (IsContentRequest(sender)) return;
            var tran = DependencyResolver.Current.GetService<ITransactionBoundary>();
            tran.Begin();
        }

        void EndCurrentTransaction(object sender, EventArgs e)
        {
            if (IsContentRequest(sender)) return;
            var tran = DependencyResolver.Current.GetService<ITransactionBoundary>();
            try
            {
                tran.Commit();
            }
            catch
            {
                tran.RollBack();
                throw;
            }
            finally
            {
                tran.Dispose();
            }
        }

        bool IsContentRequest(object sender)
        {
            var app = (HttpApplication)sender;
            return contentMatch.IsMatch(app.Context.Request.Path);
        }
    }
}