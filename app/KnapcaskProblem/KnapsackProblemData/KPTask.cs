using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackProblem
{
    public class KPTask: ITask
    {
        public IData Create(IData data)
        {
            data.Fill();
            return data;
        }
        public string Str()
        {
            return "0-1KP";
        }
    }
}
