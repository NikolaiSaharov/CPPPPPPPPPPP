using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';
        private char previousPlayer = 'O'; // Переменная для сохранения предыдущего игрока
        private bool gameOver = false;

        public MainWindow()
        {
            InitializeComponent();
            ResetBoard();
        }

        private void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
            gameOver = false;
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = (Button)FindName($"Button{i}{j}");
                    button.Content = board[i, j].ToString();
                    button.IsEnabled = board[i, j] == ' ' && !gameOver;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver) return;

            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == ' ')
            {
                board[row, col] = currentPlayer;
                UpdateBoard();

                if (CheckWin(currentPlayer))
                {
                    MessageBox.Show($"Игрок {currentPlayer} победил!");
                    gameOver = true;
                }
                else if (CheckDraw())
                {
                    MessageBox.Show("ОГО!Ничья!");
                    gameOver = true;
                }
                else
                {
                    // Переключение между игроками
                    char temp = currentPlayer;
                    currentPlayer = previousPlayer;
                    previousPlayer = temp;

                    // Если следующий игрок - робот, сделать ход робота
                    if (currentPlayer == 'O')
                    {
                        MakeRobotMove();
                    }
                    else
                    {
                        MakeRobotMove();
                    }
                    

                    
                }
            }
        }

        private void MakeRobotMove()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (board[row, col] != ' ');

            board[row, col] = currentPlayer;
            UpdateBoard();

            if (CheckWin(currentPlayer))
            {
                MessageBox.Show($"Игрок {currentPlayer} победил!");
                gameOver = true;
            }
            else if (CheckDraw())
            {
                MessageBox.Show("ОГО!Ничья!");
                gameOver = true;
            }
            else
            {
                // Переключение между игроками
                char temp = currentPlayer;
                currentPlayer = previousPlayer;
                previousPlayer = temp;
            }
        }

        private bool CheckWin(char player)
        {
            // Проверка по горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;
            }

            // Проверка по вертикали
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                    return true;
            }

            // Проверка по диагонали
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;

            return false;
        }

        private bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                        return false;
                }
            }
            return true;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
            // Сохранение предыдущего игрока
            previousPlayer = currentPlayer;
            // Переключение между игроками при рестарте
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
    }
}