﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connection
{
    /// <inheritdoc/>
    public interface IReadWriteDbConnection<DB> : IReadWriteDbConnection, IReadDbConnection<DB>
    {

    }
}
