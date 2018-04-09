namespace Algorithm
{
    public class Individ
    {
        private int m_size;
        public int[] GENOTYPE
        {
            get; set;
        }
        public int SIZE
        {
            get { return m_size; }
        }
        public Individ(int size)
        {
            m_size = size;
            GENOTYPE = new int[m_size];
        }

        public override bool Equals(object obj)
        {
            int sum = 0;
            Individ newIndivid = (Individ)obj;
            for (int i = 0; i < GENOTYPE.Length; i++)
                sum += GENOTYPE[i] ^ newIndivid.GENOTYPE[i];
            return sum == 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string Str()
        {
            return string.Join("", GENOTYPE);
        }
    }

}
