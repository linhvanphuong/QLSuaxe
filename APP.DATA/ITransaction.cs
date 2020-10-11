using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
