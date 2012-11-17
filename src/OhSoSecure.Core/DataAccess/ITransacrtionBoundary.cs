using System;
using NHibernate;

namespace OhSoSecure.Core.DataAccess
{
    public interface ITransactionBoundary : IDisposable
    {
        ISession CurrentSession { get; }
        void Begin();
        void Commit();
        void RollBack();
    }

    public class NHibernateTransactionBoundary : ITransactionBoundary
    {
        readonly ISessionSource _sessionSource;
        bool _begun;
        bool _disposed;
        bool _rolledBack;
        ITransaction _transaction;

        public NHibernateTransactionBoundary(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
        }

        public void Begin()
        {
            CheckIsDisposed();

            CurrentSession = _sessionSource.CreateSession();

            BeginNewTransaction();
            _begun = true;
        }

        public void Commit()
        {
            CheckIsDisposed();
            CheckHasBegun();

            if (_transaction.IsActive && !_rolledBack)
            {
                _transaction.Commit();
            }

            BeginNewTransaction();
        }

        public void RollBack()
        {
            CheckIsDisposed();
            CheckHasBegun();

            if (_transaction.IsActive)
            {
                _transaction.Rollback();
                _rolledBack = true;
            }

            BeginNewTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ISession CurrentSession { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_begun || _disposed)
                return;

            if (disposing)
            {
                _transaction.Dispose();
                CurrentSession.Dispose();
            }

            _disposed = true;
        }

        void BeginNewTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = CurrentSession.BeginTransaction();
        }

        void CheckHasBegun()
        {
            if (!_begun)
                throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
        }

        void CheckIsDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}