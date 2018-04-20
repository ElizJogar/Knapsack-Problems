namespace KnapsackProblem
{
    public class KPTask: ITask
    {
        public IData Create(IData data)
        {
            data.Fill();
            for (var i = 0; i < data.ItemMaxCounts.Length; ++i)
            {
                data.ItemMaxCounts[i] = 1;
            }
            return data;
        }
        public string Str()
        {
            return "01KP";
        }
    }
}
