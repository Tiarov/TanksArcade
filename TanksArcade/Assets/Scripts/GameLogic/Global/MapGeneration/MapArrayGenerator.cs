using UnityEngine;

namespace Assets.Scripts.GameLogic.Global
{
    public class MapArrayGenerator
    {
        public int[,] BuildArray(int x, int y, int minSpaceinterval = 1, int maxSpaceInterval = 1, int countOfWalls = 1, float maxSpacePercentInLine = 0.8f, float minSpacePercentInLine = 0.6f)
        {
            var array = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                        array[i, j] = -1;
                    else
                        array[i, j] = 0;
                }
            }

            for (int i = 0; i < y - 1; i++)
            {
                var interval = Random.Range(minSpaceinterval, maxSpaceInterval + 1);
                i += interval;

                var lenght = (int)Random.Range(x - x * maxSpacePercentInLine, x - x * minSpacePercentInLine);
                int dist = 0;
                int sum = 0;
                for (int k = 0; k < countOfWalls; k++)
                {
                    var coef = (countOfWalls - k);
                    int localLenght = (k == coef) ? lenght - sum : (int)(Random.Range((lenght / countOfWalls) * 0.5f, (lenght - sum) / coef));
                    var position = Random.Range(dist + Random.Range(1, (x - dist) / coef), ((x - dist) / coef));
                    dist = position + localLenght;
                    for (int j = position; j < dist; j++)
                    {
                        if (j < x && i < y && array[j, i] != -1)
                            array[j, i] = -1;
                    }
                    sum += localLenght;
                }
            }
            return array;
        }
    }
}
