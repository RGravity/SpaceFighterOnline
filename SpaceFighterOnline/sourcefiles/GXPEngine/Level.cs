using System;
using System.Drawing;
using GXPEngine;
using System.Xml;

namespace GXPEngine
{
    class Level : GameObject
    {
        public Player1 _player1;
        public Player2 _player2;
        public Ground _ground;


        public int _levelWidth;
        public int _levelHeight;

        public Level(string levelchoice)
        {
            string levelStrings = XMLReader(levelchoice);
            int[,] levelArray = LevelArrayBuilder(levelStrings);
            BuildLevel(levelArray);
            AddChild(_player1);
            AddChild(_player2);
        }

        public string XMLReader(string level)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Levels\" + level);
            XmlElement root = doc.DocumentElement;

            if (root.HasAttribute("width"))
            {
                _levelWidth = Convert.ToInt16(root.GetAttribute("width"));
            }
            if (root.HasAttribute("height"))
            {
                _levelHeight = Convert.ToInt16(root.GetAttribute("height"));
            }


            string text = "";
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                text = node.InnerText;
            }

            return text;
        }

        public int[,] LevelArrayBuilder(string level)
        {
            string[] levelSplitter = level.Split(',');

            int[,] levelArray = new int[_levelHeight, _levelWidth];

            int indexStringLevel = 0;
            for (int h = 0; h < _levelHeight; h++)
            {
                for (int w = 0; w < _levelWidth; w++)
                {
                    levelArray[h, w] = Convert.ToInt16(levelSplitter[indexStringLevel]);
                    indexStringLevel++;
                }
            }
            return levelArray;
        }

        public void BuildLevel(int[,] levelArray)
        {
            for (int h = 0; h < _levelHeight; h++)
            {
                for (int w = 0; w < _levelWidth; w++)
                {
                    int tile = levelArray[h, w];

                    switch (tile)
                    {
                        case 1:
                            _player1 = new Player1();
                            _player1.SetXY(w * 32, h * 32-32);
                            break;
                        case 2:
                            _player2 = new Player2();
                            _player2.SetXY(w * 32, h * 32-32);
                            break;
                        case 3:
                            _ground = new Ground();
                            AddChild(_ground);
                            _ground.SetXY(w * 32, h * 32);
                            break;                            
                    }
                }
            }
        }
    }
}
