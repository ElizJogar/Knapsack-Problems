using KnapsackProblem;
using Algorithm;

namespace ExcelReport
{
    public interface IGAOperatorsFactory
    {
        IInitialPopulation GetInitialPopulation();
        ICrossover GetCrossover();
        IMutation GetMutation();
        ISelection GetSelection();
    }
    public class KPUncorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new DantzigAlgorithm();
        }
        public ICrossover GetCrossover()
        {
            return new UniformCrossover();
        }
        public IMutation GetMutation()
        {
            return new PointMutation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class KPWeaklyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new UniformCrossover();
        }
        public IMutation GetMutation()
        {
            return new PointMutation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class KPStronglyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new DantzigAlgorithm();
        }
        public ICrossover GetCrossover()
        {
            return new TwoPointCrossover();
        }
        public IMutation GetMutation()
        {
            return new Inversion();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class KPSubsetSumDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new DantzigAlgorithm();
        }
        public ICrossover GetCrossover()
        {
            return new UniformCrossover();
        }
        public IMutation GetMutation()
        {
            return new Saltation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class KPVeryVeryStronglyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new DantzigAlgorithm();
        }
        public ICrossover GetCrossover()
        {
            return new TwoPointCrossover();
        }
        public IMutation GetMutation()
        {
            return new Inversion();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class UKPUncorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new SinglePointCrossover();
        }
        public IMutation GetMutation()
        {
            return new Saltation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class UKPWeaklyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new SinglePointCrossover();
        }
        public IMutation GetMutation()
        {
            return new PointMutation();
        }
        public ISelection GetSelection()
        {
            return new BettaTournament(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class UKPStronglyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new SinglePointCrossover();
        }
        public IMutation GetMutation()
        {
            return new Saltation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class UKPSubsetSumDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new UniformCrossover();
        }
        public IMutation GetMutation()
        {
            return new Saltation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }
    public class UKPVeryVeryStronglyCorrDataFactory : IGAOperatorsFactory
    {
        public IInitialPopulation GetInitialPopulation()
        {
            return new RandomPopulation();
        }
        public ICrossover GetCrossover()
        {
            return new SinglePointCrossover();
        }
        public IMutation GetMutation()
        {
            return new Saltation();
        }
        public ISelection GetSelection()
        {
            return new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
    }

    public static class Factory
    {
        public static IGAOperatorsFactory Create(ITask task, IData data)
        {
            if (task as KPTask != null)
            {
                if (data as UncorrData != null)
                {
                    return new KPUncorrDataFactory();
                }
                if (data as WeaklyCorrData != null)
                {
                    return new KPWeaklyCorrDataFactory();
                }
                if (data as StronglyCorrData != null)
                {
                    return new KPStronglyCorrDataFactory();
                }
                if (data as SubsetSumData != null)
                {
                    return new KPSubsetSumDataFactory();
                }
                if (data as VeryVeryStronglyCorrData != null)
                {
                    return new KPVeryVeryStronglyCorrDataFactory();
                }
            }
            if (task as UKPTask != null)
            {
                if (data as UncorrData != null)
                {
                    return new UKPUncorrDataFactory();
                }
                if (data as WeaklyCorrData != null)
                {
                    return new UKPWeaklyCorrDataFactory();
                }
                if (data as StronglyCorrData != null)
                {
                    return new UKPStronglyCorrDataFactory();
                }
                if (data as SubsetSumData != null)
                {
                    return new UKPSubsetSumDataFactory();
                }
                if (data as VeryVeryStronglyCorrData != null)
                {
                    return new UKPVeryVeryStronglyCorrDataFactory();
                }
            }
            return null;
        }
    }
}
