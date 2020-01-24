using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class Stock : Shape
    {
        public int Width;
        public int Depth;
        public int HeightRemaining = 0;
        public int MinHeight = 40;
        [HideInInspector]
        bool roofRowOrNot = false;

        public void Initialize(int Width, int Depth, int HeightRemaining, int MinHeight)
        {
            this.Width = Width;
            this.Depth = Depth;
            this.HeightRemaining = HeightRemaining;
            this.MinHeight = MinHeight;
        }

        protected override void ExecuteAsianRoof()
        {
            
        }

        private void AsianWallRoof(BuildingParameters param)
        {
            int[] localPattern = { 1, 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                Vector3 localPosition = new Vector3();
                switch (i)
                {
                    case 0:
                        localPosition = new Vector3(0, 0, 0); // left
                        break;
                    case 1:
                        localPosition = new Vector3(2, 0, -(Depth - 1) * 0.5f); // back
                        break;
                    case 2:
                        localPosition = new Vector3(0, 0, -(Width - 1)); // right
                        break;
                    case 3:
                        localPosition = new Vector3(-2, 0, -2); // front
                        break;
                }

                Row roofRow = CreateSymbol<Row>("WallWithRoof", localPosition, Quaternion.Euler(0, i * 90, 0), transform);
                roofRow.Initialize(
                    i % 2 == 1 ? Width : Depth,
                    param.wallStyleRoof,
                    /*param.wallPattern*/localPattern,
                    new Vector3(1, 0, 0)
                );
                roofRow.Generate();
                roofRowOrNot = false;
            }

            double randomValue = param.Rand.NextDouble();

            if (HeightRemaining > 0 && randomValue < param.StockContinueChance && MinHeight > 0)
            {
                Stock nextStock = CreateSymbol<Stock>("stock", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextStock.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextStock.Generate(param.buildDelay, true);
                roofRowOrNot = false;
            }
            else
            {
                Roof nextRoof = CreateSymbol<Roof>("roof", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextRoof.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextRoof.Generate(param.buildDelay);
            }
        }

        protected override void ExecuteAsian()
        {
            if (parameters == null)
            {
                parameters = GetComponent<BuildingParameters>();
            }

            BuildingParameters param = (BuildingParameters)parameters;

            GameObject[] walls = new GameObject[4];

            if (HeightRemaining % 2 == 0)
            {
                AsianWall(param);
                //roofRowOrNot = true;
                //Debug.Log("Status of the bool: " + roofRowOrNot);
            }
            else
            {
                Debug.Log("Ready for roof.");
                AsianWallRoof(param);
                //roofRowOrNot = false;
            }
        }

        private void AsianWall(BuildingParameters param)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 localPosition = new Vector3();
                switch (i)
                {
                    case 0:
                        localPosition = new Vector3(0, 0, 0); // left
                        break;
                    case 1:
                        localPosition = new Vector3(2, 0, -(Depth - 1) * 0.5f); // back
                        break;
                    case 2:
                        localPosition = new Vector3(0, 0, -(Width - 1)); // right
                        break;
                    case 3:
                        localPosition = new Vector3(-2, 0, -2); // front
                        break;
                }


                Row normalRow = CreateSymbol<Row>("WallWithoutRoof", localPosition, Quaternion.Euler(0, i * 90, 0), transform);
                normalRow.Initialize(
                    i % 2 == 1 ? Width : Depth,
                    param.wallStyleNormal,
                    param.wallPattern,
                    new Vector3(1, 0, 0)
                );
                normalRow.Generate();
            }

            roofRowOrNot = true;

            double randomValue = param.Rand.NextDouble();

            if (HeightRemaining > 0 && randomValue < param.StockContinueChance || MinHeight > 0)
            {
                Stock nextStock = CreateSymbol<Stock>("stock", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextStock.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextStock.Generate(param.buildDelay, true);

            }
            else
            {
                Roof nextRoof = CreateSymbol<Roof>("roof", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextRoof.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextRoof.Generate(param.buildDelay);
            }
        }

        protected override void Execute()
        {
            if (parameters == null)
            {
                parameters = GetComponent<BuildingParameters>();
            }

            BuildingParameters param = (BuildingParameters)parameters;

            GameObject[] walls = new GameObject[4];

            for (int i = 0; i < 4; i++)
            {
                Vector3 localPosition = new Vector3();
                switch (i)
                {
                    case 0:
                        localPosition = new Vector3(0, 0, 0); // left
                        break;
                    case 1:
                        localPosition = new Vector3(2, 0, -(Depth - 1) * 0.5f); // back
                        break;
                    case 2:
                        localPosition = new Vector3(0, 0, -(Width - 1) ); // right
                        break;
                    case 3:
                        localPosition = new Vector3(-2, 0, -2); // front
                        break;
                }
                Row newRow = CreateSymbol<Row>("wall", localPosition, Quaternion.Euler(0, i * 90, 0), transform);
                newRow.Initialize(
                    i % 2 == 1 ? Width : Depth,
                    param.wallStyle,
                    param.wallPattern,
                    new Vector3(1, 0, 0)
                );
                newRow.Generate();
            }

            double randomValue = param.Rand.NextDouble();

            if (HeightRemaining > 0 && randomValue < param.StockContinueChance && MinHeight > 0)
            {
                Stock nextStock = CreateSymbol<Stock>("stock", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextStock.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextStock.Generate(param.buildDelay);
                roofRowOrNot = true;
            }
            else
            {
                Roof nextRoof = CreateSymbol<Roof>("roof", new Vector3(0, 1, 0), Quaternion.identity, transform);
                nextRoof.Initialize(Width, Depth, HeightRemaining - 1, MinHeight - 1);
                nextRoof.Generate(param.buildDelay);
            }
        }
    }
}
