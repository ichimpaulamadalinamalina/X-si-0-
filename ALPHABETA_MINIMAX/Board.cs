/**************************************************************************
 *                                                                        *
 *  File:        Board.cs                                                 *
 *  Copyright:   (c) 2022, Ichim Paula-Mădălina                           *
 *  E-mail:      paula-madalina.ichim@student.tuiasi.ro                   *
 *  Description: Class Board information about Game                       *
 *                                                                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPHABETA_MINIMAX
{
    /// <summary>
    ///  Clasa contine informatii importante starii jocului, tabla de joc, miscarile disponibile, randul jucatorilor, castigatorul,starea de sfarsit a jocului
    /// </summary>
    public class Board
    {
        public enum State { None, X, O }; // enum folositor sa vedem cine este castigatorul, cine ocupa o casuta, a cui este randul 

        List<int> _availableMoves;   //miscarile disponibile

        private State[][] _board;   //tabla de joc 
        private State _turn; //randul jucatorilor
        private State _winner;      //castigatorul 
        private int _numbermove;     //numarul de miscari/casute ocupate 
        private bool _gameover;  //variabila care desemneaza sfarsirea jocului 


        /// <summary>
        /// Constructorul clasei
        /// </summary>
 
        public Board()
        {
            _board = new State[3][];  //initializarea tablei
            _availableMoves = new List<int>();   //initilizarea listei de miscari disponibile
            ResetGame();                        // resetarea jocului
        }
        /// <summary>
        /// Functia reseteaza jocul 
        /// </summary>
        public void ResetGame()
        {
            _numbermove = 0;   //stergerea mutarilor 
            _gameover = false;   
            _turn = State.X;//incepe jucatorul cu x
            _winner = State.None;// inca nu este un castigator
            for (int row = 0; row < 3; row++)
            {
                State[] c = new State[3];
                for (int col = 0; col < 3; col++)
                {
                    c[col] = State.None;     //reseteaza fiecare coloana
                }
                _board[row] = c;   ///reseteaza tabla 
            }
            _availableMoves.Clear();  //sterge mutarile 
            for (int i = 0; i < 9; i++)
            {
                _availableMoves.Add(i);     //adauga toate casutele ca miscari disponibile deoarece sunt goale 
            }
        }

        /// <summary>
        /// Muta pe casuta corespunzatoare indexului dat
        /// </summary>
        public void Move(int index)
        {
            //tabla este vazuta ca matrice si convertim indicele in coloana si linia corespunzatoare 
            int x = index / 3;
            int y = index % 3;

            if (_board[x][y] == State.None) //daca este casuta goala
            {
                _board[x][y] = _turn; //punem pe casuta x sau 0,depinzand de a cui este randul
            }

            _numbermove++;  //se incrementeaza numarul de miscari efectuate pe tabla de joc
            _availableMoves.Remove(x * 3 + y);  // eliminam miscarea din lista celor disponibile

            if (_numbermove == 9)//tabla este plina, se termina jocul si este remiza
            {
                _winner = State.None;
                _gameover = true;
            }

            CheckFinishGame(x, y);  //verificam daca s-a terminat jocul sau nu

            _turn = (_turn == State.X) ? State.O : State.X; // verificam a cui este randul
        }

        /// <summary>
        /// Returneaza cate miscari s-au facut
        /// </summary>
        public int getMoveNumber()
        {
            return _numbermove;
        }
   
        /// <summary>
        /// Returneaza cine este castigatorul
        /// </summary>
        public State getWinner()
        {
            return _winner;
        }
        /// <summary>
        /// Returneaza a cui este randul
        /// </summary>
        public State getTurn()
        {
            return _turn;
        }

        /// <summary>
        /// Returneaza starea jocului
        /// </summary>
        public bool GameOver()
        {
            return _gameover;
        }
        /// <summary>
        /// Returneaza casutele care nu sunt ocupate 
        /// </summary>
        public List<int> getAvailableMoves()
        {
            return _availableMoves;
        }
    

        /// <summary>
        /// Verifica daca s-a terminat jocul
        /// </summary>
        private void CheckFinishGame(int x, int y)
        {
            if((_board[0][0]==_board[2][2]) && (_board[1][1]==_board[0][0]) && _board[1][1] !=Board.State.None)//diagonala principala
            {
                _winner = _turn; //avem castigator
                _gameover = true;  //jocul s-a terminat
            }
            if ((_board[0][2] == _board[1][1]) && (_board[1][1] == _board[2][0]) && _board[1][1] != Board.State.None)//diagonala secundara
            {
                _winner = _turn; //avem castigator
                _gameover = true;  //jocul s-a terminat
            }

            for (int i = 0; i < 3; i++)  // verificam pe linie 
            {
                if (_board[i][0] == _board[i][1] && _board[i][0] == _board[i][2] && _board[i][0] != Board.State.None)
                {
                    _winner = _turn;//avem castigator
                    _gameover = true;//jocul s-a terminat
                }
                if (_board[0][i] == _board[1][i] && _board[0][i] == _board[2][i] && _board[0][i] != Board.State.None)//verificare pe coloana
                {
                    _winner = _turn;//avem castigator
                    _gameover = true;//jocul s-a terminat
                }
            }

 
        }

       

        /// <summary>
        /// Copiaza starea si tabla de joc
        /// </summary>
        public Board GetCopy()
        {
            Board board = new Board(); // creaza o noua instanta a clasei Board

            for (int i = 0; i < board._board.Length; i++)
            {
                board._board[i] = (State[])this._board[i].Clone(); // copiaza tabla
            }
            board._turn = this._turn;              //copiaza randului playerului
            board._winner = this._winner;                        //copiaza winnerul 
            board._availableMoves = new List<int>();    
            board._availableMoves.AddRange(this._availableMoves);   //copiaza miscarile valabile
            board._numbermove = this._numbermove;                     //copiaza numarul de casute pline/ocupate (miscari facute)
            board._gameover = this._gameover;                     // copiaza starea de sfarsit a jocului
            return board;
        }

        /// <summary>
        /// returneaza lista miscarilor lui 0
        /// </summary>
        public List<int> Move_of_O()
        {
            List<int> move_o = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_board[i][j] == State.O) // daca pe casuta este 0
                    {
                        move_o.Add(i * 3 + j);   //adauga miscarea lui 0
                    }
                }
            }
            return move_o;   //returneaza lista cu miscarile lui 0
        }
        /// <summary>
        /// returneaza miscarea Computerului
        /// </summary>
        public int ComputerMove()
        {
            Random rand = new Random();
            int index = rand.Next(9);//Prima miscare este random
            _turn = Board.State.O; //este randul lui 0
            Move(index);            // muta pe casuta cu indexul
            return index;
        }
     
    }
}
