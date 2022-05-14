using UnityEngine;

namespace Utils {
    public class Probability {

        public static double RollDice(int nbRoll, int nbSide) {
            double value = 0.0d;

            for (int i = 0; i < nbRoll; i++) value += Random.Range(1, nbSide);

            return value;
        }

        public static double MaxAsymmetricRepartition(int nbDice, int nbRoll, int nbSide) {
            double[] dices = new double[nbDice];
            double res = 0.0d;
            for (int i = 0; i < nbDice; i++) {
                dices[i] += RollDice(nbRoll, nbSide);
                res += dices[i];
            }
            return (res - Utils.Tools.Max(dices));
        }


    }
}