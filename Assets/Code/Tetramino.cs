namespace Code
{
    public class Tetramino
    {
        public enum Type : int
        {
            I = 0,
            O,
            T,
            S,
            Z,
            J,
            L,
            MaxValue
        }

        //TODO Just a test, re-check coords, m.b. need to use other elements...

        // Stick 
        private void SetupI(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[0, 0] = fill;
            figure[1, 0] = fill;
            figure[2, 0] = fill;
            figure[3, 0] = fill;
        }

        // Block
        private void SetupO(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[1, 1] = fill;
            figure[1, 2] = fill;
            figure[2, 1] = fill;
            figure[2, 2] = fill;
        }

        // T
        private void SetupT(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[0, 1] = fill;
            figure[1, 1] = fill;
            figure[2, 1] = fill;
            figure[1, 2] = fill;
        }

        // S
        private void SetupS(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[1, 1] = fill;
            figure[2, 1] = fill;
            figure[2, 2] = fill;
            figure[3, 2] = fill;
        }

        // Z
        private void SetupZ(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[1, 2] = fill;
            figure[2, 2] = fill;
            figure[2, 1] = fill;
            figure[3, 1] = fill;
        }

        // J
        private void SetupJ(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[1, 2] = fill;
            figure[1, 1] = fill;
            figure[2, 1] = fill;
            figure[3, 1] = fill;
        }

        // L
        private void SetupL(int fillValue)
        {
            fill = fillValue;
            
            figure = new int[4, 4];
            figure[1, 1] = fill;
            figure[2, 1] = fill;
            figure[3, 1] = fill;
            figure[3, 2] = fill;
        }

        private int[,] figure = new int[4, 4];

        private int fill = 0;

        public Tetramino(Type type, int fillValue)
        {
            switch (type)
            {
                case Type.I:
                    SetupI(fillValue);
                    break;
                case Type.O:
                    SetupO(fillValue);
                    break;
                case Type.T:
                    SetupT(fillValue);
                    break;
                case Type.S:
                    SetupS(fillValue);
                    break;
                case Type.Z:
                    SetupZ(fillValue);
                    break;
                case Type.J:
                    SetupJ(fillValue);
                    break;
                case Type.L:
                    SetupL(fillValue);
                    break;
                default:
                    throw new System.Exception("Unknown figure type, check generation code!");
            }
        }

        public int[,] GetFigure()
        {
            return figure;
        }

        public int GetFill()
        {
            return fill;
        }

        public void Rotate()
        {
            var newFigure = new int[4, 4];
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    newFigure[i, j] = figure[j, 3 - i];
                }
            }

            figure = newFigure;
        }
    }
}
