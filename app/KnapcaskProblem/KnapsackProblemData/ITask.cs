using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackProblem
{
    public interface ITask
    {
        IData Create(IData data);
        string Str();
    }
}
