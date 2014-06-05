﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris
{
    public class Block
    {
        public static SolidColorBrush LINE = Brushes.Cyan;
        public static SolidColorBrush SQUARE = Brushes.Yellow;
        public static SolidColorBrush TEE = Brushes.HotPink;
        public static SolidColorBrush JAY = Brushes.Blue;
        public static SolidColorBrush EL = Brushes.Orange;
        public static SolidColorBrush SSKEW = Brushes.LightGreen;
        public static SolidColorBrush ZSKEW = Brushes.Red;

        protected Point[][] _shape;
        protected Point _coordinates;
        protected SolidColorBrush _color;
        protected int _rotation;
        protected string _type;
        protected Random _rand;

        public Block(Random rand)
        {
            _rand = rand;
            generateBlock();
        }

        protected void generateBlock()
        {
            switch (_rand.Next() % 7)
            {
                case 0: //T
                    generateT();
                    break;

                case 1: //L
                    generateL();
                    break;

                case 2: // _
                    generateLine();
                    break;

                case 3: // J
                    generateJ();
                    break;


                case 4: // Z
                    generateZ();
                    break;


                case 5: // S
                    generateS();
                    break;

                case 6: //[]
                    generateSquare();
                    break;

                default:
                    _shape = null;
                    break;
            }

            _rotation = _rand.Next(0, 4);
        }

        private void generateSquare()
        {
            _color = Block.SQUARE;
            _type = "Square";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{ 
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1)}
                    };
        }

        private void generateS()
        {
            _color = Block.SSKEW;
            _type = "S Skew";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(-1,1),
                        new Point(0,1),
                        new Point(0,0),
                        new Point(1,0)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{   
                        new Point(-1,1),
                        new Point(0,1),
                        new Point(0,0),
                        new Point(1,0)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,1)}
                    };
        }

        private void generateZ()
        {
            _color = Block.ZSKEW;
            _type = "Z Skew";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,1)},
                    new Point[]{   
                        new Point(0,1),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,-1)},
                    new Point[]{   
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,1)},
                    new Point[]{   
                        new Point(0,1),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,-1)}
                    };
        }

        private void generateJ()
        {
            _color = Block.JAY;
            _type = "Jay";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(-1,1)},
                    new Point[]{   
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(-1,-1)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,-1)}
                    };
        }

        private void generateLine()
        {
            _color = Block.LINE;
            _type = "Straight line";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(2,0)},
                    new Point[]{
                        new Point(1,-2),
                        new Point(1,-1),
                        new Point(1,0),
                        new Point(1,1)},
                    new Point[]{
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(2,0)},
                    new Point[]{
                        new Point(1,-2),
                        new Point(1,-1),
                        new Point(1,0),
                        new Point(1,1)}
                    };
        }

        private void generateL()
        {
            _color = Block.EL;
            _type = "El";
            _shape = new Point[][]{
                    new Point[]{
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(-1,1)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(-1,-1)},
                    new Point[]{   
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,-1)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,1)}
                    };
        }

        private void generateT()
        {
            _color = Block.TEE;
            _type = "Tee";
            _shape = new Point[][]{
                    new Point[]{   
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,1),
                        new Point(1,0)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(-1,0)},
                    new Point[]{   
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0)},
                    new Point[]{   
                        new Point(0,-1),
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0)}
                    };
        }

        public Point Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
            }
        }

        public void Rotate()
        {
            _rotation++;
        }

        public Point[] Shape
        {
            get
            {
                return _shape[_rotation%4];
            }
        }

        public Point[] NextRotation
        {
            get
            {
                return _shape[(_rotation+1) % 4];
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return _color;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public override string ToString()
        {
            return _type;
        }
    }
}