/**************************************************************************
 *                                                                        *
 *  File:        Minimax_AlphaBeta.cs                                     *
 *  Copyright:   (c) 2022, Ichim Paula-Mădălina                           *
 *  E-mail:      paula-madalina.ichim@student.tuiasi.ro                   *
 *                                                                        *
 *  Description: Class that the algoritHm of game                         *
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
    ///  Clasa contine algoritmul minimax cu retezare alfa beta
    public class Minimax_AlphaBeta
    {
        private int maxDepth;
        /// <summary>
        ///  Constructorul clasei
        /// </summary>
        public Minimax_AlphaBeta(int maxDepth) // utilizatorul va putea alea parametrul max-Depth
        {
            this.maxDepth = maxDepth;
        }

        /// <summary>
        ///  Algoritmul propriu-zis de retezare 
        /// </summary>
        public int GetBestMove_alphabeta(Board board, Board.State player, int depth, double alpha, double beta)
        {
            Board.State computer = (player == Board.State.X) ? Board.State.O : Board.State.X; // verificăm dacă Computerul este cu X sau cu 0

            if (board.GameOver() || maxDepth==depth)  //conditia de terminare
            {
                return EvaluationFunction(board, player); 
            }

            if (board.getTurn() == player)  
            {
                int index_bestmove = -1;//initializam cu o valoare imposibila
                List<int> valid_moves = board.getAvailableMoves(); //lista mutari posibile
                for (int i = 0; i < valid_moves.Count; i++)
                {
                    int current_move = valid_moves[i]; // luam mutarile la rand 
                    Board modifiedBoard = board.GetCopy(); //facem copie la tabla de joc
                    modifiedBoard.Move(current_move);  //facem mutarea curenta
                    int best_move = GetBestMove_alphabeta(modifiedBoard, player, depth, alpha, beta); //apelam functia 

                    if (best_move > alpha) // daca miscarea are scorul mai mare decat alfa 
                    {
                        alpha = best_move;    //alfa=miscare 
                        index_bestmove = current_move;// indexul este miscarea curenta
                    }
                    if (alpha >= beta)  
                    {
                        break;
                    }
                }
                if (index_bestmove != -1) //exista miscare mai buna 
                {
                    board.Move(index_bestmove); //facem miscarea 
                }
                return (int)alpha; 
            }
            else // este randul computerului , procedam asemanator dar cu beta 
            {
                int index_bestmove = -1;//initializam cu o valoare imposibila
                List<int> valid_moves = board.getAvailableMoves();//lista mutari posibile
                for (int i = 0; i < valid_moves.Count; i++)
                {
                    int current_move = valid_moves[i]; //luam fiecare mutare in parte
                    Board modifiedBoard = board.GetCopy();//copiem tabla de joc
                    modifiedBoard.Move(current_move); // facem mutarea curenta
                    int score = GetBestMove_alphabeta(modifiedBoard, player, depth, alpha, beta); //vedem score-ul ei 
                    if (score < beta)   // daca este mai mica decat valoarea beta o salvam ca miscare mai buna
                    {
                        beta = score;
                        index_bestmove = current_move;
                    }
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                if (index_bestmove != -1)//exista miscare mai buna
                {
                    board.Move(index_bestmove);//efectuam miscarea
                }
                return (int)beta;
            }
        }

        /// <summary>
        ///  Functia de evaluare
        /// </summary>
        public int EvaluationFunction(Board board, Board.State player)
        {
            Board.State computer = (player == Board.State.X) ? Board.State.O : Board.State.X; // verificam daca este cu 0 sau X computerul
            if (board.GameOver() && board.getWinner() == player)//a castigat playerul 
            {
                return 1;         
            }
            else if (board.GameOver() && board.getWinner() == computer) //a castigat calculatorul
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
