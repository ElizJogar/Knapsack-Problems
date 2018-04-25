using KnapsackProblem;

namespace CorrectnessTests
{
    public interface ITest
    {
        IData Data();
        long Gold();
        string Name();
    }
    class TestData: AData
    {
        public TestData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
           : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {}
        public override string Str()
        {
            return "correctness_test";
        }
    }
    class Test: ITest
    {
        private IData m_data;
        private long m_gold;
        private string m_name;

        public Test(ITask task, IData data, long gold, string name)
        {
            m_data = task.Create(data);
            m_gold = gold;
            m_name = name;
        }
        public IData Data()
        {
            return m_data;
        }
        public long Gold()
        {
            return m_gold;
        }
        public string Name()
        {
            return m_name;
        }
    }
}
