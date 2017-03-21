using System;
using System.Collections.Generic;

namespace Forecast.Manager
{
    public class BaseManager
    {
        protected List<Tuple<string, Exception>> errorCollection; // stores a list of errors, with a tuple - the first is user friendly, the second is the exception
    }
}