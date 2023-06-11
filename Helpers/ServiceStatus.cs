using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Helpers
{
    public enum ServiceStatus
    {
        Request,
        Started,
        Progress,
        Cancel,
        Completed
    }
}
