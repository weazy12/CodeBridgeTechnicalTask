using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace TechnicalTask.BLL.Mediatr.ResultVariation
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}
