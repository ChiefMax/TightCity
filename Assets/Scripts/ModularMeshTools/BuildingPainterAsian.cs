using UnityEngine;

namespace Demo {
	public class BuildingPainterAsian : MonoBehaviour {
		public GameObject HousePrefab;
        public int Width;
		public int Depth;
		public WallStyle[] wallStyles;
		public GameObject[] roofStyle;
        public GameObject[] specialRoofStyle;

        // For placing buildings in play mode:
        void Update() {
			if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift)) {
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
					//CreateHouse(/*hit.point*/);
				}
			}
		}


		public void CreateHouse() {
			if (Width==0 || Depth==0)
				return;

			GameObject house = Instantiate(HousePrefab);
            house.transform.position = transform.position;
            house.transform.parent = transform;

            var houseBuilder = house.GetComponent<Stock>();
			houseBuilder.Width=Width; 
			houseBuilder.Depth=Depth;

            // Choose random wall style:
            var param = house.GetComponent<BuildingParameters>();
			param.wallStyle=wallStyles[(int)(Random.value * wallStyles.Length)].wallParts;
			param.seed=(int)(Random.value*1000000);

            int decideAntenna = param.Rand.Next(5);

            if (decideAntenna % 2 >= 1)
            {
                param.specialRoofStyle = specialRoofStyle;
            }
            else
            {
                param.roofStyle = roofStyle;
            }

            print("Generating...");
			houseBuilder.Generate();
            print("Done...");
        }
	}
}