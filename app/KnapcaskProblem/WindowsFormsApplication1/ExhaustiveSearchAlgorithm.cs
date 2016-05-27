using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GenAlgorithm
{
    class ExhaustiveSearchAlgorithm
    {
        private ADataInstances  _data;
        public ExhaustiveSearchAlgorithm (ADataInstances data)
        {
            _data = data;
        }
        public int Run()
        {
            int itemsCount = _data.WEIGHT.Length;
            int limit = _data.MAX_WEIGHT;
            int[,] K = new int[itemsCount + 1, limit + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= limit; ++w)
                {
                    if (i == 0 || w == 0)
                        K[i, w] = 0;
                    else if (_data.WEIGHT[i - 1] <= w)
                        K[i, w] = Math.Max(_data.COST[i - 1] + K[i - 1, w - _data.WEIGHT[i - 1]], K[i - 1, w]);
                    else
                        K[i, w] = K[i - 1, w];
                }
            }

            return K[itemsCount, _data.MAX_WEIGHT];
        }
    }

//    class GrayCodeAlgorithm
//    {
//        private ADataInstances _data;
//        public GrayCodeAlgorithm(ADataInstances data)
//        {
//            _data = data;
//        }
//        public int Run()
//        {
//            int limit = _data.MAX_WEIGHT;        // размер ранца
//            int subjectsNumber = _data.WEIGHT.Length;    // количество предметов

//            int nIterations = 1 << subjectsNumber; // количество комбинаций, 2^n
//            int cost = 0;                   // текущий вес вещей
//            int weight = 0;
//            int bestCost = 0;               // лучший вес (максимальный без перегрузки)
//            BitArray mask = new BitArray(subjectsNumber);
//            BitArray bestMask = new BitArray(subjectsNumber);        // лучшая комбинация выбранных вещей
//            for (int i = 1; i < nIterations; i++)
//            {
//                int position = 0;    // какой по счёту бит инвертируем
//                //_BitScanForward(&position, i);
//                mask.Set(position, !mask.Get(position));
//                cost += mask.Get(position) ? _data.COST[position] : _data.COST[position] * (-1);
//                weight += mask.Get(position) ? _data.WEIGHT[position] : _data.WEIGHT[position] * (-1);
//                if (weight > limit)          // перегруз
//                    continue;
//                if (cost > bestCost)         // Отличный вариант! Надо запомнить...
//                {
//                    bestCost = cost;
//                }
//            }
//            return bestCost;
//        }
//    }
}
