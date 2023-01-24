using Core.Utilities.UsableModel;

namespace Core.Utilities.Helpers
{
    public class Calculator
    {
        public decimal BetAverage(params decimal[] inputs)
        {
            inputs = inputs.Where(x => x >= 0).ToArray();
            if (inputs.Length == 0) return (decimal)-1.00;
            decimal sum = inputs.Sum();
            return (decimal)sum / inputs.Length;
        }

        public static decimal GetBetAverage(params decimal[] inputs)
        {
            inputs = inputs.Where(x => x >= 0).ToArray();
            if(inputs.Length == 0) return (decimal) -1.00;
            decimal sum = inputs.Sum();
            return (decimal)sum / inputs.Length;
        }



        public PercentageComplainer BetAverage(params PercentageComplainer[] inputs)
        {
            inputs = inputs.Where(x => x != null).ToArray();
            inputs = inputs.Where(x => x.Percentage >= 0).ToArray();

            if (inputs.Length == 0) return null;

            if (inputs[0].FeatureName.ToLower() == "true" || inputs[0].FeatureName.ToLower() == "false")
            {
                inputs = inputs.OrderByDescending(x => x.Percentage).ToArray();

                var existProperties = new List<string>();

                for (int i = 0; i < inputs.Length - 1; i++)
                {
                    var currentInput = inputs[i];
                    var nextInput = inputs[i + 1];

                    if (nextInput.FeatureName != currentInput.FeatureName)
                    {
                        nextInput.FeatureName = currentInput.FeatureName;
                        nextInput.Percentage = 100 - nextInput.Percentage;
                    }
                }
            }

            List<PercentageComplainer> resultList = inputs.GroupBy(x => x.FeatureName)
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = Convert.ToInt32(g.Average(a => a.Percentage)),
                              CountFound = g.Count(),
                              CountAll = inputs.Length,
                              FeatureName = g.Key,
                              PropertyName = g.Key
                          }).ToList().OrderByDescending(x => x.Percentage).ToList();

            if (resultList.Count == 0)
                return null;

            else if (resultList.Count == 1)
                return resultList[0];

            else
            {
                var higestItem = resultList[0];
                resultList.Remove(higestItem);

                int responseExtraCalculate = Convert.ToInt32(100 - resultList.Select(x => x.Percentage).Average());

                higestItem.Percentage = Convert.ToInt32(Average(higestItem.Percentage, responseExtraCalculate));

                return higestItem;
            }
        }

        public static PercentageComplainer GetBetAverage(params PercentageComplainer[] inputs)
        {
            inputs = inputs.Where(x => x != null).ToArray();
            inputs = inputs.Where(x => x.Percentage >= 0).ToArray();

            if (inputs.Length == 0) return null;

            if (inputs[0].FeatureName.ToLower() == "true" || inputs[0].FeatureName.ToLower() == "false")
            {
                inputs = inputs.OrderByDescending(x => x.Percentage).ToArray();

                var existProperties = new List<string>();

                for (int i = 0; i < inputs.Length-1; i++)
                {
                    var currentInput = inputs[i];
                    var nextInput = inputs[i+1];

                    if(nextInput.FeatureName != currentInput.FeatureName)
                    {
                        nextInput.FeatureName = currentInput.FeatureName;
                        nextInput.Percentage = 100 - nextInput.Percentage;
                    }
                }
            }

            List<PercentageComplainer> resultList = inputs.GroupBy(x => x.FeatureName)
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = Convert.ToInt32(g.Average(a=>a.Percentage)),
                              CountFound = g.Count(),
                              CountAll = inputs.Length,
                              FeatureName = g.Key,
                              PropertyName = g.Key
                          }).ToList().OrderByDescending(x => x.Percentage).ToList();

            if (resultList.Count == 0)
                return null;

            else if(resultList.Count == 1)
                return resultList[0];

            else
            {
                var higestItem = resultList[0];
                resultList.Remove(higestItem);

                int responseExtraCalculate = Convert.ToInt32(100 - resultList.Select(x => x.Percentage).Average());

                higestItem.Percentage = Convert.ToInt32(GetAverage(higestItem.Percentage, responseExtraCalculate));

                return higestItem;
            }
        }


        public static double GetAverage(params int[] values)
        {
            return values.Average();
        }

        public double Average(params int[] values)
        {
            return values.Average();
        }


        public decimal BetAverage(params int[] inputs)
        {
            inputs = inputs.Where(x => x >= 0).ToArray();
            if (inputs.Length == 0) return (decimal)-1.00;
            decimal sum = inputs.Sum();
            return (decimal)sum / inputs.Length;
        }

        public static decimal GetBetAverage(params int[] inputs)
        {
            inputs = inputs.Where(x => x >= 0).ToArray();
            if (inputs.Length == 0) return (decimal)-1.00;
            decimal sum = inputs.Sum();
            return (decimal)sum / inputs.Length;
        }
    }
}
