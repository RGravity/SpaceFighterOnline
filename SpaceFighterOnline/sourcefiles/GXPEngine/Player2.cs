using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player2 : Sprite
    {
        private AnimSprite animSpritePlayer;

        private const float _braking = 0.9f;
        private const float _gravity = 0.9f;
        private const float _jumpAcceleration = -25;

        private float _lastXpos;
        private float _lastYpos;
        private float _xSpeed;
        private float _ySpeed;
        private bool _touchBorder = false;
        private bool _touchObject = false;
        private bool _movedBack = false;

        public float LastXpos { get { return _lastXpos; } }
        public float LastYpos { get { return _lastYpos; } }
        public float XSpeed { get { return _xSpeed; } }
        public float YSpeed { get { return _ySpeed; } }
        public bool TouchBorder { get { return _touchBorder; } }
        public bool TouchObject { get { return _touchObject; } set { _touchObject = value; } }
        public bool MovedBack { get { return _movedBack; } }

        public Player2()
            : base("checkers.png")
        {
            /*Animation here*/

            //animSpritePlayer = new AnimSprite("barryBackup.png", 7, 1);
            //AddChild(animSpritePlayer);
        }

        void Update()
        {
            _lastXpos = x;
            _lastYpos = y;
            UpdateControls();
        }

        void UpdateControls()
        {
            ApplySteering();
            ApplyGravityJumping();
        }

        void ApplySteering()
        {
            //move right
            if (Input.GetKey(Key.RIGHT))
            {
                _touchObject = false;
                _xSpeed++;
            }
            //move left
            else if (Input.GetKey(Key.LEFT))
            {
                _touchObject = false;
                _xSpeed--;
            }

            //check touching border
            _touchBorder = ApplyMovementAndBorders(_xSpeed, 0);

            //apply braking
            _xSpeed = _xSpeed * _braking;

            //bounce effect with border/wall
            if (_touchBorder == true)
            {
                _xSpeed = -_xSpeed;
            }
        }

        void ApplyGravityJumping()
        {
            //touchborder check
            _touchBorder = ApplyMovementAndBorders(0, _ySpeed);

            _ySpeed++;

            //check whether you are jumping or not
            if (_touchBorder == true || _touchObject == true)
            {

                _ySpeed = 0.0f;
                //check W is pressed for jump
                if (Input.GetKey(Key.UP))
                {
                    _movedBack = false;
                    _ySpeed = _jumpAcceleration;
                    _touchObject = false;
                }
            }

            else
            {
                //gravity
                if (!Input.GetKey(Key.UP))
                {

                    _ySpeed = _ySpeed + _gravity;
                }

                //if no space or W gravity
                else
                {
                    _ySpeed = _ySpeed + _gravity;
                }
            }
        }

        bool ApplyMovementAndBorders(float moveX, float moveY)
        {
            x = x + moveX;
            if (x < 0)
            {
                x = 0;
                return true;
            }
            if (x > 800 - 43)
            {
                x = 800 - 43;
                return true;
            }

            y = y + moveY;
            if (y < 0)
            {
                y = 0;
                return true;
            }
            if (y > 608 - 48 - 32)
            {
                y = 608 - 48 - 32;
                return true;
            }

            return false;
        }

        public void moveBack(string side, int crateCoords)
        {
            switch (side)
            {
                case "top":
                    //_touchCrate = true;
                    _ySpeed = 0;
                    this.y = crateCoords;
                    x = LastXpos;
                    break;
                case "bottom":
                    //nothing
                    break;
                case "right":
                    _ySpeed = 15;
                    _xSpeed = 0;
                    break;
                case "left":
                    _ySpeed = 15;
                    _xSpeed = 0;
                    break;
            }
            _movedBack = true;
            x = LastXpos;
            y = LastYpos;
        }
    }
}
