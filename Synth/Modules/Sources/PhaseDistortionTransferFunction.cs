using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Sources {
    internal struct Point {
        internal Point(float x, float y) {
            X = x;
            Y = y;
        }

        internal float X;
        internal float Y;
    }


    internal class PhaseDistortionTransferFunction {



        /*                                   |+1                         
         *                                   |                          / p3
         *                                   |                      /
         *                                   |                  /
         *                                   |              /
         *                                   |          /
         *                                   |      /
         *                                   |  /
         *                                   |    y = m2 x x + c2
         *                                /  |
         *             p2             /      |
         *         knee point     /          |
         *         moves      /              |
         *           <--   /  -->            |
         *  ----------------------------------------------------------------
         *  -1           / x intercept       |                            +1
         *              /  (Distortion       | 
         *             /                     |
         *            /                      |
         *           /                       |                                  
         *          /                        |
         *         /                         |
         *        /                          |
         *       /y= m1 x x + c1             |
         *      /                            |
         *     /                             |
         *    /                              |-1
         *     p1
         * */


        // We can of course have extra transfer functions, and even vary transfer function per quadrant


        // Do transfer function here after normalising input to between -1 and +1
        //     -1 to +1                    -1 to +1       -1 to + 1
        private static float GetPhaseNominal(float Phase, float   Distortion) {
            // Shortcircuit if no distortion
            if (Distortion == 0)
                return Phase;

            var p1 = new Point(-1f, -1f ) ;
            var p2 = new Point(Distortion, 0f);
            var p3 = new Point(1f, 1f);

            float m;
            float c;

            if (Phase < Distortion) {
                // We are on y = m1 x x + c1
                // (-1, -1) to (Distortion, 0)
                m = (p2.Y - p1.Y) / (p2.X - p1.X);
            } else {
                // We are on y = m2 x x +c2
                // (Distortion, 0) to (1, 1)
                m = (p3.Y - p2.Y) / (p3.X - p2.X);
            }
            c = p2.Y - m * p2.X;

            return m * Phase + c;            
        }

        // Normalise values, call GetPhaseNominal transfer function, the de-normalise to 0-360°
        //           0 to 360       0 to 360     0 - 1,  0.5% = no distortion
        public static float GetPhase(float Phase, float Distortion) {
            float _Phase = Phase / 180f - 1f;
            float _distPhase = GetPhaseNominal(_Phase, Distortion);
            return (_distPhase + 1f) * 180f;
        }
    }
}
