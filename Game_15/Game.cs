using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_15
{
    class Game
    {
        int size;
        int[,] map;
        int space_x, space_y;
        static Random rnd = new Random(); // static, щоб не повертав однакові числа
        public Game(int size)
        {
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this.size = size;
            map = new int[size, size];

        }

        public void start()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    map[x, y] = coords_to_position(x, y) + 1;

                }
                space_x = size - 1;
                space_y = size - 1;

                map[space_x, space_y] = 0;//the latest button==0
            }
        }


        public void Switch(int position)//switch button's position 
        {
            int x, y;
            position_to_coords(position, out x, out y);// отримую координату в масиві
            if (Math.Abs(space_x - x) + Math.Abs(space_y - y) != 1) return;//дозволяє переміщувати комірки лише на сусідню позицію
            else
            {
                map[space_x, space_y] = map[x, y];
                map[x, y] = 0;
                space_x = x;
                space_y = y;
            }
        }

    
        public void Switch_random()//mix array 
        {
            // Switch(rnd.Next(0,size* size)); - неоптимізоване рішення задачі
            int step = rnd.Next(0, 4);//максимально можлива к-сть ходів в грі ==4, більш оптимізований варіант 
            int x = space_x;
            int y = space_y;
            switch (step)
            {
                case 0: x--; break;// go left 
                case 1: x++; break;//go right
                case 2: y--; break;// go up
                case 3: y++; break;// go down
            }
            Switch(coords_to_position(x, y));// викликаю функцію, передаю координати 
        }

     

        public int get_position(int position)// returns button's position 
        {
            int x, y;
            position_to_coords(position, out x, out y);
            if (x < 0 || x >= size) return 0;//перевірка, щоб уникнути переповнення масива 
            if (y < 0 || y >= size) return 0;//перевірка, щоб уникнути переповнення масива 

            return map[x, y];//check it 
        }

        public bool check_positions()//перевірка результату гри 
        {
            if (!(space_x == size - 1 && space_y == size - 1))
                return false;//пуста клітинка повинна бути в правому нижньому кутку
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!(x == size - 1 && y == size - 1))//якщо це не остання комірка, то перевіряємо умову нижче 
                        if (map[x, y] != coords_to_position(x, y) + 1)
                            return false;
                }
            }
            return true;
        }

        private int coords_to_position(int x, int y)
        {
            if (x < 0) x = 0;//перевірка чи координати не виходять за рамки масива 
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return x * size + y; // знаходить номер позиції кнопки
        }

        private void position_to_coords(int position, out int x, out int y)//обернена функція до coords_to_position
        {
            if (position < 0) position = 0;//перевірка на вихід за рамки 
            if (position > size * size - 1) position = size * size - 1;
            x = position / size;//знаходить координату х
            y = position % size;//знаходить координату х
        }

       
    }
}
