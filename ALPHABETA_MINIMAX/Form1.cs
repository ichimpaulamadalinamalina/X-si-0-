/**************************************************************************
 *                                                                        *
 *  File:        Form1.cs                                                 *
 *  Copyright:   (c) 2022, Ichim Paula-Mădălina                           *
 *  E-mail:      paula-madalina.ichim@student.tuiasi.ro                   *
 *                                                                        *
 *  Description:   The MainForm of the application                        *
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALPHABETA_MINIMAX
{
    public partial class Form1 : Form
    {
        Board _board;
        int _depth; 
        int score_player = 0;
        int score_computer = 0;
        int _indexComputer = 0;
        bool _turn = true;
        public List<Button> _buttons;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Functie care pune X pe casetele selectate de jucator
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;//butonul apasat
            label1.Text = "";
            string number_button = button.Name.Substring(button.Name.Length - 1); //extragem indexul butonului
            //convertim indecsii de la 1 la 9, in indicii unei matrici de 3x3
            int x = (Int32.Parse(number_button) - 1) / 3;  
            int y = (Int32.Parse(number_button) - 1) % 3;
            button.Text = "X";  //plasam x pe butonul selectat
            button.Enabled = false; 
            GameLoop(x, y);
        }

        /// <summary>
        ///  Functie care reseteaza scorul playerilor si tabla de joc
        /// </summary>
        private void Board_newgame()
        {

            _board.ResetGame(); //reseteaza tabla de joc
            label3.Text = score_player.ToString();//actualizeaza scorul
            label4.Text = score_computer.ToString();//actualizeaza scorul
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].Enabled = true;  //activeaza butoanele
                _buttons[i].Text = "";        //reseteaza textul din ele 
            }

        }
        /// <summary>
        ///  Functie care prin apasarea butonul de reset, reseteaza scorul si seteaza un nou joc 
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            score_player = 0;  //reseteaza scorul playerului 
            score_computer = 0;//reseteaza scorul computerului
            Board_newgame();  //un nou joc
        }

        /// <summary>
        ///  Load Main Form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //initializam butoanele , combo-boxul 
            _board = new Board();
            _buttons = new List<Button>();
            _buttons.Add(button1);
            _buttons.Add(button2);
            _buttons.Add(button3);
            _buttons.Add(button4);
            _buttons.Add(button5);
            _buttons.Add(button6);
            _buttons.Add(button7);
            _buttons.Add(button8);
            _buttons.Add(button9);
            for (int i = 2; i < 9; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 2;
            _depth = (int)comboBox1.SelectedItem;
        }

        /// <summary>
        ///  Functie care face muatarile lui 0
        /// </summary>
        private void moveOnBoard(Board board)
        {
            List<int> oStates = board.Move_of_O();  //mutarile lui 0
            for (int i = 0; i < oStates.Count; i++)
            {
                int index = oStates[i];
                _buttons[index].Text = "O";//punem 0 
                _buttons[index].Enabled = false;//dezactivam butonul
            }
        }
        /// <summary>
        ///  Functie care se ocupa cu secventa mutarilor si apelarea verificarea castigatorului
        /// </summary>
        private void GameLoop(int x, int y)
        {
            if (!_board.GameOver())  //cat timp nu s-a terminat jocul
            {
                _board.Move(x * 3 + y);//muta pe indexul corespunzator indecsilor matricii, pentru player
                Minimax_AlphaBeta alphaBeta = new Minimax_AlphaBeta(_depth);
                alphaBeta.GetBestMove_alphabeta(_board, Board.State.O, 0, int.MinValue, int.MaxValue);//apeleaza functia alpha beta
                moveOnBoard(_board);//muta pentru computer
                CheckTheWinner();//verificam daca exista un castigator
                if (_board.GameOver())//daca jocul s-a terminat
                    CheckTheWinner(); //verifica cine a castigat
            }
            else
            {
                CheckTheWinner();//verificam daca exista un castigator
            }
        }

        /// <summary>
        ///  Functie care se ocupa cu verificarea castigatorului
        /// </summary>
        private void CheckTheWinner()
        {
            if (_board.getWinner() == Board.State.X)//exista un castigator si este playerul
            {
                label1.Text = "You Won!";
                score_player++;//se incrementeaza scorul playerului 
                MessageBox.Show("You Won!", " ", MessageBoxButtons.OK);//mesaj cu ai castigat
                _turn = !_turn;//schimbam randul 
               
                Board_newgame();//resetam tabla de joc
            
                if (!_turn)//incepe celalalt jucator
                {
                    _indexComputer = _board.ComputerMove();
                    _buttons[_indexComputer].Text = "O";
                    _buttons[_indexComputer].Enabled = false;
                }

            }
            else if (_board.getWinner() == Board.State.O)//exista un castigator si este calculatorul
            {
                label1.Text = "You Lost!";//mesaj cu ai pierdut
                score_computer++; //incrementam scorului computerului 
                MessageBox.Show("You Lost!", " ", MessageBoxButtons.OK);
                _turn = !_turn;//schimbam randul
                Board_newgame();//resetam tabla de joc 
                if (!_turn)// incepe computerul daca jocul anterior a inceput playerul
                {
                    _indexComputer = _board.ComputerMove();
                    _buttons[_indexComputer].Text = "O";
                    _buttons[_indexComputer].Enabled = false;
                }

            }
            else if (_board.getMoveNumber() == 9 && _board.getWinner() == Board.State.None)//nu exista un castigator, remiza
            {
                label1.Text = "Draw!";
                MessageBox.Show("It's a draw!", " ", MessageBoxButtons.OK);
                _turn = !_turn;//schimbam randul
                Board_newgame();//resetam tabla de joc 
                if (!_turn)//incepe computerul daca jocul anterior a inceput playerul
                {
                    _indexComputer = _board.ComputerMove();
                   _buttons[_indexComputer].Text = "O";
                   _buttons[_indexComputer].Enabled = false;
                }

            }
        }

    }
}
