using Core.Utilities.UsableModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson.IO;
using System.Linq;

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
            if (inputs.Length == 0) return (decimal)-1.00;
            decimal sum = inputs.Sum();
            return (decimal)sum / inputs.Length;
        }

        public static decimal GetSpecialBetAverage(ComparisonGuessContainer inputOnlyDB, string propertyNames, params decimal[] inputs)
        {
            if(inputOnlyDB == null)
            {
                return (decimal)-899;
            }

            dynamic value = inputOnlyDB;

            foreach (var prop in propertyNames.Split("|"))
            {
                value = value.GetType().GetProperty(prop).GetValue(value, null);
            }

            inputs.Append((decimal)value);
            
            inputs = inputs.Where(x => x >= 0).ToArray();
            if (inputs.Length == 0) return (decimal)-1.00;
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

            var orderedInputs = inputs.OrderByDescending(x => x.Percentage).ToList();

            bool trOrFalse = inputs[0].FeatureName.ToLower() == "true" || inputs[0].FeatureName.ToLower() == "false";

            if (trOrFalse)
            {
                for (int i = 0; i < orderedInputs.Count - 1; i++)
                {
                    var currentInput = orderedInputs[i];
                    var nextInput = orderedInputs[i + 1];

                    if (nextInput.FeatureName != currentInput.FeatureName)
                    {
                        nextInput.FeatureName = currentInput.FeatureName;
                        nextInput.Percentage = (100 - nextInput.Percentage);
                    }
                }
            }

            List<PercentageComplainer> resultList = orderedInputs.GroupBy(x => x.FeatureName)
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
            {
                if (resultList[0].Percentage < 50 && trOrFalse)
                {
                    resultList[0].Percentage = 100 - resultList[0].Percentage;
                    resultList[0].FeatureName = (!Convert.ToBoolean(resultList[0].FeatureName)).ToString().ToUpper();
                }

                return resultList[0];
            }

            else
            {
                var higestItem = resultList[0];
                resultList.Remove(higestItem);

                int responseExtraCalculate = Convert.ToInt32(100 - resultList.Select(x => x.Percentage).Average());

                higestItem.Percentage = Convert.ToInt32(GetAverage(higestItem.Percentage, responseExtraCalculate));

                return higestItem;
            }
        }


        public static PercentageComplainer GetSpecialBetAverage(ComparisonGuessContainer comparisonDB, string propertyNames,  params PercentageComplainer[] inputs)
        {
            if (comparisonDB == null)
            {
                return new PercentageComplainer
                {
                    FeatureName = "false",
                    CountAll= 0,
                    Percentage = -899,
                    CountFound= 0,
                    PropertyName = "false"
                };
            }

            dynamic value = comparisonDB;

            foreach (var prop in propertyNames.Split("|"))
            {
                value = value.GetType().GetProperty(prop).GetValue(value, null);
            }

            inputs.Append((PercentageComplainer)value);

            inputs = inputs.Where(x => x != null).ToArray();
            inputs = inputs.Where(x => x.Percentage >= 0).ToArray();

            if (inputs.Length == 0) return null;

            var orderedInputs = inputs.OrderByDescending(x => x.Percentage).ToList();

            bool trOrFalse = inputs[0].FeatureName.ToLower() == "true" || inputs[0].FeatureName.ToLower() == "false";

            if (trOrFalse)
            {
                for (int i = 0; i < orderedInputs.Count - 1; i++)
                {
                    var currentInput = orderedInputs[i];
                    var nextInput = orderedInputs[i + 1];

                    if (nextInput.FeatureName != currentInput.FeatureName)
                    {
                        nextInput.FeatureName = currentInput.FeatureName;
                        nextInput.Percentage = (100 - nextInput.Percentage);
                    }
                }
            }

            List<PercentageComplainer> resultList = orderedInputs.GroupBy(x => x.FeatureName)
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
            {
                if (resultList[0].Percentage < 50 && trOrFalse)
                {
                    resultList[0].Percentage = 100 - resultList[0].Percentage;
                    resultList[0].FeatureName = (!Convert.ToBoolean(resultList[0].FeatureName)).ToString().ToUpper();
                }

                return resultList[0];
            }

            else
            {
                var higestItem = resultList[0];
                resultList.Remove(higestItem);

                int responseExtraCalculate = Convert.ToInt32(100 - resultList.Select(x => x.Percentage).Average());

                higestItem.Percentage = Convert.ToInt32(GetAverage(higestItem.Percentage, responseExtraCalculate));

                return higestItem;
            }
        }


        public static PercentageComplainer GetBetAverageNisbi(params PercentageComplainer[] inputs)
        {
            inputs = inputs.Where(x => x != null).ToArray();
            inputs = inputs.Where(x => x.Percentage >= 0).ToArray();

            if (inputs.Length == 0) return null;

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
            {
                return resultList[0];
            }

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
