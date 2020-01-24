using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class Roof : Shape
    {
        public int Width;
        public int Depth;
        public int HeightRemaining;
        public int MinHeight = 40;
        bool oneStrip = false;
        int counter = 0;

        public void Initialize(int Width, int Depth, int HeightRemaining, int MinHeight)
        {
            this.Width = Width;
            this.Depth = Depth;
            this.HeightRemaining = HeightRemaining;
            this.MinHeight = MinHeight;
        }

        // (offset) values for the next layer:
        int newWidth;
        int newDepth;

        protected override void Execute()
        {
            if (Width == 0 || Depth == 0)
                return;

            newWidth = Width;
            newDepth = Depth;

            CreateFlatRoofPart();
            CreateNextPart();
        }

        protected override void ExecuteAsian()
        {

        }

        protected override void ExecuteAsianRoof()
        {

        }

        void CreateFlatRoofPart()
        {
            BuildingParameters param = (BuildingParameters)parameters;

            int side = param.Rand.Next(2);
            Row flatRoof;

            int[] localPattern = { 0, 0, 1, 0, 0 };

            for (int i = 0; i < 5; i++)
            {
                Vector3 localPosition = new Vector3();
                switch (i)
                {
                    case 0:
                        localPosition = new Vector3(0, 0, 0); // left
                        break;
                    case 1:
                        localPosition = new Vector3(0, 0, -2); // back
                        break;
                    case 2:
                        localPosition = new Vector3(0, 0, -1); // right
                        break;
                    case 3:
                        localPosition = new Vector3(0, 0, -3); // front
                        break;
                    case 4:
                        localPosition = new Vector3(0, 0, -4); // left
                        break;
                }
                flatRoof = CreateSymbol<Row>("roofStrip",
                            localPosition,
                            Quaternion.Euler(0, 0, 0),
                            transform
                );
                //for (int ii = 0; ii < 2; ii++)
                //{


                int decideAntenna = param.Rand.Next(111);

                if (!oneStrip)
                {
                    flatRoof.Initialize(Width, param.roofStyle, localPattern, new Vector3(1, 0, 0));
                    flatRoof.Generate();
                }
                else
                {
                    flatRoof.Initialize(Width, param.specialRoofStyle, null, new Vector3(1, 0, 0));
                    flatRoof.Generate();
                }
                //}

            }
        }

        void CreateNextPart()
        {
            if (newWidth <= 0 || newDepth <= 0)
                return;
            BuildingParameters param = (BuildingParameters)parameters;

            double randomValue = param.Rand.NextDouble();
            if (randomValue < param.RoofContinueChance || HeightRemaining <= 0)
            { 
                Roof nextRoof = CreateSymbol<Roof>("roof");
                nextRoof.Initialize(newWidth, newDepth, HeightRemaining, MinHeight);
                nextRoof.Generate();
            }
        }
    }
}