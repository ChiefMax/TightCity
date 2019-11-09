using UnityEngine;

namespace Demo {
	public class BuildingParametersAsian : MonoBehaviour {
		public int seed;
        [HideInInspector]
        public float buildDelay;
		public float RoofContinueChance;
		public float StockContinueChance;

		public GameObject[] wallStyle;
		public int[] wallPattern;

		public GameObject[] roofStyle;
        public GameObject[] specialRoofStyle;

        System.Random rand=null;

		public System.Random Rand {
			get {
				if (rand==null) {
					rand=new System.Random(seed);
				}
				return rand;
			}
		}

		public void ResetRandom() {
			rand=new System.Random(seed);
		}
	}
}
