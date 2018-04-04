using System;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class XMLAnalysisException : Exception
    {

        public XMLAnalysisException() : base() { }

        public XMLAnalysisException(string excptionMessage) : base(excptionMessage) { }
    }
}
